using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Music.Tool.Model;

namespace Music.Tool.Platform
{
    /// <summary>
    /// 检索存在问题
    /// </summary>
    public class Joox : MusicPlatform
    {
        public Joox(ILogger<MusicPlatform> logger, Config config) : base(logger, config)
        {
        }

        public override string Id => Config.JOOX;

        public override SearchResult ParseSearchResult(string text)
        {
            throw new NotImplementedException();
        }

        public override SongInfo ParseSongInfo(string text)
        {
            throw new NotImplementedException();
        }

        public override IFlurlRequest GetSearchRequest(string keyword)
        {
            return Platform.SearchUrl
                .SetQueryParams(new Dictionary<string, string>(){
                    { "country", "hk" },
                    {"lang", "zh_TW" },
                    {"search_input", keyword },
                    {"sin", "0" },
                    {"ein", Config.SearchNum.ToString() }
                })
                .WithHeaders(new Dictionary<string, string>()
                {
                    { "User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/605.1.15 (KHTML, like Gecko)" },
                    { "Cookie", "wmid=142420656; user_type=1; country=id; session_key=2a5d97d05dc8fe238150184eaf3519ad;" },
                    { "X-Forwarded-For", "36.73.34.109" }
                });
        }
    }
}
