using System;
using GameAnalyticsSDK;
using UnityEngine;

namespace Controllers
{
    public class GAController : MonoBehaviour
    {
        private void Awake()
        {
            GameAnalytics.Initialize();
        }

        private void Start()
        {
            StartGame(1);
        }

        public static void TutorialStageChange(float value)
        {
            GameAnalytics.NewDesignEvent ("TutorialStageComplete",value);
        }
        
        public static void ExitGame(float value)
        {
            GameAnalytics.NewDesignEvent ("ExitGame",value);
        }
        
        public static void StartGame(float value)
        {
            GameAnalytics.NewDesignEvent ("StartGame",value);
        }
        
        public static void LevelReload(float value)
        {
            GameAnalytics.NewDesignEvent ("LevelReload",value);
        }
        
        public static void LevelComplete(float value)
        {
            GameAnalytics.NewDesignEvent ("LevelComplete",value);
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Quit();
            }
        }

        private void OnApplicationQuit()
        {
            Quit();
        }

        private void Quit()
        {
            ExitGame(Time.realtimeSinceStartup);
        }
    }
}
