using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Localization
{
    public static class StaticRes
    {
        public static Languages language { get; private set; }
        
        public static string GetTextByKey(string _id)
        {
            //string json = "";
            
            StreamReader reader = new StreamReader(Application.dataPath + "/StreamingAssets" + "/lang/" + "enus" + ".json");
            string json = reader.ReadToEnd();
            //json = BetterStreamingAssets.ReadAllText("/lang/" + "enus" + ".json");
            language = JsonUtility.FromJson<Languages>(json);
            List<System.Reflection.FieldInfo> asd = GetIds();
            foreach (var item in asd)
            {
                if (item.Name == _id)
                {
                    return language.GetTextById(item.Name);
                }
            }

            return "";
        }
        
        
        public static string[] GetLanguageKey()
        {
            List<string> temp = new List<string>();
            temp.Add("SearchID");
            foreach (var item in GetIds())
            {
                temp.Add(item.Name);
            }

            return temp.ToArray();
        }
        private static List<System.Reflection.FieldInfo> GetIds()
        {
            return typeof(Languages).GetFields().ToList();
        }
    }
    
    
}