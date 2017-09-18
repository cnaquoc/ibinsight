using System;
using System.Collections.Generic;
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
                    //var keywordArray = classify.Keyword.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var keywordArray = classify.Keyword.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var subkey in keywordArray)
                    {
                        if (keyword.Trim().ToLower().Contains(subkey.ToLower().Replace(@"\r", "").Trim()))
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
                    var keywordArray = classify.Keyword.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var subkey in keywordArray)
                    {
                        if (keyword.Trim().ToLower() == subkey.Replace(@"\r", "").Trim().ToLower())
                        {
                            return classify;
                        }
                    }
                }

            }
            return null;
        }
    }
}
