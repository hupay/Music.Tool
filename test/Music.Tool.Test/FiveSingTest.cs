using Microsoft.Extensions.DependencyInjection;
using Music.Tool.Model;
using Music.Tool.Platform;
using Music.Tool.Util;
using System.Text.Json;

namespace Music.Tool.Test
{
    public class FiveSingTest : BaseTest
    {
        private readonly FiveSing _fivesing;
        public FiveSingTest() : base()
        {
            _fivesing = _provider.GetService<FiveSing>();
        }

        [Theory]
        [InlineData("阿刁")]
        public async Task SearchAsync(string keyword)
        {
            var result = await _fivesing.SearchAsync(keyword);
            Assert.NotNull(result);
            Assert.True(result.Songs.Any());
            Assert.Equal(keyword, result.Songs.First().Name);
        }

        /// <summary>
        /// 格式化json
        /// </summary>
        [Fact]
        public void ParseText()
        {
            var text = """
                [{\"createTime\":\"2018-04-30 07:04:22\",\"originSinger\":\"\\u8d75\\u96f7\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":403,\"style\":\"\\u6d41\\u884c|\\u6c11\\u8c23\",\"downloadCnt\":1140,\"playCnt\":54162,\"singer\":\"\\u7956\\u5a05\\u7eb3\\u60dc\",\"postProduction\":\"\",\"popularityCnt\":2192082,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16500407,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":12789595,\"nickName\":\"\\u7956\\u5a05\\u7eb3\\u60dc\",\"singerId\":3060435,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16500407.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16500407\",\"typeEname\":\"fc\"},{\"createTime\":\"2018-01-21 23:00:46\",\"originSinger\":\"\\u5f20\\u97f6\\u6db5\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":154,\"style\":\"\",\"downloadCnt\":3009,\"playCnt\":37971,\"singer\":\"Joonho\",\"postProduction\":\"\",\"popularityCnt\":1727735,\"songWriter\":\"\",\"composer\":\"\",\"songId\":3004008,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":12858680,\"nickName\":\"Joonho\",\"singerId\":247716,\"type\":3,\"typeName\":\"\\u4f34\\u594f\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/bz\\/3004008.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/bz\\/down\\/3004008\",\"typeEname\":\"bz\"},{\"createTime\":\"2018-09-02 23:36:52\",\"originSinger\":\"\\u8d75\\u96f7\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":85,\"style\":\"\\u6d41\\u884c\",\"downloadCnt\":295,\"playCnt\":25599,\"singer\":\"\\u7384\\u5b50\",\"postProduction\":\"\",\"popularityCnt\":993077,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16691465,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":0,\"nickName\":\"\\u7384\\u5b50_Vinchou\",\"singerId\":13761626,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16691465.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16691465\",\"typeEname\":\"fc\"},{\"createTime\":\"2018-01-21 21:43:35\",\"originSinger\":\"\\u5f20\\u97f6\\u6db5\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":76,\"style\":\"\\u6d41\\u884c|\",\"downloadCnt\":234,\"playCnt\":19881,\"singer\":\"\\u5927\\u96c4\",\"postProduction\":\"\",\"popularityCnt\":774557,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16356236,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":0,\"nickName\":\"p09o87i65\",\"singerId\":878749,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16356236.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16356236\",\"typeEname\":\"fc\"},{\"createTime\":\"2018-02-01 01:50:08\",\"originSinger\":\"\\u5f20\\u97f6\\u6db5\\/\\u8d75\\u96f7\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\\uff08\\u5f20\\u97f6\\u6db5\\u7248\\uff09\",\"status\":1,\"collectCnt\":183,\"style\":\"\",\"downloadCnt\":1068,\"playCnt\":14016,\"singer\":\"\\u7d20\\u654c\\u55b5\\u55b5\",\"postProduction\":\"\",\"popularityCnt\":655576,\"songWriter\":\"\",\"composer\":\"\",\"songId\":3007267,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":12858680,\"nickName\":\"\\u7d20\\u654c\\u55b5\\u55b5\",\"singerId\":14817812,\"type\":3,\"typeName\":\"\\u4f34\\u594f\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/bz\\/3007267.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/bz\\/down\\/3007267\",\"typeEname\":\"bz\"},{\"createTime\":\"2018-05-07 10:18:36\",\"originSinger\":\"\\u5f20\\u97f6\\u6db5\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":50,\"style\":\"\\u6d41\\u884c\",\"downloadCnt\":240,\"playCnt\":15642,\"singer\":\"\\u98ce\\u5c0f\\u7b5d\",\"postProduction\":\"\",\"popularityCnt\":613232,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16510830,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":12820942,\"nickName\":\"\\u98ce\\u5c0f\\u7b5d\\uff08\\u8983\\u6c90\\u66e6\\uff09\",\"singerId\":9332303,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16510830.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16510830\",\"typeEname\":\"fc\"},{\"createTime\":\"2018-08-17 00:08:25\",\"originSinger\":\"\\u8d75\\u96f7\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":58,\"style\":\"\\u6c11\\u8c23\",\"downloadCnt\":442,\"playCnt\":12355,\"singer\":\"Hz\\u5c0f\\u624b\",\"postProduction\":\"\",\"popularityCnt\":512313,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16665071,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":0,\"nickName\":\"Mobile_67665515\",\"singerId\":67665515,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16665071.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16665071\",\"typeEname\":\"fc\"},{\"createTime\":\"2018-07-24 21:32:44\",\"originSinger\":\"1\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":32,\"style\":\"\\u6d41\\u884c\",\"downloadCnt\":114,\"playCnt\":12584,\"singer\":\"\\u5c0f\\u8e66\",\"postProduction\":\"\",\"popularityCnt\":487950,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16628521,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":0,\"nickName\":\"\\u5c0f\\u8e66\\u97f3\\u4e50\\u53f0\",\"singerId\":65470495,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16628521.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16628521\",\"typeEname\":\"fc\"},{\"createTime\":\"2020-01-16 16:30:34\",\"originSinger\":\"\\u8d75\\u96f7\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\\uff08\\u8d75\\u96f7\\u539f\\u7248\\u4f34\\u594f\\uff09\",\"status\":1,\"collectCnt\":69,\"style\":\"\",\"downloadCnt\":797,\"playCnt\":10640,\"singer\":\"\\u5200\\u5200\\u5200\",\"postProduction\":\"\",\"popularityCnt\":483644,\"songWriter\":\"\",\"composer\":\"\",\"songId\":3249026,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":0,\"nickName\":\"\\u5200\\u5200\\u5200\",\"singerId\":856082,\"type\":3,\"typeName\":\"\\u4f34\\u594f\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/bz\\/3249026.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/bz\\/down\\/3249026\",\"typeEname\":\"bz\"},{\"createTime\":\"2018-09-14 22:17:08\",\"originSinger\":\"\\u5feb\\u4e50\",\"songName\":\"<em class=\\\"keyword\\\">\\u963f\\u5201<\\/em>\",\"status\":1,\"collectCnt\":18,\"style\":\"\\u6d41\\u884c\",\"downloadCnt\":40,\"playCnt\":12102,\"singer\":\"\\u738b\\u6653\\u5f64\",\"postProduction\":\"\",\"popularityCnt\":457086,\"songWriter\":\"\",\"composer\":\"\",\"songId\":16707862,\"optComposer\":\"\",\"ext\":\"mp3\",\"songSize\":12861693,\"nickName\":\"\\u7b2c\\u4e5d\\u5929\\u539f\\u521b\\u97f3\\u4e50\",\"singerId\":407166,\"type\":2,\"typeName\":\"\\u7ffb\\u5531\",\"songurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/16707862.html\",\"downloadurl\":\"http:\\/\\/5sing.kugou.com\\/fc\\/down\\/16707862\",\"typeEname\":\"fc\"}]
                """;
            var text2 = text.FivesingClear();
            // , new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) }
            var result = JsonSerializer.Deserialize<List<FiveSingSearchResult>>(text2);
            Assert.NotNull(result);
            Assert.Equal("阿刁", result.First().songName);
            Assert.NotNull(result);
        }

