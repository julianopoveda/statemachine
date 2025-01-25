// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using statemachine.presentation;
using statemachine.presentation.States;

var serviceProvider = new ServiceCollection();
serviceProvider.AddScoped<FailState>();
serviceProvider.AddSingleton<StateMachine>(f=> new StateMachine(f.GetService<FailState>(), f));
var service = serviceProvider.BuildServiceProvider();

var stateMachine = service.GetService<StateMachine>();
stateMachine.Start();