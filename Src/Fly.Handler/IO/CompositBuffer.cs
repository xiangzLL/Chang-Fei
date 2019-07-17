using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fly.Handler.Utilities;

namespace Fly.Handler.IO
{
    /// <summary>
    /// 适合数据比较大的时候使用
    /// </summary>
    public class CompositBuffer:IBuffer
    {
        private readonly IList<IBuffer> _buffers = new List<IBuffer>();

        public IBuffer[] Buffers => _buffers.ToArray();

        public int Size => _buffers.Sum(x=>x.Size);

        public virtual byte[] HashCode
        {
            get
            {
                using (var stream = new MemoryStream())
                {
                    foreach (var buffer in Buffers)
                    {
                        var hashCode = buffer.HashCode;
                        stream.Write(hashCode, 0, hashCode.Length);
                    }
                    return MD5.GetHash(stream.ToArray());
                }
            }
        }

        public virtual void Add(IBuffer buffer)
        {
            _buffers.Add(buffer);
        }

        /// <summary>
        /// 不要执行这个函数，会导致内存问题
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            throw new InvalidOperationException("Do not call this API, this could casue memory issue.");
        }

        public override string ToString()
        {
            return $"CompositBuffer, Count:{_buffers.Count} Size:{Size}";
        }
    }
}
