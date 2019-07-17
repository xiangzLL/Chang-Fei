using System;

namespace Fly.Handler.IO
{
    public class ByteBuffer:IBuffer
    {
        private readonly byte[] _data;

        public int Size => _data.Length;
        public byte[] HashCode { get; }

        public ByteBuffer(byte[] data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            HashCode = MD5.GetHash(_data);
        }

        public byte[] GetBytes()
        {
            var bytes = new byte[Size];
            Array.Copy(_data,0,bytes,0,Size);
            return bytes;
        }
    }
}
