// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceCollection = new ServiceCollection();
serviceCollection.AddGitHubClient();
