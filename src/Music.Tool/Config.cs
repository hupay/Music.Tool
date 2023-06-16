using Microsoft.Extensions.Logging;
using Music.Tool.Model;
using System.Text.Json;

namespace Music.Tool;

public class Config
{
    /// <summary>
    /// 5SING音乐下载: http://5sing.kugou.com/
    /// </summary>
    public const string FiveSing = "5sing";
    /// <summary>
    /// JOOX音乐下载: https://www.joox.com/cn/login
    /// </summary>
    public const string JOOX = "joox";
    /// <summary>
    /// 酷狗音乐下载: http://www.kugou.com/
    /// </summary>
    public const string KuGou = "kugou";
    /// <summary>
    /// 酷我音乐下载: http://www.kuwo.cn/
    /// </summary>
    public const string KuWo = "kuwo";
    /// <summary>
    /// 荔枝FM下载: http://m.lizhi.fm
    /// </summary>
    public const string LiZhi = "lizhi";
    /// <summary>
    /// 咪咕音乐下载: http://www.migu.cn/
    /// </summary>
    public const string MiGu = "migu";
    /// <summary>
    /// 网易云音乐下载: https://music.163.com/
    /// </summary>
    public const string Netease = "netease";
    /// <summary>
    /// 千千音乐下载: http://music.taihe.com/
    /// </summary>
    public const string QianQian = "";
    /// <summary>
    /// qq音乐下载: https://y.qq.com/
    /// </summary>
    public const string QQMusic = "qqmusic";
    /// <summary>
    /// 喜马拉雅下载: https://www.ximalaya.com/
    /// </summary>
    public const string XiMaLaYa = "ximalaya";
    /// <summary>
    /// 一听音乐下载: https://h5.1ting.com/
    /// </summary>
    public const string FirstTing = "1ting";

    /// <summary>
    /// 高品质音乐
    /// </summary>
    public const string HighQuality = "sq";
    /// <summary>
    /// 标准音质音乐
    /// </summary>
    public const string StandardQuality = "hq";
    /// <summary>
    /// 流畅音质音乐
    /// </summary>
    public const string FluentQuality = "lq";

    private readonly ILogger _logger;
    public Config(ILogger<Config> logger, string path)
    {
        // 加载数据。
        if (!File.Exists(path))
            throw new FileNotFoundException("配置文件不存在。", path);

        var str = File.ReadAllText(path);
        Platforms = JsonSerializer.Deserialize<List<MusicPlatformInfo>>(str);
        if (Platforms == null || !Platforms.Any())
            throw new NullReferenceException(nameof(Platforms));
        _logger = logger;
        _logger.LogInformation("加载配置文件成功。");
    }

    /// <summary>
    /// 检索数量
    /// </summary>
    public const int SearchNum = 5;

    public List<MusicPlatformInfo> Platforms { get; private set; }
    /// <summary>
    /// 读取指定名称的平台。
    /// </summary>
    /// <value></value>
    public MusicPlatformInfo? this[string id]
    {
        get
        {
            return Platforms.FirstOrDefault(p => p.Id == id);
        }
    }
}