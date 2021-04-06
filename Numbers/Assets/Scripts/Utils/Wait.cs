using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class Wait : MonoBehaviour
    {
        private static Wait _instance = null;
        private static Wait Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("[WaitComponent]").AddComponent<Wait>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        public static void Stop(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
        
        public static Coroutine ForSeconds(float seconds, Action onComplete)
        {
            return Instance.StartCoroutine(EnumeratorForSeconds(seconds, onComplete));
        }
        
        private static IEnumerator EnumeratorForSeconds(float seconds, Action onComplete)
        {
            yield return new WaitForSeconds(seconds);
            onComplete.Invoke();
        }

        public static Coroutine NextFrame(Action onComplete)
        {
            return Instance.StartCoroutine(EnumeratorNextFrame(onComplete));
        }
        
        private static IEnumerator EnumeratorNextFrame(Action onComplete)
        {
            yield return null;
            onComplete.Invoke();
        }
    }
}