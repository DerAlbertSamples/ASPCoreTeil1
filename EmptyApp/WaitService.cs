using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace EmptyApp
{
    public interface IWaitService
    {
        Task Wait();
    }

    public class WaitService : IWaitService
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