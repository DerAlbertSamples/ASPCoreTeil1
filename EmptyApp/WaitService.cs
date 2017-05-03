using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public class WaitService
    {
        private ReinRausOptions _options;

        public WaitService(IOptions<ReinRausOptions> options)
        {
            _options = options.Value;
        }

        public Task Wait()
        {
            return Task.Delay(_options.WaitTime);
        }
    }
}