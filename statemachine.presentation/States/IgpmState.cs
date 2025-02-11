using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using statemachine.presentation.DTO;

namespace statemachine.presentation.States;

public class IgpmState : IState
{
    public Type NextState { get; set; }
    public IEnumerable<Type> NextStatesAllowed { get; set; }
    private IDbConnection connection;
    private readonly HttpClient _client;

    public IgpmState(HttpClient client)
    {
        connection =
            new SqliteConnection(
                "DataSource=/home/poveda/projects/statemachine/statemachine.presentation/db/statemachine.db");
        _client = client;
        NextStatesAllowed = new List<Type>
        {
            typeof(FailState)
        };
    }

    public (Type nextState, object outputContext) Execute(object inputContext)
    {
        IEnumerable<IGPMIndex> igpm = !IgpmIndexIsUpdated()
            ? GetIgpmIndexUpdated().GetAwaiter().GetResult()
            : GetIgpmIndex();

        if (!igpm.Any())
            NextState = NextStatesAllowed.First(s => s.Name != "FailState");

        return (NextState, igpm);
    }

    private IEnumerable<IGPMIndex> GetIgpmIndex()
    {
        throw new NotImplementedException();
    }

    private async Task<IEnumerable<IGPMIndex>> GetIgpmIndexUpdated()
    {
        var response = await _client.GetAsync("https://api.bcb.gov.br/dados/serie/bcdata.sgs.4175/dados?formato=json");

        if (!response.IsSuccessStatusCode)
            return new List<IGPMIndex>();

        var igpmData = await response.Content.ReadFromJsonAsync<IEnumerable<IGPMIndex>>();

        UpdateIgpmIndex(igpmData);

        return igpmData;
    }

    private void UpdateIgpmIndex(IEnumerable<IGPMIndex> igpm)
    {
        //Save it to igpm database
        connection.Open();
        var insertIgpmIndex = connection.CreateCommand();

        insertIgpmIndex.CommandText = 
            "insert into igpm_dados values (@indiceData,@indice)" +
            "on conflict (indiceDate) do nothing";

        foreach (var (data, indice) in igpm)
        {
            insertIgpmIndex.Parameters.Clear();
            insertIgpmIndex.Parameters.Add(new SqliteParameter("@indiceData", data));
            insertIgpmIndex.Parameters.Add(new SqliteParameter("@indice", indice));
            insertIgpmIndex.ExecuteNonQuery();
        }

        connection.Close();
    }

    /// <summary>
    /// Check if IGP-M index is updated in database for the current Date
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private bool IgpmIndexIsUpdated()
    {
        connection.Open();
        var checkIGPM = connection.CreateCommand();
        checkIGPM.CommandText = "select 1 from igpm_dados where indiceDate=@currentDate";
        checkIGPM.Parameters.Add(new SqliteParameter("@currentDate", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-2).ToString("dd/MM/yyyy")));

        var result= checkIGPM.ExecuteScalar();
        
        connection.Close();
        return result != null;
    }
}