namespace Fly.Handler.Tcp
{
    /// <summary>
    /// 创建Tcp连接客户端
    /// </summary>
    public interface ITcpClientCreator:IClientCreator
    {
        /// <summary>
        /// 是否可以获得一个合理的ip地址
        /// </summary>
        bool HasValidIpAddress { get; }

        /// <summary>
        /// Url地址
        /// </summary>
        string Url { get; }
    }
}
