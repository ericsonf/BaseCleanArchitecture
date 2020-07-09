using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using BaseCleanArchitecture.Infra.Logs;
using BaseCleanArchitecture.Core.Shared.Interfaces;

namespace BaseCleanArchitecture.API.Filters
{
    public class TrackPerformanceFilter : IActionFilter
    {
        private readonly ILogger<TrackPerformanceFilter> _logger;
        private Stopwatch _timer;
        private readonly IScopeInformation _scopeInformation;
        private IDisposable _userScope;
        private IDisposable _hostScope;

        public TrackPerformanceFilter(ILogger<TrackPerformanceFilter> logger, IScopeInformation scopeInformation)
        {
            _logger = logger;
            _scopeInformation = scopeInformation;  
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            if (context.Exception == null)
            {
                _logger.LogPerformance(context.HttpContext.Request.Method, context.HttpContext.Request.Path,
                    _timer.ElapsedMilliseconds);
            }

            _userScope?.Dispose();
            _hostScope?.Dispose();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = new Stopwatch();

            var dictionary = new Dictionary<string, string>
            {
                { "UserId", "Valor Usuario Id" },
                { "UserName", "Valor Usuario Name" }
            };

            _userScope = _logger.BeginScope(dictionary);
            _hostScope = _logger.BeginScope(_scopeInformation.HotScopeInfo);

            _timer.Start();  
        }
    }
}