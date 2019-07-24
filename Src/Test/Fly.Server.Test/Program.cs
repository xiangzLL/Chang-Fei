using System;
using Fly.Handler;
using Fly.Handler.Tcp;

namespace Fly.Server.Test
{
    class Program
    {


        static void Main(string[] args)
        {
            var server = new ServerBootstrap<TcpServerChannel>();

            server.AddHandler(new EchoServerHandler());
            server.BindAsync(9096).ConfigureAwait(false);

            Console.ReadLine();
        }
    }
}
