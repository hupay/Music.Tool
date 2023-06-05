using Microsoft.Extensions.Logging;
using Music.Tool.Model;
using Music.Tool.Util;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Music.Tool.Platform;

public class FiveSing : MusicPlatform
{
    public FiveSing(ILogger<FiveSing> logger, Config config) : base(logger, config)
    {

    }
    public override string Id => Config.FiveSing;

    public override SearchResult ParseSearchResult(string text)
    {
        _logger.LogInformation($"平台：{Platform.Name}   解析搜索结果。");
        Regex regex = new Regex(@"dataList = '(.*?)';");
        var match = regex.Match(text);
        if (match.Success)
        {
            var json = match.Groups[1].Value.Clear();

            var result = JsonSerializer.Deserialize<List<FiveSingSearchResult>>(json);
            _logger.LogWarning($"平台：{Platform.Name}   解析搜索结果成功。");
            return Convert(result);
        }
        _logger.LogWarning($"平台：{Platform.Name}   解析搜索结果失败。");
        return default;
    }

    public override SongInfo ParseSongInfo(string text)
    {
        _logger.LogInformation($"平台：{Platform.Name}   解析歌曲信息结果。");
        var result = JsonSerializer.Deserialize<FiveSingSongResult>(text);
        if (result != null && result.success)
        {
            _logger.LogWarning($"平台：{Platform.Name}   解析歌曲信息结果成功。");
            return Convert(result);
        }
        _logger.LogWarning($"平台：{Platform.Name}   解析歌曲信息结果失败。");
        return default;
    }

    private SearchResult Convert(List<FiveSingSearchResult> result)
    {
        var searchResult = new SearchResult()
        {
            Songs = result.Select(x => new SongInfo
            {
                Id = x.songId.ToString(),
                Name = x.songName,
                Artist = x.singer,
                PlayUrl = x.songurl,
                DownloadUrl = x.downloadurl,
                Platform = Platform.Name,
            })
        };
        _logger.LogDebug($"平台：{Platform.Name}   结果转换成功！");
        Parallel.ForEach(searchResult.Songs, async x =>
        {
            var songInfo = await GetSongAsync(new { songid = x.Id, songtype = x.DownloadUrl.Contains("yc") ? "yc" : "fc" });
            x.Urls = songInfo.Urls;
        });
        return searchResult;
    }

    private SongInfo Convert(FiveSingSongResult result)
    {
        var songInfo = new SongInfo()
        {
            Id = result.data.songid.ToString(),
            Name = result.data.songName,
            Artist = result.data.user?.NN,
            Album = null,
            Platform = Platform.Name,
            Urls = new List<SongUrl>()
            {
                new SongUrl()
                {
                    Type= Config.HighQuality,
                    Url=result.data.squrl??result.data.squrl_backup,
                    Size=(int.Parse(result.data.sqsize??"0")*1.0/1024/1024).ToString("F2")+"MB",
                },
                new SongUrl()
                {
                    Type=Config.StandardQuality,
                    Url=result.data.hqurl??result.data.hqurl_backup,
                    Size=(int.Parse(result.data.hqsize??"0")*1.0/1024/1024).ToString("F2")+"MB",
                },
                new SongUrl()
                {
                    Type=Config.FluentQuality,
                    Url=result.data.lqurl??result.data.lqurl_backup,
                    Size=(int.Parse(result.data.lqsize??"0")*1.0/1024/1024).ToString("F2")+"MB",
                },
            }
        };
        return songInfo;
    }
}