using Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleSignalRMessagePack;

class Program
{
    private static HubConnection? _hubConnection;

    public static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        await SetupSignalRHubAsync();
        if(_hubConnection != null )
        {
            _hubConnection.On<MessageDto>("Send", (message) =>
            {
                Console.WriteLine($"Received Message: {message.Name}");
            });
            Console.WriteLine("Connected to Hub");
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    var message = Console.ReadLine();
                    if (message != null)
                    {
                        await _hubConnection.SendAsync("Send",
                        new MessageDto
                        {
                            Id = Guid.NewGuid(),
                            Name = message,
                            Amount = 7
                        });
                    }

                    Console.WriteLine("SendAsync to Hub");
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }

    public static async Task SetupSignalRHubAsync()
    {
        _hubConnection = new HubConnectionBuilder()
             .WithUrl("https://localhost:44324/loopymessage")
             .AddMessagePackProtocol()
             .ConfigureLogging(factory =>
             {
                 factory.AddConsole();
                 factory.AddFilter("Console", level => level >= LogLevel.Trace);
             }).Build();

         await _hubConnection.StartAsync();
    }
}
