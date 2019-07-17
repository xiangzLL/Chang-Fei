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

        }

        public IBuffer ReadBufferAsync()
        {

        }
    }
}
