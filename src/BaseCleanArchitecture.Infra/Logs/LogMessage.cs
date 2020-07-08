using System;
using Microsoft.Extensions.Logging;

namespace BaseCleanArchitecture.Infra.Logs
{
    public static class LogMessage
    {
        private static readonly Action<ILogger, string, string, long, Exception> _trackPerformance;

        static LogMessage()
        {
            _trackPerformance = LoggerMessage.Define<string, string, long>(LogLevel.Information, 0, "{Method} - The {RouteName} took {ElapsedMilliseconds} milliseconds.");
        }

        public static void LogPerformance(this ILogger logger, string method, string routeName, long elapsedMilliseconds)
        {
            _trackPerformance(logger, method, routeName, elapsedMilliseconds, null);
        }
    }
}