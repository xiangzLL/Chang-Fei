using System;
using System.Collections.Generic;
using System.Text;

namespace Fly.Handler.Tcp
{
    public class FlyTcpClientCreator:ITcpClientCreator
    {
        public IClient CreateClient()
        {
            throw new NotImplementedException();
        }

        public bool HasValidIpAddress { get; }
        public string Url { get; }
    }
}
