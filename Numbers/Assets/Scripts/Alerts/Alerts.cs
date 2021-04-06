using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Alerts : MonoBehaviour
{
    public static Alerts AlertCall = null;

    [SerializeField]
    private Image image1;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private Button buttonText;

    [SerializeField] public Animator DefaultAlert;
    [SerializeField] public Animator AlertError;
    [SerializeField] public Animator DownloadAlert;
    
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI alertText;
    
    [SerializeField]
    private Sprite err;
    [SerializeField]
    private Sprite suc;
    private void Awake()
    {
        if(AlertCall == null) AlertCall = this;
    }

    public void CallSuccess(string Txt = "Done!")
    {
        image.overrideSprite = suc;
        alertText.text = Txt;
        AlertError.SetTrigger("Show");
    }
    
    public void CallError(string Txt = "Error!")
    {
        image.overrideSprite = err;
        alertText.text = Txt;
        AlertError.SetTrigger("Show");
    }

    public void CallWithText([CanBeNull]Sprite MainImage, [CanBeNull] UnityAction method, string Txt = "Error!", string btnText = "Back")
    {
        if (MainImage)
        {
            image1.enabled = true;
            image1.sprite = MainImage;
        }
        else
        {
            image1.enabled = false;
            image1.sprite = null;
        }
        buttonText.onClick.RemoveAllListeners();
        text.text = Txt;
        buttonText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = btnText;
        if(method != null)
            buttonText.onClick.AddListener(method);
        buttonText.onClick.AddListener(delegate { DefaultAlert.SetTrigger("Hide");});
        DefaultAlert.SetTrigger("Show");
    }
    
    public void CallYesNoAlert([CanBeNull] UnityAction NoButtonAction,[CanBeNull] UnityAction YesButtonAction, string MainText, [CanBeNull]Sprite MainImage, [CanBeNull]Sprite ActiveAdditionalImages, string MiniText = "", string BtnNo = "No", string BtnYes = "Yes")
    {
        var downloadPopUp = GetComponent<DownloadPopUp>();

        if (MainImage)
        {
            downloadPopUp.MainImage.enabled = true;
            downloadPopUp.MainImage.sprite = MainImage;
        }
        else
        {
            downloadPopUp.MainImage.enabled = false;
            downloadPopUp.MainImage.sprite = null;
        }

        if (NoButtonAction != null)
        {
            downloadPopUp.No.AddListener(NoButtonAction);
        }

        if (YesButtonAction != null)
        {
            downloadPopUp.Yes.AddListener(YesButtonAction);
        }
        downloadPopUp.MiniText.text = MiniText;
        downloadPopUp.MainText.text = MainText;
        downloadPopUp.YesBut.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = BtnYes;
        downloadPopUp.NoBut.GetComponent<TextMeshProUGUI>().text = BtnNo;
        foreach (var item in downloadPopUp.AdditionalImages)
        {
            if (ActiveAdditionalImages != null)
            {
                item.gameObject.SetActive(true);
                item.sprite = ActiveAdditionalImages;
            }
            else
            {
                item.gameObject.SetActive(false);
            }

        }
        DownloadAlert.SetTrigger("Show");
    }
}
