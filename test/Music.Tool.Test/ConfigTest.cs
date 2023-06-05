namespace Music.Tool.Test
{
    public class ConfigTest : BaseTest
    {

        [Fact]
        public void Check()
        {
            Assert.NotNull(_config);
            Assert.True(_config.Platforms != null && _config.Platforms.Any());
            Assert.Equal(11, _config.Platforms.Count());
            var fivesing = _config[Config.FiveSing];
            Assert.NotNull(fivesing);
            Assert.Equal("5SING“Ù¿÷", fivesing.Name);
        }
    }
}