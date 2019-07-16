using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Fly.Handler.Tcp
{
    /// <summary>
    /// FlyClient的创建器
    /// </summary>
    public class FlyTcpClientCreator:ITcpClientCreator
    {
        private static readonly ConcurrentDictionary<string,IPAddress> IpCache = 
            new ConcurrentDictionary<string, IPAddress>();

        private readonly int _port;
        private readonly string _host;
        private IPAddress _ipAddress;

        public bool HasValidIpAddress => _ipAddress != null;
        public string Url => $"{_host}:{_port}";

        public FlyTcpClientCreator(string host, int port)
        {
            _host = host;
            _port = port;
            if (IpCache.TryGetValue(host, out var ipAddress))
            {
                _ipAddress = ipAddress;
            }
            else
            {
                _ipAddress = GetIpAddressAsync(_host, _port).Result;
                if (_ipAddress != null)
                {
                    IpCache.AddOrUpdate(_host, h => _ipAddress, (h, exist) => _ipAddress);
                }
            }
        }

        private async Task<IPAddress> GetIpAddressAsync(string host, int port)
        {
            IPAddress address = null;
            try
            {
                var ipAddresses = await Dns.GetHostAddressesAsync(host);
                //先查找IPV6的地址
                address = ipAddresses?.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetworkV6) ??
                              ipAddresses?.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

                return address;
            }
            catch (Exception e)
            {
                //打印日志
            }

            return await Task.FromResult(address);
        }

        public IClient CreateClient()
        {
            throw new NotImplementedException();
        }

    }
}
