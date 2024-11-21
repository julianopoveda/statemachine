# Projeto State Machine

O objetivo deste projeto e dar suporte para o post homonimo

## Requisitos funcionais

- Baixar as informações de uma api do igpm (https://api.bcb.gov.br/dados/serie/bcdata.sgs.4175/dados?formato=csv)
- Ler um arquivo com as dĩvidas a serem calculadas
- Gerar um relatório de saída em formato .csv contendo os dados do cliente bem como sua dívida atualizada
- Persistir os dados na tabela dívidas

## Requisitos não funcionais

- Garantir que a máquina de estado possa parar seu processamento em qualquer etapa do processo e reiniciar de onde parou
- permitir a passagem de um contexto (para mostrar que a máquina pode gerar um output para ser adicionado como input da próxima etapa)

## Ferramentas

- sqlite
- api do bc: <https://dadosabertos.bcb.gov.br/dataset/4175-divida-mobiliaria---participacao-por-indexador---posicao-em-carteira---igp-m/resource/cc4d29f5-cbd7-47b6-9ab7-712407afc89f>