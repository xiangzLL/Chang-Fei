using System.IO;
using Fly.Handler.Channels;

namespace Fly.Handler
{
    public interface IClient
    {
        /// <summary>
        /// 发送超时时间
        /// </summary>
        int SendTimeout { get; set; }

        /// <summary>
        /// 读取超时时间
        /// </summary>
        int ReadTimeout { get; set; }

        /// <summary>
        /// Server信息
        /// </summary>
        HostInfo Remote { get; }

        /// <summary>
        /// Client信息
        /// </summary>
        HostInfo Local { get; }

        /// <summary>
        /// 读流
        /// </summary>
        Stream ReadStream { get; }

        /// <summary>
        /// 写流
        /// </summary>
        Stream WriteStream { get; }

        /// <summary>
        /// 连接状态
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// 断开连接
        /// </summary>
        void Disconnect();
    }
}
