namespace Music.Tool.Model
{
    public class KuwoSearchResult
    {
        public int code { get; set; }
        public long curTime { get; set; }
        public KuwoSearchData data { get; set; }
        public string msg { get; set; }
        public string profileId { get; set; }
        public string reqId { get; set; }
        public string tId { get; set; }
    }

    public class KuwoSearchData
    {
        public string total { get; set; }
        public KuwoSearchList[] list { get; set; }
    }

    public class KuwoSearchList
    {
        public string musicrid { get; set; }
        public string barrage { get; set; }
        public string artist { get; set; }
        public Mvpayinfo mvpayinfo { get; set; }
        public string pic { get; set; }
        public int isstar { get; set; }
        public int rid { get; set; }
        public int duration { get; set; }
        public string score100 { get; set; }
        public string content_type { get; set; }
        public int track { get; set; }
        public bool hasLossless { get; set; }
        public int hasmv { get; set; }
        public string album { get; set; }
        public string albumid { get; set; }
        public string pay { get; set; }
        public int artistid { get; set; }
        public string albumpic { get; set; }
        public int originalsongtype { get; set; }
        public string songTimeMinutes { get; set; }
        public bool isListenFee { get; set; }
        public string pic120 { get; set; }
        public string name { get; set; }
        public int online { get; set; }
        public Payinfo payInfo { get; set; }
    }

    public class Mvpayinfo
    {
        public string play { get; set; }
        public string vid { get; set; }
        public string download { get; set; }
    }

    public class Payinfo
    {
        public string nplay { get; set; }
        public string play { get; set; }
        public string overseas_nplay { get; set; }
        public string local_encrypt { get; set; }
        public string limitfree { get; set; }
        public string refrain_start { get; set; }
        public Feetype feeType { get; set; }
        public string ndown { get; set; }
        public string download { get; set; }
        public string cannotDownload { get; set; }
        public string overseas_ndown { get; set; }
        public string cannotOnlinePlay { get; set; }
        public string listen_fragment { get; set; }
        public string refrain_end { get; set; }
        public Paytagindex paytagindex { get; set; }
        public string tips_intercept { get; set; }
    }

    public class Feetype
    {
        public string song { get; set; }
        public string album { get; set; }
        public string vip { get; set; }
        public string bookvip { get; set; }
    }

    public class Paytagindex
    {
        public int S { get; set; }
        public int F { get; set; }
        public int ZP { get; set; }
        public int H { get; set; }
        public int ZPGA201 { get; set; }
        public int ZPLY { get; set; }
        public int HR { get; set; }
        public int L { get; set; }
        public int ZPGA501 { get; set; }
        public int DB { get; set; }
        public int AR501 { get; set; }
    }
}
