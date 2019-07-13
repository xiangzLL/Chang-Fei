using System;
using System.Linq;
using System.Threading.Tasks;
using Consul;

namespace Fly.Core.Utilities
{
    /// <summary>
    /// 通过负载均衡从consul中获取service
    /// </summary>
    public class ServiceBalance
    {
        /// <summary>
        /// 根据ServiceName 随机获取到Url
        /// </summary>
        /// <param name="consulServerUrl">Consul 服务器地址</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public async Task<string> GetUrlByConsulServiceName(string consulServerUrl,string serviceName)
        {
            using (var consulClient = new ConsulClient(c => c.Address = new Uri(consulServerUrl)))
            {
                var services = (await consulClient.Agent.Services()).Response.Values
                    .Where(s => s.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).ToList();
                if (!services.Any())
                {
                    throw new ArgumentException($"find {serviceName} failed");
                }

                var service = services.ElementAt(Environment.TickCount % services.Count);
                return $"{service.Address}:{service.Port}";
            }
        }
    }
}
