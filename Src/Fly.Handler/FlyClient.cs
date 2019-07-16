using Fly.Handler.Tcp;

namespace Fly.Handler
{
    /// <summary>
    /// FlyClient
    /// </summary>
    public class FlyClient:BaseFlyClient
    {
        private readonly IClientCreator _creator;

        /// <summary>
        /// 创建FlyClient
        /// </summary>
        /// <param name="creator">创建指定协议的客户端</param>
        public FlyClient(IClientCreator creator)
        {
            _creator = creator;
        }
    }
}
