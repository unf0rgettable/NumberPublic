using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DownloadPopUp : MonoBehaviour
{
    public UnityEvent Yes, No;

    [SerializeField]
    public Button YesBut;
    
    [SerializeField]
    public Button NoBut;
    
    [SerializeField]
    public TextMeshProUGUI MainText;

    [SerializeField]
    public TextMeshProUGUI MiniText;
    
    [SerializeField]
    public Image MainImage;

    [SerializeField]
    public List<Image> AdditionalImages = new List<Image>();
    
    public static DownloadPopUp Instance;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Yes == null) Yes = new UnityEvent();
        if (No == null) No = new UnityEvent();
        
    }

    private void Start()
    {
        YesBut.onClick.AddListener(() =>
        {
            if (Yes != null) Yes.Invoke();
            Alerts.AlertCall.DownloadAlert.SetTrigger("Hide");
            Yes.RemoveAllListeners();
            No.RemoveAllListeners();
        });
        NoBut.onClick.AddListener(() =>
        {
            if (No != null) No.Invoke();
            Alerts.AlertCall.DownloadAlert.SetTrigger("Hide");
            Yes.RemoveAllListeners();
            No.RemoveAllListeners();
        });
    }
}
