using System.Threading.Tasks;
using Fly.Handler.IO;

namespace Fly.Handler.Channels
{
    public interface IChannel
    {
        int Id { get; }

        bool IsClosed { get; set; }

        /// <summary>
        /// 远端信息，对server来说，这个意味着客户端，对client来说，这个意味是服务端
        /// </summary>
        HostInfo Remote { get; }
        /// <summary>
        /// 本端信息，对server来着，这个意味着服务端，对client来说，这个意味着客户端
        /// </summary>
        HostInfo Local { get; }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <returns></returns>
        Task WriteAsync(IBuffer buffer);

         void Test();
    }
}
