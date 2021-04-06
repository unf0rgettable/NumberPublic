using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

public class AchiveController : MonoBehaviour
{
    // public List<Achievement> achives = new List<Achievement>();
    //
    // public int LocalScore = 0;
    //
    // public static AchiveController Instance;
    //
    // private void Awake()
    // {
    //     if (Instance == null) Instance = this;
    //     achives = FindObjectsOfType<Achievement>(true).ToList();
    //
    // }
    //
    //
    // public Achievement GetNearestAchive()
    // {
    //     Achievement temp = new Achievement();
    //     temp.NeedScore = 0;
    //     
    //     foreach (var achive in achives)
    //     {
    //         if (achive.NeedScore > temp.NeedScore)
    //         {
    //             temp.NeedScore = achive.NeedScore;
    //         }
    //     }
    //     
    //     foreach (var achive in achives)
    //     {
    //         if (achive.OpenAvailable() && achive.NeedScore < temp.NeedScore)
    //         {
    //             temp = achive;
    //         }
    //     }
    //
    //     return temp;
    // }
}
