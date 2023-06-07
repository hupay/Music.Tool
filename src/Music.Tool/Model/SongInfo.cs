namespace Music.Tool.Model;

public class SongInfo
{
    public SongInfo()
    {
        ExtraData = new Dictionary<string, string>();
    }
    /// <summary>
    /// 歌曲ID
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 歌曲名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 歌手
    /// </summary>
    public string Artist { get; set; }
    /// <summary>
    /// 专辑
    /// </summary>
    public string Album { get; set; }
    /// <summary>
    /// 专辑ID
    /// </summary>
    public string AlbumId { get; set; }
    /// <summary>
    /// 歌曲时长
    /// </summary>
    public TimeSpan Duration { get; set; }
    /// <summary>
    /// 歌曲图片
    /// </summary>
    public string Picture { get; set; }
    /// <summary>
    /// 歌词
    /// </summary>
    public string Lyric { get; set; }
    /// <summary>
    /// 歌曲来源平台
    /// </summary>
    public string Platform { get; set; }

    public string PlayUrl { get; set; }

    public string DownloadUrl { get; set; }

    public Dictionary<string, string> ExtraData { get; set; }

    public IEnumerable<SongUrl> Urls { get; set; }
}

public class SongUrl
{
    public string Url { get; set; }
    public string Type { get; set; }
    /// <summary>
    /// mb
    /// </summary>
    public string Size { get; set; }
    public string Ext { get; set; }
}