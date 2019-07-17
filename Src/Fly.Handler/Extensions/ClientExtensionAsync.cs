using System;
using System.Text;
using System.Threading.Tasks;

namespace Fly.Handler.Extensions
{
    /// <summary>
    /// 客户端发送异步扩展方法
    /// </summary>
    public static class ClientExtensionAsync
    {
        public static async Task WriteStringAsync(this IClient client, string value)
        {
            var writeData = Encoding.Unicode.GetBytes(value);
            var dataLength = writeData.Length;
            await WriteIntAsync(client, dataLength).ConfigureAwait(false);
            await WriteBinaryAsync(client, writeData).ConfigureAwait(false);
        }

        public static async Task<string> ReadStringAsync(this IClient client)
        {
            var dataLength = await ReadIntAsync(client).ConfigureAwait(false);
            var readData = await ReadBinaryAsync(client, dataLength).ConfigureAwait(false);
            var result = Encoding.Unicode.GetString(readData, 0, dataLength);
            return result;
        }

        public static async Task WriteIntAsync(this IClient client, int value)
        {
            var writeData = BitConverter.GetBytes(value);
            await WriteBinaryAsync(client, writeData).ConfigureAwait(false);
        }

        public static async Task<int> ReadIntAsync(this IClient client)
        {
            var readData = await ReadBinaryAsync(client, sizeof(int)).ConfigureAwait(false);
            var result = BitConverter.ToInt32(readData, 0);
            return result;
        }

        public static async Task WriteShortAsync(this IClient client, short value)
        {
            var writeData = BitConverter.GetBytes(value);
            await WriteBinaryAsync(client, writeData).ConfigureAwait(false);
        }

        public static async Task<short> ReadShortAsync(this IClient client)
        {
            var readData = await ReadBinaryAsync(client, sizeof(short)).ConfigureAwait(false);
            var result = BitConverter.ToInt16(readData, 0);
            return result;
        }

        public static async Task WriteBinaryAsync(this IClient client, byte[] data)
        {
            var stream = client.WriteStream;
            await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
            stream.Flush();
        }

        public static async Task<byte[]> ReadBinaryAsync(this IClient client, int size)
        {
            var stream = client.ReadStream;
            if (!stream.CanRead)
            {
                throw new ConnectionAbortException();
            }
            var data = new byte[size];
            var dataLength = data.Length;
            var startPosition = 0;
            var bytesRead = 0;
            do
            {
                dataLength -= bytesRead;
                if (dataLength == 0)
                {
                    break;
                }
                bytesRead = await stream.ReadAsync(data, startPosition, dataLength).ConfigureAwait(false);
                if (bytesRead == 0)
                {
                    throw new ConnectionAbortException();
                }
                if (bytesRead != dataLength)
                {
                    startPosition += bytesRead;
                }

            } while (bytesRead != dataLength);
            return data;
        }
    }
}
