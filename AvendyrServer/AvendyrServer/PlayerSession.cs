using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AvendyrServer
{
    public class PlayerSession
    {
        public TcpClient Client { get; private set; }

        public string PlayerName { get; set; }

        public PlayerSession(TcpClient client)
        {
            Client = client;
        }
    }
}
