namespace Music.Tool.Model
{
    public class KugouSongResult
    {
        public int status { get; set; }
        public int err_code { get; set; }
        public KugouSongResultData data { get; set; }
    }

    public class KugouSongResultData
    {
        public string hash { get; set; }
        public int timelength { get; set; }
        public int filesize { get; set; }
        public string audio_name { get; set; }
        public int have_album { get; set; }
        public string album_name { get; set; }
        public string album_id { get; set; }
        public string img { get; set; }
        public int have_mv { get; set; }
        public string video_id { get; set; }
        public string author_name { get; set; }
        public string song_name { get; set; }
        public string lyrics { get; set; }
        public string author_id { get; set; }
        public int privilege { get; set; }
        public string privilege2 { get; set; }
        public string play_url { get; set; }
        public KugouSongResultAuthor[] authors { get; set; }
        public int is_free_part { get; set; }
        public int bitrate { get; set; }
        public string recommend_album_id { get; set; }
        public string store_type { get; set; }
        public int album_audio_id { get; set; }
        public int is_publish { get; set; }
        public string e_author_id { get; set; }
        public string audio_id { get; set; }
        public bool has_privilege { get; set; }
        public string play_backup_url { get; set; }
        public KugouSongResultTrans_Param trans_param { get; set; }
        public int small_library_song { get; set; }
        public string encode_album_id { get; set; }
        public string encode_album_audio_id { get; set; }
        public string e_video_id { get; set; }
    }

    public class KugouSongResultTrans_Param
    {
        public KugouSongResultHash_Offset hash_offset { get; set; }
        public int musicpack_advance { get; set; }
        public int pay_block_tpl { get; set; }
        public int display { get; set; }
        public int display_rate { get; set; }
        public string appid_block { get; set; }
        public int cpy_grade { get; set; }
        public int cpy_level { get; set; }
        public int cid { get; set; }
        public int cpy_attr0 { get; set; }
        public Classmap classmap { get; set; }
        public string hash_multitrack { get; set; }
        public Qualitymap qualitymap { get; set; }
    }

    public class KugouSongResultHash_Offset
    {
        public int start_byte { get; set; }
        public int end_byte { get; set; }
        public int start_ms { get; set; }
        public int end_ms { get; set; }
        public string offset_hash { get; set; }
        public int file_type { get; set; }
        public string clip_hash { get; set; }
    }

    public class KugouSongResultAuthor
    {
        public string author_id { get; set; }
        public string author_name { get; set; }
        public string is_publish { get; set; }
        public string sizable_avatar { get; set; }
        public string e_author_id { get; set; }
        public string avatar { get; set; }
    }

}
