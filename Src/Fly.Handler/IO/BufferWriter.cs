using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Handler.IO
{
    public class BufferWriter
    {

        private readonly IClient _client;

        public BufferWriter(IClient client)
        {
            _client = client;
        }

        public void WriteBuffer(IBuffer buffer)
        {

        }

        public Task WriteBufferAsync(IBuffer buffer)
        {

        }

        private void Flush()
        {
            _client.WriteStream.Flush();
        }
    }
}
