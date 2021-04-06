using UnityEngine;

namespace Utils
{
    public static class Vibration
    {
        public static void Light(bool isVibration = true)
        {
            if (isVibration)
            {
                Taptic.Light();
                //Debug.Log("Vibro: light");
            }
        }
        
        public static void Medium(bool isVibration = true)
        {
            if (isVibration)
            {
                Taptic.Medium();
                //Debug.Log("Vibro: Medium");
            }
        }
        
        public static void Heavy(bool isVibration = true)
        {
            if (isVibration)
            {
                Taptic.Heavy();
                //Debug.Log("Vibro: Medium");
            }
        }
    }
}