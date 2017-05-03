using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public interface IWaitService
    {
        Task Wait();
    }

    public class WaitService : IWaitService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ReinRausOptions _options;

        public WaitService(IOptions<ReinRausOptions> options, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _options = options.Value;
        }

        public Task Wait()
        {
            var logger = _loggerFactory.CreateLogger<WaitService>();
            logger.LogInformation("Waiting for {WaitTime}", _options.WaitTime);
            return Task.Delay(_options.WaitTime);
        }
    }
}