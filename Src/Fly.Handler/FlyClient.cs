namespace Fly.Handler
{
    /// <summary>
    /// FlyClient
    /// </summary>
    public class FlyClient:BaseFlyClient
    {
        /// <summary>
        /// 创建Tcp协议的客户端
        /// </summary>
        public FlyClient()
        {

        }

        /// <summary>
        /// FlyClient
        /// </summary>
        /// <param name="creator">创建指定协议的客户端</param>
        public FlyClient(IClientCreator creator)
        {

        }
    }
}
