using Microsoft.Extensions.Logging;

namespace Fly.Logger
{
    public class LogHelper
    {
        private static ILogger _logger;

        public static void RegisterLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void LogTrace(string message,params object[] args)
        {
            _logger?.LogTrace(message,args);
        }

        public static void LogDebug(string message, params object[] args)
        {
            _logger?.LogDebug(message, args);
        }

        public static void LogError(string message, params object[] args)
        {
            _logger?.LogError(message, args);
        }
    }
}
