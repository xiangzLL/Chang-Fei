using System;
using System.Net;
using Fly.Handler.Channels;
using Fly.Handler.IO;

namespace Fly.Handler
{
    /// <summary>
    /// 客户端入口
    /// </summary>
    public class Bootstrap
    {
        private ChannelContext _channelContext;

        public void Connect(IPEndPoint )
        {

            _channelContext.InputChannel.BufferReceived += OnBufferReceived;
            _channelContext.InputChannel.StartReceive();
        }

        private void OnBufferReceived(object sender, BufferEventArgs e)
        {
            Console.WriteLine("客户端收到一条消息");
        }

        public void SendAsync(IBuffer buffer)
        {
            _channelContext.OutputChannel.Send();
        }
    }
}
