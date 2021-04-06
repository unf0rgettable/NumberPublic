using System;
using Controllers;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class Bonus : MonoBehaviour
{
    [SerializeField] public Sprite cry;
    [SerializeField] public GameObject Border;
    [SerializeField] private TextMeshProUGUI CountText;
    [NonSerialized] public Sprite MainSprite;
    private bool Active;
    private int _count = 0;
    [SerializeField] private bool isLog = false;
    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            if (_count <= 0)
            {
                _count = 0;
            }
            CountText.text = _count.ToString();
            CountText.enabled = _count != 0;
            if(isLog)
                Debug.LogError("Count Bomb: " +  _count);
        }
    }
    public Action<GridModel, int, GridModel, GridController> _do;

    public Action<GridModel, int, GridModel, GridController> Do
    {
        get => _do;
        set
        {
            if (isLog)
            {
                Debug.LogError("Do set!");
            }
            _do = value;
        } 
    }

    private void Awake()
    {
        MainSprite = GetComponent<Image>().sprite;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(Count <= 0)
                Alerts.AlertCall.CallYesNoAlert(null, () =>
                {
                    Count++;
                }, Res.lang.BonusEnd, MainSprite, cry, Res.lang.WatchAdAndGetBonus, Res.lang.Denial[0], Res.lang.Confirmation[0]);
        });
    }

    public bool GetActive()
    {
        return Active;
    }

    public void SetActive(bool active)
    {
        Active = active;
        if(Border)
            Border.SetActive(active);
        if (Count <= 0)
        {
            Active = false;
            if(Border)
                Border.SetActive(false);
        }
    }
}