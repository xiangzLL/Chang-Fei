using System;
using Microsoft.Extensions.Logging;

namespace Fly.Logger
{
    public class FlyLoggerProvider:ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FlyLogger();
        }

        public void Dispose()
        {
        }
    }
}
