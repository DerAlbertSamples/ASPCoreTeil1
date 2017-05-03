using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public class ReinRausMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ReinRausOptions> _options;
        private readonly WaitService _waitService;

        public ReinRausMiddleware(RequestDelegate next, IOptions<ReinRausOptions> options, WaitService waitService)
        {
            _next = next;
            _options = options;
            _waitService = waitService;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{_options.Value.Nummer} rein<br />");
            await _waitService.Wait();
            await _next(context);
            await _waitService.Wait();
            await context.Response.WriteAsync($"{_options.Value.Nummer} raus<br />");
        }
    }
}