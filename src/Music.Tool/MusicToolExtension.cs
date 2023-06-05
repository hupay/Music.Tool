using Microsoft.Extensions.Logging;
using Music.Tool;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MusicToolExtension
    {
        /// <summary>
        /// 引入音乐平台
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configPath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static IServiceCollection AddMusic(this IServiceCollection services, string configPath = "music.json")
        {
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException(configPath);
            }
            services.AddSingleton(provider =>
            {
                var logger = provider.GetService<ILogger<Config>>();
                return new Config(logger, configPath);
            });
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(MusicPlatform)));
            foreach (var type in types)
            {
                services.AddSingleton(type);
            }
            return services;
        }
    }
}
