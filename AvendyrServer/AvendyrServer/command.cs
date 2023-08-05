using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AvendyrServer
{

    public interface ICommand
    {
        string Name { get; }
        Task ExecuteAsync(string argument, TcpClient client);
    }

    public class SayCommand : ICommand
    {
        public string Name => "say";

        public async Task ExecuteAsync(string argument, TcpClient client)
        {
            // Implement the logic for the "say" command here.
        }
    }

    public class QuitCommand : ICommand
    {
        public string Name => "quit";

        public async Task ExecuteAsync(string argument, TcpClient client)
        {
            // Implement the logic for the "quit" command here.
        }
    }

    public class CommandRegistry
    {
        public Dictionary<string, ICommand> Commands { get; }

        public CommandRegistry()
        {
            Commands = new Dictionary<string, ICommand>
            {
                { "say", new SayCommand() },
                { "quit", new QuitCommand() },
                // Add other commands here.
            };
        }
    };
}
