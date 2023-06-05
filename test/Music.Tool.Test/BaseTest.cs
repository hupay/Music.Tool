using Microsoft.Extensions.DependencyInjection;

namespace Music.Tool.Test
{
    public class BaseTest
    {
        protected readonly Config _config;
        protected readonly ServiceProvider _provider;
        public BaseTest()
        {
            var service = new ServiceCollection()
                .AddLogging()
                .AddMusic();

            _provider = service.BuildServiceProvider();
            _config = _provider.GetService<Config>();
        }
    }
}
