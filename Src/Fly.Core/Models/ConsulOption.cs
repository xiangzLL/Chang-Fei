namespace Fly.Core.Models
{
    /// <summary>
    /// Consul 注册发现相关参数
    /// </summary>
    public class ConsulOption
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务IP
        /// </summary>
        public string ServiceIp { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServicePort { get; set; }

        /// <summary>
        /// 服务监控检查地址
        /// </summary>
        public string ServiceHealthCheck { get; set; }

        /// <summary>
        /// Consul地址
        /// </summary>
        public string ConsulAddress { get; set; }
    }
}
