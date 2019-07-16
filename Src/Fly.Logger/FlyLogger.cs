using System;
using Microsoft.Extensions.Logging;

namespace Fly.Logger
{
    /// <summary>
    /// FlyLogger 发送消息到队列
    /// </summary>
    public class FlyLogger:ILogger
    {
        public FlyLogger()
        {
            
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            //通过eventBus 写到rabbitMQ
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
