using System;
using Fly.Handler.IO;

namespace Fly.Handler.Channels
{
    public class BufferEventArgs:EventArgs
    {
        public IBuffer Buffer { get; }

        public BufferEventArgs(IBuffer buffer)
        {
            Buffer = buffer;
        }
    }
}
