using System;
using System.Threading.Tasks;
using Fly.Handler.Extensions;

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
            var data = buffer.GetBytes();
            if (data.Length != buffer.Size)
            {
                throw new InvalidOperationException("Buffer data length and size are not matched.");
            }
            _client.WriteInt(data.Length);
            _client.WriteBinary(data);
            _client.WriteBinary(buffer.HashCode);
        }

        public async Task WriteBufferAsync(IBuffer buffer)
        {
            var data = buffer.GetBytes();
            if (data.Length != buffer.Size)
            {
                throw new InvalidOperationException("Buffer data length and size are not matched.");
            }
            await _client.WriteIntAsync(data.Length);
            await _client.WriteBinaryAsync(data);
            await _client.WriteBinaryAsync(buffer.HashCode);
        }
    }
}
