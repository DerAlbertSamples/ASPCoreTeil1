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
        private readonly ILogger _logger;
        private readonly ReinRausOptions _options;

        public WaitService(IOptions<ReinRausOptions> options, ILogger<WaitService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public Task Wait()
        {
            using (_logger.BeginScope(new { text = "Ding" }))
            {
                _logger.LogDebug("Waiting for {WaitTime}", _options.WaitTime);
                _logger.LogInformation("Waiting for {WaitTime}", _options.WaitTime);
            }
            return Task.Delay(_options.WaitTime);
        }
    }
}