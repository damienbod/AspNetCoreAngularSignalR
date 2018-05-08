using Dtos;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConsoleSignalRMessagePack
{
    class Program
    {
        private static HubConnection _hubConnection;

        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            await SetupSignalRHubAsync();
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
                    await _hubConnection.SendAsync("Send", new MessageDto() { Id = Guid.NewGuid(), Name = message, Amount = 7 });
                    Console.WriteLine("SendAsync to Hub");
                }
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            await _hubConnection.DisposeAsync();
        }

        public static async Task SetupSignalRHubAsync()
        {
            IHubProtocol protocol = new MessagePackHubProtocol();
            var hubConnectionBuilder = new HubConnectionBuilder()
                 .WithUrl("https://localhost:44324/loopymessage")
                 .ConfigureLogging(factory =>
                 {
                     factory.AddConsole();
                     factory.AddFilter("Console", level => level >= LogLevel.Trace);
                 }) ;
            hubConnectionBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IHubProtocol), protocol));

            _hubConnection = hubConnectionBuilder.Build();
             await _hubConnection.StartAsync();
        }
    }
}
