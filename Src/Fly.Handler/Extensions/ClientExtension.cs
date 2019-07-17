using System;
using System.IO;
using System.Text;

namespace Fly.Handler.Extensions
{
    /// <summary>
    /// 客户端发送扩展方法
    /// </summary>
    public static class ClientExtension
    {
        private const int MaxReadLength = 500000000; //500M

        public static void WriteString(this IClient client, string value)
        {
            var writeData = Encoding.Unicode.GetBytes(value);
            var dataLength = writeData.Length;
            WriteInt(client, dataLength);
            WriteBinary(client, writeData);
        }

        public static string ReadString(this IClient client)
        {
            var dataLength = ReadInt(client);
            var readData = Encoding.Unicode.GetString(ReadBinary(client, dataLength), 0, dataLength);
            return readData;
        }

        public static void WriteInt(this IClient client, int value)
        {
            var writeData = BitConverter.GetBytes(value);
            WriteBinary(client, writeData);
        }

        public static int ReadInt(this IClient client)
        {
            var readData = BitConverter.ToInt32(ReadBinary(client, sizeof(int)), 0);
            if (readData > MaxReadLength)
            {
                throw new InvalidDataException("Exceed max read length limit");
            }
            return readData;
        }

        public static void WriteShort(this IClient client, short value)
        {
            var writeData = BitConverter.GetBytes(value);
            WriteBinary(client, writeData);
        }

        public static short ReadShort(this IClient client)
        {
            var readData = BitConverter.ToInt16(ReadBinary(client, sizeof(short)), 0);
            return readData;
        }

        public static void WriteBinary(this IClient client, byte[] data)
        {
            var stream = client.WriteStream;
            stream.Write(data,0,data.Length);
            stream.Flush();
        }

        public static byte[] ReadBinary(this IClient client, int size)
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

                bytesRead = stream.Read(data, startPosition, dataLength);
                if (bytesRead == 0)
                {
                    throw new ConnectionAbortException();
                }

                if (bytesRead != dataLength)
                {
                    startPosition += bytesRead;
                }
            } while (bytesRead != dataLength);
            stream.Flush();
            return data;
        }
    }
}
