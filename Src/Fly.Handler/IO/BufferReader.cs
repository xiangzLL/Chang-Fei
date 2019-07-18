using System.Linq;
using System.Threading.Tasks;
using Fly.Handler.Extensions;

namespace Fly.Handler.IO
{
    public class BufferReader
    {
        /// <summary>
        /// Size >4M 被认为大buffer
        /// </summary>
        private const int LargeSize = 4194304;

        private readonly IClient _client;

        public BufferReader(IClient client)
        {
            _client = client;
        }

        public IBuffer ReadBuffer()
        {
            var bufferSize = _client.ReadInt();
            var buffer = _client.ReadBinary(bufferSize);
            var result = new ByteBuffer(buffer);

            var hashCode = _client.ReadBinary(16);
            if (!hashCode.SequenceEqual(result.HashCode))
            {
                throw new ErrorDataException("Hash error");
            }
            return result;
        }

        public async Task<IBuffer> ReadBufferAsync()
        {
            var bufferSize = await _client.ReadIntAsync();
            var buffer = await _client.ReadBinaryAsync(bufferSize);
            var result = new ByteBuffer(buffer);

            var hashCode = await _client.ReadBinaryAsync(16);
            if (!hashCode.SequenceEqual(result.HashCode))
            {
                throw new ErrorDataException("Hash error");
            }
            return result;
        }
    }
}
