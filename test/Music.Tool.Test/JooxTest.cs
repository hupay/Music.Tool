using Microsoft.Extensions.DependencyInjection;
using Music.Tool.Platform;
using System.Runtime.InteropServices;

namespace Music.Tool.Test
{
    public class JooxTest : BaseTest
    {
        private readonly Joox _joox;
        public JooxTest()
        {
            _joox = _provider.GetService<Joox>();
        }

        [Theory]
        [InlineData("Siempre Me Quedará")]
        public async Task SearchAsync(string keyword)
        {
            var result = await _joox.SearchAsync(keyword);
            Assert.NotNull(result);
            Assert.True(result.Songs.Any());
            Assert.Equal(keyword, result.Songs.First().Name);
        }
    }
}
