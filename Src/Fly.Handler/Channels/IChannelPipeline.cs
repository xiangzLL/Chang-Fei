using System.Collections.Generic;

namespace Fly.Handler.Channels
{
    /// <summary>
    /// Channel管道
    /// </summary>
    public interface IChannelPipeline:IEnumerable<IChannelHandler>
    {
    }
}
