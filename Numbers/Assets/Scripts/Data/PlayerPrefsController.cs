using System;
using Controllers;
using UnityEditor;
using UnityEngine;

namespace Data
{
    public class PlayerPrefsController : MonoBehaviour
    {
        public static int CurrentScore
        {
            get => PlayerPrefs.GetInt("CurrentScore");
            set
            {
                PlayerPrefs.SetInt("CurrentScore", value);
                if (PlayerPrefs.GetInt("CurrentScore") > PlayerPrefs.GetInt("Record"))
                {
                    PlayerPrefs.SetInt("Record", value);
                }
                PlayerPrefs.Save();
            }
        }
        
        public static int Record
        {
            get => PlayerPrefs.GetInt("Record");
            set => PlayerPrefs.SetInt("Record", value);
        }

        public static int Tutorial
        {
            get => PlayerPrefs.GetInt("Tutorial", 0);
            set => PlayerPrefs.SetInt("Tutorial", value);
        }
        
        public static string LastGrid
        {
            get => PlayerPrefs.GetString("LastGrid");
            set => PlayerPrefs.SetString("LastGrid", value);
        }
        
        public static int LevelsComplete
        {
            get => PlayerPrefs.GetInt("LevelsComplete");
            set
            {
                PlayerPrefs.SetInt("LevelsComplete", value);
                GAController.LevelComplete(value);
            }
        }
        
        public static int LevelsReload
        {
            get => PlayerPrefs.GetInt("LevelsReload");
            set
            {
                PlayerPrefs.SetInt("LevelsReload", value);
                GAController.LevelReload(value);
            }
        }

        
#if UNITY_EDITOR
        [MenuItem("Application Data/Delet Data")]
        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }
#endif
    }
}
