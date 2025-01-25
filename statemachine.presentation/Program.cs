// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using statemachine.presentation;
using statemachine.presentation.States;

var serviceProvider = new ServiceCollection();
serviceProvider.AddHttpClient<IgpmState>();
serviceProvider.AddScoped<IgpmState>();
serviceProvider.AddScoped<FailState>();
serviceProvider.AddSingleton<StateMachine>(f=> new StateMachine(f.GetService<IgpmState>(), f));
var service = serviceProvider.BuildServiceProvider();

var stateMachine = service.GetService<StateMachine>();
stateMachine.Start();