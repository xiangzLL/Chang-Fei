using System;
using System.IO;
using System.Net.Sockets;

namespace Fly.Handler
{
    /// <summary>
    /// 网络连接异常
    /// </summary>
    public abstract class NetworkException:Exception
    {
        public string Name { get; protected set; }

        protected NetworkException(string message = "") : base(message)
        {

        }

        public static Exception Convert(Exception ex)
        {
            var ioException = ex as IOException;
            if (ioException?.InnerException is SocketException socketException)
            {
                if (socketException.SocketErrorCode == SocketError.NetworkReset ||
                    socketException.SocketErrorCode == SocketError.ConnectionAborted ||
                    socketException.SocketErrorCode == SocketError.ConnectionReset)
                {
                    return new ConnectionAbortException();
                }
                if (socketException.SocketErrorCode == SocketError.NotConnected ||
                    socketException.SocketErrorCode == SocketError.Shutdown)
                {
                    return new NotConnectedException();
                }
                if (socketException.SocketErrorCode == SocketError.ConnectionRefused ||
                    socketException.SocketErrorCode == SocketError.HostDown ||
                    socketException.SocketErrorCode == SocketError.HostUnreachable)
                {
                    return new CanNotConnectException();
                }
                if (socketException.SocketErrorCode == SocketError.TimedOut)
                {
                    return new ConnectionTimeoutException();
                }
            }

            return ex;
        }
    }

    /// <summary>
    /// 网络被挂起的异常
    /// </summary>
    public class ConnectionAbortException : NetworkException
    {
        public ConnectionAbortException()
        {
            Name = "ConnectionAbort";
        }
    }

    /// <summary>
    /// 网络未连接异常
    /// </summary>
    public class NotConnectedException : NetworkException
    {
        public NotConnectedException()
        {
            Name = "NotConnected";
        }
    }

    /// <summary>
    /// 网络连接失败异常
    /// </summary>
    public class CanNotConnectException : NetworkException
    {
        public CanNotConnectException()
        {
            Name = "CanNotConnect";
        }
    }

    /// <summary>
    /// 网络连接超时异常
    /// </summary>
    public class ConnectionTimeoutException : NetworkException
    {
        public ConnectionTimeoutException()
        {
            Name = "ConnectionTimeout";
        }
    }

    /// <summary>
    /// 数据错误异常
    /// </summary>
    public class ErrorDataException : NetworkException
    {
        public ErrorDataException(string message=""):base(message)
        {
            Name = "ErrorData";
        }
    }
}
