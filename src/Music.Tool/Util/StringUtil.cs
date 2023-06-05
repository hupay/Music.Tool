namespace Music.Tool.Util
{
    public static class StringUtil
    {
        /// <summary>
        /// 清理字符串中的高亮标签等
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Clear(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default;
            }
            return str
                .Replace("""<em class=\\\"keyword\\\">""", string.Empty)
                .Replace("""<\\/em>""", string.Empty)
                .Replace("\\\"", "\"")
                .Replace("\\\\u", "\\u")
                .Replace("""\/""","/");
        }
    }
}
