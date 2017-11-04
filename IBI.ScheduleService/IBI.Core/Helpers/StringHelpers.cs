using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace IBI.Core
{
    public static class StringHelpers
    {
        public static bool IsEmpty(this String s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public static bool IsAllDigits(this string s)
        {
            if (String.IsNullOrWhiteSpace(s)) return false;
            return s.All(char.IsDigit);
        }

        public static string GetDigitOnly(this string s)
        {
            return new string(s.Where(char.IsDigit).ToArray());
        }

        #region Format
        
        public static string CleanForName(this string s)
        {
            return Regex.Replace(s, @"[^\w\d\s-]", "").Trim();
        }

        public static string TrimSafe(this string s)
        {
            return (s != null) ? s.Trim() : s;
        }

        public static string GetLastWord(this string s)
        {
            try
            {
                return s.Substring(s.LastIndexOf(' '));
            }
            catch
            {
                return s;
            }
        }

        public static string RemoveDiacritics(string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                              UnicodeCategory.NonSpacingMark)
              ).Normalize(NormalizationForm.FormC);
        }

        #endregion

        #region HTML

        public static string RenderHTML(this string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

                return client.DownloadString(url);
            }
        }

        public static string EncodeHTML(this string s)
        {
            return WebUtility.HtmlEncode(s);
        }

        public static string DecodeHTML(this string html)
        {
            return WebUtility.HtmlDecode(html);
        }

        public static string SwapHTML(this string content, string tagClass)
        {
            return SwapHTML(content, "span", tagClass);
        }

        public static string SwapHTML(this string content, string tag, string tagClass)
        {
            return "<" + tag + " class=\"" + tagClass + "\">" + content + "</" + tag + ">";
        }

        public static string GetFileNameFromUrl(this string url)
        {
            var host = new Uri(url).GetLeftPart(UriPartial.Authority);
            return url.Replace(host, "").Replace("/", "_");
        }

        #endregion

        public static string ConvertToUnSign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        public static string ConvertToFirstUpper(string input)
        {
            var arr = input.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>(); 

            foreach (var item in arr)
            {
                string itemTemp = item[0].ToString().ToUpper() + item.Substring(1);
                list.Add(itemTemp);
            }

            string result = String.Join("", list.ToArray()) ;
            return result;
        }

        public static double StringToDouble(string input)
        {
            double result = 0;
            try
            {
                input = input.Replace("(", "-");
                input = input.Replace(")", "");
                //input = input.Replace(",", ".");
                double.TryParse(input, out result);

            }
            catch (Exception)
            {

            }
            return result;
        }


        public static bool IsDouble(string input)
        {
            double result = 0;
            try
            {                
                double.TryParse(input, out result);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool IsDateTime(string input, string format = "MM/dd/yyyy")
        {
            try
            {
                DateTime result;
                DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
        public static DateTime StringToDateTime(this string input, string format = "MM/dd/yyyy")
        {
            try
            {
                DateTime result;
                DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
                return result;
            }
            catch (Exception)
            {

            }

            return DateTime.MinValue;
        }

        public static double StringToDoubleExact(this string input)
        {
            double result = 0;
            try
            {               
                double.TryParse(input, out result);
            }
            catch (Exception)
            {

            }
            return result;
        }


        public static string Reverse(string input)
        {
            string result = input;
            var arr = input.ToCharArray();
            Array.Reverse(arr);
            result = new string(arr);

            return result;
        }
    }
}
