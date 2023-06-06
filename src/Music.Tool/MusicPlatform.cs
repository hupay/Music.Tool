using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Music.Tool.Model;

namespace Music.Tool;

/// <summary>
/// 平台基类
/// </summary>
public abstract class MusicPlatform
{
    public abstract string Id { get; }
    protected readonly ILogger _logger;
    protected readonly MusicPlatformInfo Platform;
    public MusicPlatform(ILogger<MusicPlatform> logger, Config config)
    {
        _logger = logger;
        Platform = config[Id];
        if (Platform == null)
            throw new Exception($"平台[{Id}]不存在。");
        _logger.LogInformation($"加载平台[{Id}]成功。");
    }

    /// <summary>
    /// 平台名称
    /// </summary>
    public string Name => Platform.Name;

    /// <summary>
    /// 平台网址
    /// </summary>
    public string Url => Platform.Url;

    ///<summary>
    /// 搜索网址
    /// </summary>
    public virtual string SearchUrl => Platform.SearchUrl;

    ///<summary>
    ///  歌词网址
    /// </summary>
    public virtual string LyricUrl => Platform.LyricUrl;

    ///<summary>
    ///  歌曲网址
    /// </summary>
    public virtual string SongUrl => Platform.SongUrl;

    /// <summary>
    /// 文件下载
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public virtual async Task<byte[]> DownloadAsync(string url)
    {
        var bs = await url.GetBytesAsync();
        return bs;
    }
    /// <summary>
    /// 获取歌曲信息
    /// </summary>
    /// <param name="id">歌曲ID</param>
    /// <returns></returns>
    public virtual async Task<SongInfo> GetSongAsync(object param)
    {
        _logger.LogInformation($"平台：{Platform.Name}   开始获取歌曲信息。");
        try
        {
            var text = await Platform.SongUrl
                        .SetQueryParams(param)
                        .GetStringAsync();
            var song = ParseSongInfo(text);
            _logger.LogInformation($"平台：{Platform.Name}   结束获取歌曲信息。");
            return song;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"平台：{Platform.Name}   获取歌曲信息失败。");
            return default;
        }
    }

    /// <summary>
    /// 获取歌词
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual Task<string> GetSongLyricAsync(string id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <param name="page">页码</param>
    /// <param name="pageSize">页大小</param>
    /// <returns></returns>
    public virtual async Task<SearchResult> SearchAsync(string keyword)
    {
        try
        {
            _logger.LogInformation($"平台：{Platform.Name}   开始检索。");
            var text = await GetSearchRequest(keyword).GetStringAsync();
            var result = ParseSearchResult(text);
            _logger.LogInformation($"平台：{Platform.Name}   结束检索。");
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"平台：{Platform.Name}   检索失败。");
            return default;
        }
    }
    public virtual IFlurlRequest GetSearchRequest(string keyword)
    {
        return new FlurlRequest(Platform.SearchUrl + keyword);
    }

    /// <summary>
    /// 处理检索结果
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public abstract SearchResult ParseSearchResult(string text);
    /// <summary>
    /// 处理歌曲信息
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public abstract SongInfo ParseSongInfo(string text);
}
