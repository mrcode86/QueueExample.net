﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Queue.RabbitMQ;

namespace Queue.Demo;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        Console.WriteLine("Enter a message to send:");
        var text = Console.ReadLine();

        var message = new TestMessage { Text = text };
        var messageQueue = host.Services.GetRequiredService<IMessageQueue<TestMessage>>();
        await messageQueue.SendAsync(message, EventTypes.Added);
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddLogging(configure => configure.AddConsole());
                // Using the provided Setup class for InMemory
                services.ConfigureRabbitMq(configuration.GetConnectionString("MyConnectionString"));
            });
    }
}