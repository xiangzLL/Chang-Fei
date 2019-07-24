using System.Threading.Tasks;
using Fly.Handler.Channels;

namespace Fly.Handler
{
    /// <summary>
    /// 服务端入口
    /// </summary>
    public class ServerBootstrap
    {
        private readonly IServerChannel _serverChannel;

        public ServerBootstrap(IServerChannel channel)
        {
            _serverChannel = channel;
        }

        /// <summary>
        /// 启动服务监听
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public Task BindAsync(int port)
        {
            return _serverChannel.BindAsync(port);
        }

        /// <summary>
        /// 注册处理通道
        /// </summary>
        /// <param name="handler"></param>
        public void AddHandler(IChannelHandler handler)
        {

        }
    }
}
