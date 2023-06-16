using System.Text.Json;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Music.Tool.Model;

namespace Music.Tool.Platform
{
    public class Kuwo : MusicPlatform
    {
        private readonly Dictionary<string, string> _Header = new Dictionary<string, string>()
        {
            {"User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36"},
            {"Accept", "*/*"},
            {"Accept-Encoding", "gzip,deflate" },
            {"Accept-Language", "zh-CN,zh;q=0.8,gl;q=0.6,zh-TW;q=0.4"},
            {"Host", "kuwo.cn" },
            {"Referer", "http://kuwo.cn" },
        };
        public Kuwo(ILogger<MusicPlatform> logger, Config config) : base(logger, config)
        {
        }

        public override string Id => Config.KuWo;

        public override SearchResult ParseSearchResult(string text)
        {
            var result = JsonSerializer.Deserialize<KuwoSearchResult>(text);
            if (result?.data?.list?.Any() ?? false)
            {
                return Convert(result.data.list);
            }
            return default;
        }

        public override SongInfo ParseSongInfo(string text)
        {
            throw new NotImplementedException();
        }

        public override IFlurlRequest GetSearchRequest(string keyword)
        {
            var check = GetCookie();
            return Platform.SearchUrl
                .SetQueryParams(new Dictionary<string, string>(){
                    {"key", keyword },
                    {"pn", "1" },
                    {"rn", Config.SearchNum.ToString() },
                })
                .WithHeaders(_Header)
                .WithHeader("csrf", check.Item2.Value)
                .WithCookies(check.Item1);
        }

        public override IFlurlRequest GetSongRequest(object param)
        {
            var check = GetCookie();
            return new FlurlRequest(Platform.SongUrl)
                .SetQueryParams(param)
                .WithHeaders(_Header)
                .WithHeader("csrf", check.Item2.Value)
                .WithCookies(check.Item1);
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <returns></returns>
        private (IReadOnlyList<FlurlCookie>, FlurlCookie) GetCookie()
        {
            var response = "http://kuwo.cn/search/list?key=hello"
                .WithHeaders(_Header)
                .GetAsync()
                .Result;

            var cookie = response.Cookies;
            var token = cookie.FirstOrDefault(x => x.Name == "kw_token");
            return (cookie, token);
        }

        /// <summary>
        /// 实体转换
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private SearchResult Convert(KuwoSearchList[] list)
        {
            var searchResult = new SearchResult()
            {
                Songs = list.Select(x => new SongInfo()
                {
                    Id = x.rid.ToString(),
                    Name = x.name,
                    Artist = x.artist,
                    Album = x.album,
                    Duration = TimeSpan.Parse(x.songTimeMinutes),
                    //PlayUrl = item.songurl,
                    //DownloadUrl = item.pl,
                    Platform = Platform.Name,
                    ExtraData = new Dictionary<string, string>
                    {
                        // {"hash",x.FileHash },
                        // {"album_id",x.AlbumID }
                    }
                })
            };
            _logger.LogDebug("平台：{Platform.Name}   结果转换成功！", Platform.Name);
            Parallel.ForEach(searchResult.Songs, async x =>
            {
                var para = new
                {
                    format = "mp3",
                    br = "320kmp3",
                    rid = x.Id,
                    type = "convert_url",
                    response = "url",
                };
                var songInfo = await GetSongAsync(para);
                x.Urls = songInfo.Urls;
            });
            return searchResult;
        }
    }
}
