using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Screen = UI.Screen;

public class ScreenController : MonoBehaviour
{
    List<Screen> screens = new List<Screen>();

    private void Start()
    {
        screens = FindObjectsOfType<Screen>(true).ToList();
    }

    public void ChangeScreenTo(GameObject _screen)
    {
        foreach (var screen in screens)
        {
            screen.gameObject.SetActive(false);
        }
        _screen.SetActive(true);
    }
}
