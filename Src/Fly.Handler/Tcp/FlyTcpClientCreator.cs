using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Fly.Logger;

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

        /// <summary>
        /// 通过ip和端口创建FlyTcpClientCreator
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
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
                _ipAddress = GetIpAddress(_host, _port);
                if (_ipAddress != null)
                {
                    IpCache.AddOrUpdate(_host, h => _ipAddress, (h, exist) => _ipAddress);
                }
            }
        }

        /// <summary>
        /// 通过url地址创建FlyTcpClientCreator eg. www.baidu.com:8080
        /// </summary>
        /// <param name="url"></param>
        public FlyTcpClientCreator(string url)
        {
            var hostAndPort = url.Split(':');
            var host = hostAndPort[0];
            _host = host;
            if (hostAndPort.Length == 2)
            {
                if (!int.TryParse(hostAndPort[1], out _port))
                {
                    throw new InvalidOperationException("Invalid port.");
                }
            }

            if (IpCache.TryGetValue(host, out var ipAddress))
            {
                _ipAddress = ipAddress;
            }
            else
            {
                _ipAddress = GetIpAddress(host, _port);
                if (_ipAddress != null)
                {
                    IpCache.AddOrUpdate(host, h => _ipAddress, (h, exist) => _ipAddress);
                }
            }
        }

        private IPAddress GetIpAddress(string host, int port)
        {
            IPAddress address = null;
            try
            {
                var ipAddresses = Dns.GetHostAddresses(host);
                //先查找IPV6的地址
                address = ipAddresses?.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetworkV6) ??
                              ipAddresses?.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

                return address;
            }
            catch (Exception e)
            {
                LogHelper.LogError(e.ToString());
            }

            return address;
        }

        public IClient CreateClient()
        {
            if (_ipAddress == null)
            {
                _ipAddress = GetIpAddress(_host, _port);
                if (_ipAddress != null)
                {
                    IpCache.AddOrUpdate(_host, h => _ipAddress, (h, exist) => _ipAddress);
                }
                else
                {
                    throw new InvalidOperationException("Can not get ip address from host.");
                }
            }
            return new FlyTcpClient(new IPEndPoint(_ipAddress,_port));
        }

    }
}
