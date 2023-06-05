namespace Music.Tool.Model
{
    public class FiveSingSongResult
    {
        public string msg { get; set; }
        public int code { get; set; }
        public FiveSingSongInfo data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }

    /// <summary>
    /// sq 320k 高品质
    /// hq 192k 标准
    /// lq 128k 流畅
    /// </summary>
    public class FiveSingSongInfo
    {
        public int songid { get; set; }
        public string songtype { get; set; }
        public string squrl { get; set; }
        public string squrl_backup { get; set; }
        public string squrlmd5 { get; set; }
        public string sqsize { get; set; }
        public string sqext { get; set; }
        public string hqurl { get; set; }
        public string hqurl_backup { get; set; }
        public string hqurlmd5 { get; set; }
        public string hqsize { get; set; }
        public string hqext { get; set; }
        public string lqurl { get; set; }
        public string lqurl_backup { get; set; }
        public string lqurlmd5 { get; set; }
        public string lqsize { get; set; }
        public string lqext { get; set; }
        public string songName { get; set; }
        public int songKind { get; set; }
        public User user { get; set; }
        public int DigitalAlbumID { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string NN { get; set; }
        public string I { get; set; }
    }
}
