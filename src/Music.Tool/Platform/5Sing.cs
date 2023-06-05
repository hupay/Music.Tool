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
        _logger.LogInformation($"ƽ̨��{Platform.Name}   �������������");
        Regex regex = new Regex(@"dataList = '(.*?)';");
        var match = regex.Match(text);
        if (match.Success)
        {
            var json = match.Groups[1].Value.Clear();

            var result = JsonSerializer.Deserialize<List<FiveSingSearchResult>>(json);
            _logger.LogWarning($"ƽ̨��{Platform.Name}   ������������ɹ���");
            return Convert(result);
        }
        _logger.LogWarning($"ƽ̨��{Platform.Name}   �����������ʧ�ܡ�");
        return default;
    }

    public override SongInfo ParseSongInfo(string text)
    {
        _logger.LogInformation($"ƽ̨��{Platform.Name}   ����������Ϣ�����");
        var result = JsonSerializer.Deserialize<FiveSingSongResult>(text);
        if (result != null && result.success)
        {
            _logger.LogWarning($"ƽ̨��{Platform.Name}   ����������Ϣ����ɹ���");
            return Convert(result);
        }
        _logger.LogWarning($"ƽ̨��{Platform.Name}   ����������Ϣ���ʧ�ܡ�");
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
        _logger.LogDebug($"ƽ̨��{Platform.Name}   ���ת���ɹ���");
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