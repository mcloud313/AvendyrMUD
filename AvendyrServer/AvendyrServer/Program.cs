using AvendyrServer;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        const int port = 13000;

        var localAddr = IPAddress.Parse("127.0.0.1");

        var server = new TcpListener(localAddr, port);

        server.Start();

        Console.WriteLine("Waiting for a connection...");

        while (true)
        {
            var client = await server.AcceptTcpClientAsync();

            // Pass off to a separate method so we can await a new client immediately.
            HandleClientAsync(client);
        }
    }

    static async void HandleClientAsync(TcpClient client)
    {
        var bytes = new byte[256];
        Console.WriteLine("Connected");

        var stream = client.GetStream();

        int i;

        while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
        {

            //Translate data bytes to an ASCII string and print it. 
            var data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine($"Received: {data}");


            string commandName = data.Split(' ')[0].ToLower(); //First word is the command name 
            string argument = string.Join(' ', data.Split(' ').Skip(i)); //Rest of the string is the argument

            var commandRegistry = new CommandRegistry();
            if (commandRegistry.Commands.TryGetValue(commandName, out var commandHandler)) 
            {
                await commandHandler.ExecuteAsync(argument, client);
            } else
            {
                // Handle unknown command.
            }

            //Process the data sent by the client and send back a response 
            data = data.ToUpper();

            var msg = Encoding.ASCII.GetBytes(data);

            //Send back a response 
            await stream.WriteAsync(msg, 0, msg.Length);
            Console.WriteLine($"Sent: {data}");
        }

        client.Close();
    }
}
