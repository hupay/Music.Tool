using Microsoft.Extensions.DependencyInjection;
using Music.Tool.Model;
using Music.Tool.Platform;
using System.Text.Json;

namespace Music.Tool.Test
{
    public class KuwoTest : BaseTest
    {
        private readonly Kuwo _kuwo;
        public KuwoTest()
        {
            _kuwo = _provider.GetService<Kuwo>();
        }

        [Theory]
        [InlineData("阿刁")]
        public async Task SearchAsync(string keyword)
        {
            var result = await _kuwo.SearchAsync(keyword);
            Assert.NotNull(result);
            Assert.True(result.Songs.Any());
            Assert.Equal("阿刁 (Live)", result.Songs.First().Name);
            Assert.NotNull(result.Songs.FirstOrDefault());
        }

        [Theory]
        [InlineData("320kmp3")]
        [InlineData("192kmp3")]
        [InlineData("128kmp3")]
        public async Task GetSongAsync(string br)
        {
            var para = new
            {
                format = "mp3",
                br = br,
                rid = "40098317",
                type = "convert_url",
                response = "url",
            };
            var result = await _kuwo.GetSongAsync(para);
            Assert.NotNull(result);
            Assert.Equal("阿刁 (Live)", result.Name);
        }

        [Fact]
        public void ParseText()
        {
            var text = """
                {"code":200,"curTime":1686494144488,"data":{"total":"424","list":[{"musicrid":"MUSIC_40098317","barrage":"0","artist":"张韶涵","mvpayinfo":{"play":"0","vid":"8250381","download":"0"},"pic":"https://img4.kuwo.cn/star/albumcover/120/19/46/2144628227.jpg","isstar":0,"rid":40098317,"duration":321,"score100":"75","content_type":"0","track":0,"hasLossless":false,"hasmv":1,"album":"歌手第二季&nbsp;第2期","albumid":"5333408","pay":"16711935","artistid":492,"albumpic":"https://img4.kuwo.cn/star/albumcover/120/19/46/2144628227.jpg","originalsongtype":0,"songTimeMinutes":"05:21","isListenFee":true,"pic120":"https://img4.kuwo.cn/star/albumcover/120/19/46/2144628227.jpg","name":"阿刁","online":1,"payInfo":{"nplay":"111111111111","play":"1111","overseas_nplay":"11111","local_encrypt":"1","limitfree":"0","refrain_start":"0","feeType":{"song":"1","album":"0","vip":"1","bookvip":"0"},"ndown":"111111111111","download":"1111","cannotDownload":"0","overseas_ndown":"11111","cannotOnlinePlay":"0","listen_fragment":"1","refrain_end":"0","paytagindex":{"S":2,"F":3,"ZP":6,"H":1,"ZPGA201":9,"ZPLY":11,"HR":4,"L":0,"ZPGA501":10,"DB":7,"AR501":8},"tips_intercept":"0"}},{"musicrid":"MUSIC_16827758","barrage":"0","artist":"赵雷","mvpayinfo":{"play":"0","vid":"8047580","download":"0"},"pic":"https://img4.kuwo.cn/star/albumcover/120/50/55/2861943135.jpg","isstar":0,"rid":16827758,"duration":377,"score100":"70","content_type":"0","track":0,"hasLossless":false,"hasmv":1,"album":"无法长大","albumid":"1861237","pay":"16515324","artistid":89199,"albumpic":"https://img4.kuwo.cn/star/albumcover/120/50/55/2861943135.jpg","originalsongtype":1,"songTimeMinutes":"06:17","isListenFee":false,"pic120":"https://img4.kuwo.cn/star/albumcover/120/50/55/2861943135.jpg","name":"阿刁","online":1,"payInfo":{"nplay":"001111111111","play":"1100","overseas_nplay":"11111","local_encrypt":"1","limitfree":"0","refrain_start":"143000","feeType":{"song":"1","album":"0","vip":"1","bookvip":"0"},"ndown":"111111111111","download":"1111","cannotDownload":"0","overseas_ndown":"11111","cannotOnlinePlay":"0","listen_fragment":"0","refrain_end":"163000","paytagindex":{"S":2,"F":3,"ZP":6,"H":1,"ZPGA201":9,"ZPLY":11,"HR":4,"L":0,"ZPGA501":10,"DB":7,"AR501":8},"tips_intercept":"0"}},{"musicrid":"MUSIC_150356603","barrage":"0","artist":"美好时光音乐台","mvpayinfo":{"play":"0","vid":"0","download":"0"},"pic":"https://img1.kuwo.cn/star/starheads/120/39/1/3613643043.jpg","isstar":0,"rid":150356603,"duration":345,"score100":"51","content_type":"0","track":0,"hasLossless":false,"hasmv":0,"album":"","albumid":"0","pay":"8913032","artistid":3200371,"albumpic":"https://img1.kuwo.cn/star/starheads/120/39/1/3613643043.jpg","originalsongtype":0,"songTimeMinutes":"05:45","isListenFee":false,"pic120":"https://img1.kuwo.cn/star/starheads/120/39/1/3613643043.jpg","name":"阿刁","online":1,"payInfo":{"nplay":"000111111111","play":"1000","overseas_nplay":"000000000000","local_encrypt":"0","limitfree":"0","refrain_start":"62503","feeType":{"song":"0","album":"0","vip":"1","bookvip":"0"},"ndown":"000111111111","download":"1000","cannotDownload":"0","overseas_ndown":"000000000000","cannotOnlinePlay":"0","listen_fragment":"0","refrain_end":"97983","paytagindex":{"S":2,"F":3,"ZP":6,"H":1,"ZPGA201":9,"ZPLY":11,"HR":4,"L":0,"ZPGA501":10,"DB":7,"AR501":8},"tips_intercept":"0"}},{"musicrid":"MUSIC_188797015","barrage":"0","artist":"赵雷&张韶涵","mvpayinfo":{"play":"0","vid":"0","download":"0"},"pic":"https://img1.kuwo.cn/star/starheads/120/5/40/40754138.jpg","isstar":0,"rid":188797015,"duration":46,"score100":"52","content_type":"0","track":0,"hasLossless":false,"hasmv":0,"album":"","albumid":"0","pay":"8913032","artistid":89199,"albumpic":"https://img1.kuwo.cn/star/starheads/120/5/40/40754138.jpg","originalsongtype":0,"songTimeMinutes":"00:46","isListenFee":false,"pic120":"https://img1.kuwo.cn/star/starheads/120/5/40/40754138.jpg","name":"阿刁","online":1,"payInfo":{"nplay":"000111111111","play":"1000","overseas_nplay":"000000000000","local_encrypt":"0","limitfree":"0","refrain_start":"0","feeType":{"song":"0","album":"0","vip":"1","bookvip":"0"},"ndown":"000111111111","download":"1000","cannotDownload":"0","overseas_ndown":"000000000000","cannotOnlinePlay":"0","listen_fragment":"0","refrain_end":"0","paytagindex":{"S":2,"F":3,"ZP":6,"H":1,"ZPGA201":9,"ZPLY":11,"HR":4,"L":0,"ZPGA501":10,"DB":7,"AR501":8},"tips_intercept":"0"}},{"musicrid":"MUSIC_44381432","barrage":"0","artist":"张韶涵","mvpayinfo":{"play":"0","vid":"0","download":"0"},"pic":"https://img4.kuwo.cn/star/albumcover/120/85/99/3448140846.jpg","isstar":0,"rid":44381432,"duration":466,"score100":"49","content_type":"0","track":0,"hasLossless":false,"hasmv":0,"album":"庸人自扰男孩全私货热播女神最爱车载串烧","albumid":"5526975","pay":"8913032","artistid":492,"albumpic":"https://img4.kuwo.cn/star/albumcover/120/85/99/3448140846.jpg","originalsongtype":0,"songTimeMinutes":"07:46","isListenFee":false,"pic120":"https://img4.kuwo.cn/star/albumcover/120/85/99/3448140846.jpg","name":"阿刁(Dj培仔&nbsp;Remix&nbsp;2018)","online":1,"payInfo":{"nplay":"000111111111","play":"1000","overseas_nplay":"000000000000","local_encrypt":"0","limitfree":"0","refrain_start":"0","feeType":{"song":"0","album":"0","vip":"1","bookvip":"0"},"ndown":"000111111111","download":"1000","cannotDownload":"0","overseas_ndown":"000000000000","cannotOnlinePlay":"0","listen_fragment":"0","refrain_end":"0","paytagindex":{"S":2,"F":3,"ZP":6,"H":1,"ZPGA201":9,"ZPLY":11,"HR":4,"L":0,"ZPGA501":10,"DB":7,"AR501":8},"tips_intercept":"0"}}]},"msg":"success","profileId":"site","reqId":"69271e7307b7cffcf720904146cf0bd2","tId":""}
                """;

            var result = JsonSerializer.Deserialize<KuwoSearchResult>(text);
            Assert.NotNull(result);
            Assert.Equal(200, result.code);
            Assert.Equal(1686494144488, result.curTime);
            Assert.NotNull(result.data);
            Assert.NotNull(result.data.list);
            Assert.Equal("MUSIC_40098317", result.data.list[0].musicrid);
            Assert.Equal("张韶涵", result.data.list[0].artist);
            Assert.Equal("阿刁", result.data.list[0].name);
        }
    }
}
