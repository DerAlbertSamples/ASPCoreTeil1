using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmptyApp
{
    public class ReinRausMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _nummer;

        public ReinRausMiddleware(RequestDelegate next, string nummer)
        {
            _next = next;
            _nummer = nummer;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync($"{_nummer} ein<br />");
            await Task.Delay(TimeSpan.FromSeconds(1));
            await _next(context);
            await Task.Delay(TimeSpan.FromSeconds(1));
            await context.Response.WriteAsync($"{_nummer} raus<br />");
        }
    }
}