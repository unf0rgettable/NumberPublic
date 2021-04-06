using Data;
using Lean.Gui;
using Localization;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Slider slider;

    private bool open = false;

    [Header("Нужно очков чтобы открыть")]
    public int NeedScore;
    
    [Header("Спрайт для алерта")]
    [SerializeField]
    private Sprite sprite;
    
    [SerializeField]
    private string description;
    
    
    private void OnEnable()
    {
        if (open)
        {
            image.color = Color.white;
            gameObject.GetComponent<LeanButton>().interactable = true;
        }
        else
        {
            image.color = new Color(30f/255f,30f/255f,30f/255f,1);
            gameObject.GetComponent<LeanButton>().interactable = false;
        }
        
        gameObject.GetComponent<LeanButton>().OnClick.AddListener(delegate
        {
            Alerts.AlertCall.CallWithText(sprite, null, description, Res.lang.Confirmation[6]);
        });
    }
}
