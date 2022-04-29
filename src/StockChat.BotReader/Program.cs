using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace StockChat.BotReader
{
    public class Program
    {
        public static async Task Reader(string[] args)
        {
            const string url = "http://localhost:5000/chat";

            await using var connections = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            await connections.StartAsync();

            await foreach (var date in connections.StreamAsync<DateTime>("StreamingHour"))
            {
                Console.WriteLine(date);
            }
        }
    }
}
