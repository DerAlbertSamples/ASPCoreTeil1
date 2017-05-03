using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public class ReinRausMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ReinRausOptions> _options;
        private readonly IWaitService _waitService;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<ReinRausMiddleware> _globalLogger;

        public ReinRausMiddleware(RequestDelegate next, IOptions<ReinRausOptions> options, IWaitService waitService, ILoggerFactory factory)
        {
            _next = next;
            _options = options;
            _waitService = waitService;
            _loggerFactory = factory;
            _globalLogger = _loggerFactory.CreateLogger<ReinRausMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().Contains(".ico"))
            {
                return;
            }
            _globalLogger.LogInformation("Global: Invoking for {Nummer}", _options.Value.Nummer);
            var logger = _loggerFactory.CreateLogger<ReinRausMiddleware>();
            logger.LogWarning("local: rein");
            await context.Response.WriteAsync($"{_options.Value.Nummer} rein<br />");
            await _waitService.Wait();
            await _next(context);
            await _waitService.Wait();
            logger.LogWarning("local: raus");
            await context.Response.WriteAsync($"{_options.Value.Nummer} raus<br />");
        }
    }
}