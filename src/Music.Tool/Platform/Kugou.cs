using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Music.Tool.Model;
using Music.Tool.Util;
using System.Text.Json;

namespace Music.Tool.Platform
{
    public class Kugou : MusicPlatform
    {
        public Kugou(ILogger<MusicPlatform> logger, Config config) : base(logger, config)
        {
        }

        public override string Id => Config.KuGou;

        public override SearchResult ParseSearchResult(string text)
        {
            _logger.LogInformation($"平台：{Platform.Name}   解析搜索结果。");
            var result = JsonSerializer.Deserialize<KugouSearchResult>(text.KugouClear());
            if (result?.data?.lists?.Any() ?? false)
            {
                _logger.LogWarning($"平台：{Platform.Name}   解析搜索结果成功。");
                return Convert(result.data.lists);
            }
            _logger.LogWarning($"平台：{Platform.Name}   解析搜索结果失败。");
            return default;
        }

        public override SongInfo ParseSongInfo(string text)
        {
            _logger.LogInformation($"平台：{Platform.Name}   解析歌曲信息结果。");
            var result = JsonSerializer.Deserialize<KugouSongResult>(text);
            if (result != null && result.status == 1 && result.err_code == 0)
            {
                _logger.LogWarning($"平台：{Platform.Name}   解析歌曲信息结果成功。");
                return Convert(result);
            }
            _logger.LogWarning($"平台：{Platform.Name}   解析歌曲信息结果失败。");
            return default;
        }

        public override IFlurlRequest GetSearchRequest(string keyword)
        {
            return Platform.SearchUrl
                .SetQueryParams(new Dictionary<string, string>(){
                    {"keyword", keyword },
                    {"page", "1" },
                    {"pagesize", Config.SearchNum.ToString() },
                    {"userid", "-1" },
                    {"clientver", string.Empty },
                    {"platform", "WebFilter" },
                    {"tag", "en" },
                    {"filter", string.Empty },
                    {"iscorrection", "1" },
                    {"privilege_filter", "0" },
                    {"_", DateTime.Now.Millisecond.ToString() }
                })
                .WithHeaders(new Dictionary<string, string>()
                {
                    { "User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/605.1.15 (KHTML, like Gecko)" },
                    { "Referers", "https://www.kugou.com/yy/html/search.html" },
                });
        }

        public override IFlurlRequest GetSongRequest(object param)
        {
            return new FlurlRequest(Platform.SongUrl)
                .SetQueryParams(param)
                .WithHeaders(new Dictionary<string, string>()
                {
                    { "User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_5) AppleWebKit/605.1.15 (KHTML, like Gecko)" },
                    { "Referers", "https://www.kugou.com/song/" },
                });
        }

        private SearchResult Convert(List[] result)
        {
            var searchResult = new SearchResult()
            {
                Songs = result.Select(x => new SongInfo()
                {
                    Id = x.ID,
                    Name = x.SongName,
                    Artist = x.SingerName,
                    Album = x.AlbumName,
                    Duration = TimeSpan.FromSeconds(x.Duration),
                    //PlayUrl = item.songurl,
                    //DownloadUrl = item.pl,
                    Platform = Platform.Name,
                    ExtraData = new Dictionary<string, string>
                    {
                        {"hash",x.FileHash },
                        {"album_id",x.AlbumID }
                    }
                })
            };
            _logger.LogDebug($"平台：{Platform.Name}   结果转换成功！");
            Parallel.ForEach(searchResult.Songs, async x =>
            {
                var param = new
                {
                    r = "play/getdata",
                    hash = x.ExtraData["hash"],
                    album_id = x.ExtraData["album_id"],
                    dfid = "1aAcF31Utj2l0ZzFPO0Yjss0",
                    mid = "ccbb9592c3177be2f3977ff292e0f145",
                    platid = "4",
                    _ = DateTime.Now.Millisecond.ToString()
                };
                var songInfo = await GetSongAsync(param);
                x.Urls = songInfo.Urls;
            });
            return searchResult;
        }


        private SongInfo Convert(KugouSongResult result)
        {
            var songInfo = new SongInfo()
            {
                Id = result.data.album_audio_id.ToString(),
                Name = result.data.song_name,
                Artist = result.data.author_name,
                Album = result.data.album_name,
                Platform = Platform.Name,
                Lyric = result.data.lyrics,
                Urls = new List<SongUrl>()
                {
                    new SongUrl()
                    {
                        Type = Config.HighQuality,
                        Url = result.data.play_url??result.data.play_backup_url,
                        Size = (result.data.filesize*1.0/1024/1024).ToString("F2")+"MB",
                        Ext = result.data.play_url.Split(".").Last(),
                    },
                }
            };
            return songInfo;
        }
    }
}