        [Fact]
        public void ParseSongInfo()
        {
            // http://service.5sing.kugou.com/song/getsongurl?songid=16500407&songtype=fc
            var text = """
                {"msg":"","code":0,"data":{"songid":16500407,"songtype":"fc","squrl":"https:\/\/wsaudiobssdlbig.yun.kugou.com\/202306031729\/40c714f48b45c3bc8206142544746f6e\/bss\/extname\/wsaudio\/9b3d20daa5c1c83f4da11d6d75f92fa1.mp3","squrl_backup":"https:\/\/wsaudiobssdlbig.cloud.kugou.com\/202306031729\/2564a122619202d3da324780bd718d48\/bss\/extname\/wsaudio\/9b3d20daa5c1c83f4da11d6d75f92fa1.mp3","squrlmd5":"9b3d20daa5c1c83f4da11d6d75f92fa1","sqsize":"12789595","sqext":"mp3","hqurl":"https:\/\/wsaudiobssdlbig.yun.kugou.com\/202306031729\/fd1775215ec93e4d2387851fe3e9db76\/bss\/extname\/wsaudio\/9fd6b33d1e0a8c8f55f4f2b92af1c5ad.mp3","hqurl_backup":"https:\/\/wsaudiobssdlbig.cloud.kugou.com\/202306031729\/950bc43a2aaed663be2c7c336cf40020\/bss\/extname\/wsaudio\/9fd6b33d1e0a8c8f55f4f2b92af1c5ad.mp3","hqurlmd5":"9fd6b33d1e0a8c8f55f4f2b92af1c5ad","hqsize":"7673774","hqext":"mp3","lqurl":"https:\/\/wsaudiobssdlbig.yun.kugou.com\/202306031729\/bd60a30d8db505f59b36cef8790ba94a\/bss\/extname\/wsaudio\/3c151a9096999bbd0da762f2e097d0bb.mp3","lqurl_backup":"https:\/\/wsaudiobssdlbig.cloud.kugou.com\/202306031729\/df484d10d3857a397f92262959416132\/bss\/extname\/wsaudio\/3c151a9096999bbd0da762f2e097d0bb.mp3","lqurlmd5":"3c151a9096999bbd0da762f2e097d0bb","lqsize":"5115864","lqext":"mp3","songName":"\u963f\u5201","songKind":2,"user":{"ID":3060435,"NN":"\u7956\u5a05\u7eb3\u60dc","I":"https:\/\/wsingbssdl.kugou.com\/fff319bbb54b3e3391790c7a326e91af.jpg"},"DigitalAlbumID":0},"success":true,"message":"\u6210\u529f"}
                """.FivesingClear();
            var result = JsonSerializer.Deserialize<FiveSingSongResult>(text);
            Assert.NotNull(result);
            Assert.Equal(0, result.code);
            Assert.Equal("fc", result.data.songtype);
            Assert.Equal("阿刁", result.data.songName);
            Assert.Equal(3060435, result.data.user.ID);
        }

        [Fact]
        public void FormatSize()
        {
            var data = (int.Parse("12789595" ?? "0") * 1.0 / 1024 / 1024).ToString("F2") + "MB";
            Assert.Equal("12.20MB", data);
        }

        [Fact]
        public async Task GetSongAsync()
        {
            var result = await _fivesing.GetSongAsync(new { songid = "16500407", songtype = "fc" });
            Assert.NotNull(result);
            Assert.Equal("阿刁", result.Name);
            Assert.Equal(3, result.Urls.Count());
        }

        [Fact]
        public async Task DownloadAsync()
        {
            var song = await _fivesing.GetSongAsync(new { songid = "16500407", songtype = "fc" });
            var url = song.Urls.First();
            var data = await _fivesing.DownloadAsync(url.Url);
            var file = song.Name + "." + url.Ext;
            File.WriteAllBytes(file, data);
            if (File.Exists(file))
            {
                Assert.True(true);
                File.Delete(file);
            }
            else Assert.True(false);
        }
    }
}
