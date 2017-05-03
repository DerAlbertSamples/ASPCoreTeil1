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

        public ReinRausMiddleware(RequestDelegate next, IOptions<ReinRausOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{_options.Value.Nummer} rein<br />");
            await Task.Delay(_options.Value.WaitTime);
            await _next(context);
            await Task.Delay(_options.Value.WaitTime);
            await context.Response.WriteAsync($"{_options.Value.Nummer} raus<br />");
        }
    }
}