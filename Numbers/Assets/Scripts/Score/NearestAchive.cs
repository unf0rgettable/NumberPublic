using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using DG.Tweening.Core;
using Localization;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Slider))]
public class NearestAchive : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Sprite tort;
    public static NearestAchive Instance = null;
    public float LocalScore = 0;
    public float StepToNext = 1000;
    [SerializeField]
    private List<Color32> colors = new List<Color32>();

    private Sequence seq;
    private Sequence seq1;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
        seq = DOTween.Sequence();
        seq1 = DOTween.Sequence();
    }
    
    public void UpdateValue()
    {
        float temp = 0;

        seq.Append(DOVirtual.Float(_slider.value,
            LocalScore / StepToNext, 0.5f, temp =>
            {
                _slider.value = temp;
            }).OnComplete(delegate
            {
                if (LocalScore / StepToNext >= 1)
                {
                    Bonus tempBonus = BonusController.Instance.GetRandomBonus();
                    Alerts.AlertCall.CallYesNoAlert(() =>
                    {
                        tempBonus.Count++;
                    }, () =>
                    {
                        tempBonus.Count += 3;
                    },Res.lang.Confirmation[8], tempBonus.MainSprite, tort, Res.lang.WonBonus, Res.lang.Confirmation[9], Res.lang.Confirmation[10]);
                    LocalScore = LocalScore - StepToNext;
                    _slider.DOValue(LocalScore / StepToNext, 0.5f);
                    _slider.value = 0;
                    int index = Random.Range(0, colors.Count);
                    _slider.transform.GetChild(1).GetChild(0).GetComponent<Image>().DOColor(UpdateColors(index), 0.3f);
                    _slider.transform.GetChild(3).GetComponent<Image>().DOColor(UpdateColors(index), 0.3f);
                }
            }));
        for (int i = 1; i < 6; i++)
        { 
            DOVirtual.DelayedCall(i * 0.1f, delegate
            {
                Vibration.Light();
            });            
        }

    }

    private Color UpdateColors(int index)
    {
        if (index > colors.Count || index < 0)
        {
            return colors[Random.Range(0, colors.Count)];
        }
        else
        {
            return colors[index];
        }
    }
}
