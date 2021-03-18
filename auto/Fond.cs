public class CFont
{
    private static string tcvntouni(string text)
    {
        switch (text)
        {
            case "µ":
                return "à";
            case "\u00b8":
                return "á";
            case "¶":
                return "ả";
            case "·":
                return "ã";
            case "¹":
                return "ạ";
            case "\u00a8":
                return "ă";
            case "»":
                return "ằ";
            case "¾":
                return "ắ";
            case "¼":
                return "ẳ";
            case "½":
                return "ẵ";
            case "Æ":
                return "ặ";
            case "©":
                return "â";
            case "Ç":
                return "ầ";
            case "Ê":
                return "ấ";
            case "È":
                return "ẩ";
            case "É":
                return "ẫ";
            case "Ë":
                return "ậ";
            case "®":
                return "đ";
            case "Ì":
                return "è";
            case "Ð":
                return "é";
            case "Î":
                return "ẻ";
            case "Ï":
                return "ẽ";
            case "Ñ":
                return "ẹ";
            case "ª":
                return "ê";
            case "Ò":
                return "ề";
            case "Õ":
                return "ế";
            case "Ó":
                return "ể";
            case "Ô":
                return "ễ";
            case "Ö":
                return "ệ";
            case "×":
                return "ì";
            case "Ý":
                return "í";
            case "Ø":
                return "ỉ";
            case "Ü":
                return "ĩ";
            case "Þ":
                return "ị";
            case "ß":
                return "ò";
            case "ã":
                return "ó";
            case "á":
                return "ỏ";
            case "â":
                return "õ";
            case "ä":
                return "ọ";
            case "«":
                return "ô";
            case "å":
                return "ồ";
            case "è":
                return "ố";
            case "æ":
                return "ổ";
            case "ç":
                return "ỗ";
            case "é":
                return "ộ";
            case "¬":
                return "ơ";
            case "ê":
                return "ờ";
            case "í":
                return "ớ";
            case "ë":
                return "ở";
            case "ì":
                return "ỡ";
            case "î":
                return "ợ";
            case "ï":
                return "ù";
            case "ó":
                return "ú";
            case "ñ":
                return "ủ";
            case "ò":
                return "ũ";
            case "ô":
                return "ụ";
            case "­":
                return "ư";
            case "õ":
                return "ừ";
            case "ø":
                return "ứ";
            case "ö":
                return "ử";
            case "÷":
                return "ữ";
            case "ù":
                return "ự";
            case "ú":
                return "ỳ";
            case "ý":
                return "ý";
            case "û":
                return "ỷ";
            case "ü":
                return "ỹ";
            case "þ":
                return "ỵ";
            case "¡":
                return "Ă";
            case "¢":
                return "Â";
            case "§":
                return "Đ";
            case "£":
                return "Ê";
            case "¤":
                return "Ô";
            case "¥":
                return "Ơ";
            case "¦":
                return "Ư";
            default:
                return text;
        }
    }

    private static string unitotcvn(string text)
    {
        switch (text)
        {
            case "à":
                return "µ";
            case "á":
                return "\u00b8";
            case "ả":
                return "¶";
            case "ã":
                return "·";
            case "ạ":
                return "¹";
            case "ă":
                return "\u00a8";
            case "ằ":
                return "»";
            case "ắ":
                return "¾";
            case "ẳ":
                return "¼";
            case "ẵ":
                return "½";
            case "ặ":
                return "Æ";
            case "â":
                return "©";
            case "ầ":
                return "Ç";
            case "ấ":
                return "Ê";
            case "ẩ":
                return "È";
            case "ẫ":
                return "É";
            case "ậ":
                return "Ë";
            case "đ":
                return "®";
            case "è":
                return "Ì";
            case "é":
                return "Ð";
            case "ẻ":
                return "Î";
            case "ẽ":
                return "Ï";
            case "ẹ":
                return "Ñ";
            case "ê":
                return "ª";
            case "ề":
                return "Ò";
            case "ế":
                return "Õ";
            case "ể":
                return "Ó";
            case "ễ":
                return "Ô";
            case "ệ":
                return "Ö";
            case "ì":
                return "×";
            case "í":
                return "Ý";
            case "ỉ":
                return "Ø";
            case "ĩ":
                return "Ü";
            case "ị":
                return "Þ";
            case "ò":
                return "ß";
            case "ó":
                return "ã";
            case "ỏ":
                return "á";
            case "õ":
                return "â";
            case "ọ":
                return "ä";
            case "ô":
                return "«";
            case "ồ":
                return "å";
            case "ố":
                return "è";
            case "ổ":
                return "æ";
            case "ỗ":
                return "ç";
            case "ộ":
                return "é";
            case "ơ":
                return "¬";
            case "ờ":
                return "ê";
            case "ớ":
                return "í";
            case "ở":
                return "ë";
            case "ỡ":
                return "ì";
            case "ợ":
                return "î";
            case "ù":
                return "ï";
            case "ú":
                return "ó";
            case "ủ":
                return "ñ";
            case "ũ":
                return "ò";
            case "ụ":
                return "ô";
            case "ư":
                return "­";
            case "ừ":
                return "õ";
            case "ứ":
                return "ø";
            case "ử":
                return "ö";
            case "ữ":
                return "÷";
            case "ự":
                return "ù";
            case "ỳ":
                return "ú";
            case "ý":
                return "ý";
            case "ỷ":
                return "û";
            case "ỹ":
                return "ü";
            case "ỵ":
                return "þ";
            case "Ă":
                return "¡";
            case "Â":
                return "¢";
            case "Đ":
                return "§";
            case "Ê":
                return "£";
            case "Ô":
                return "¤";
            case "Ơ":
                return "¥";
            case "Ư":
                return "¦";
            default:
                return text;
        }
    }

    public static string UnicodeToTCVN3(string value)
    {
        string text = "";
        for (int i = 0; i < value.Length; i++)
        {
            text += unitotcvn(value[i].ToString());
        }
        return text;
    }

    public static string TCVN3ToUnicode(string value)
    {
        string text = "";
        for (int i = 0; i < value.Length; i++)
        {
            text += tcvntouni(value[i].ToString());
        }
        return text;
    }
}
