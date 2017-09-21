using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FileSystem.Helpers
{
    public class Utils
    {
        public Utils()
        {

        }

        public static int StringToInt(string input)
        {
            int result = 0;
            int.TryParse(input, out result);
            return result;
        }

        public static string ConcatPath(string first, string second)
        {
            if (!first.EndsWith(@"\")) first += @"\";
            return first + second;
        }


        public static List<Local_Classify> FindClassifyFromKeyword(string keyword, List<Local_Classify> classifyList)
        {
            if (string.IsNullOrEmpty(keyword)) return null;


            List<Local_Classify> result = new List<Local_Classify>();

            //var classifyList = db.Local_Classifies.ToList();
            foreach (var classify in classifyList)
            {
                if (!string.IsNullOrEmpty(classify.Keyword))
                {
                    
                    var keywordArray = classify.Keyword.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var subkey in keywordArray)
                    {                        
                        if (keyword.ToLower().Contains(subkey.ToLower()))
                        {
                            if (!result.Exists(t => t.Id == classify.Id))
                            {
                                result.Add(classify);
                            }
                        }
                        
                        
                    }
                }

            }
            return result;
        }

        public static Local_Classify ExistClassifyFromKeyword(string keyword, List<Local_Classify> classifyList)
        {
            if (string.IsNullOrEmpty(keyword)) return null;

            //var classifyList = db.Local_Classifies.ToList();
            foreach (var classify in classifyList)
            {
                if (!string.IsNullOrEmpty(classify.Keyword))
                {                    
                    var keywordArray = classify.Keyword.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var subkey in keywordArray)
                    {
                        if (keyword.ToLower() == subkey.ToLower())
                        {
                            return classify;
                        }
                    }
                }

            }
            return null;
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static int GetPostionFirstCharacter(string filename)
        {
            int length = filename.Length;
            for (int i = 0; i < length; i++)
            {
                var value = filename[i];
                if ((value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z'))
                {
                    return i;
                }
            }

            return 0;
        }

        public static int GetPostionFirstNotIsNumber(string filename)
        {
            int length = filename.Length;
            for (int i = 0; i < length; i++)
            {
                var value = filename[i];
                if (!(value >= '0' && value <= '9'))
                {
                    return i;
                }
            }

            return 0;
        }



        public static DateTime StringToDateTime(string inputDate, ref bool isDateTime)
        {
            isDateTime = true;
            DateTime result = new DateTime();
            try
            {
                result = DateTime.ParseExact(inputDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                isDateTime = false;
                Helpers.LogHelper.WriteLog(ex.Message);
            }
            return result;
        }

        public static DateTime StringToDateTime(string inputDate)
        {
            DateTime result = new DateTime();
            try
            {
                result = DateTime.ParseExact(inputDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Helpers.LogHelper.WriteLog(ex.Message);
            }
            return result;
        }
    }
}
