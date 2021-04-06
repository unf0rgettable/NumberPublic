using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Localization;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusController : MonoBehaviour
{
    BonusRemoveAllDigits bonusRemoveAllDigits = new BonusRemoveAllDigits();
    BonusUndo bonusUndo = new BonusUndo();
    BonusTips bonusTips = new BonusTips();
    BonusRemoveNineCell bonusRemoveNineCell = new BonusRemoveNineCell();
    
    public List<Bonus> ListBonus = new List<Bonus>();

    public static BonusController Instance = null;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {

        ListBonus = GetComponentsInChildren<Bonus>().ToList();

        ListBonus[0].Count = PlayerPrefs.GetInt("Bonus1");
        ListBonus[1].Count = PlayerPrefs.GetInt("Bonus2");
        ListBonus[2].Count = PlayerPrefs.GetInt("Bonus3");
        ListBonus[3].Count = PlayerPrefs.GetInt("Bonus4");
        
        
        ListBonus[0].Do = delegate(GridModel model, int i, GridModel newModel, GridController gridController)
        {
            if (model != null && ListBonus[0].GetActive())
            {
                if(bonusTips.Activate(model, i, newModel, gridController))
                {
                    ListBonus[0].Count--;
                    ListBonus[0].SetActive(false);
                }
                else
                {
                    Alerts.AlertCall.CallWithText(null, () =>
                    {
                        ListBonus[0].SetActive(false);
                    }, Res.lang.MovesOver, Res.lang.Confirmation[5]);
                }
            }
            else
            {
                ListBonus[0].SetActive(false);
            }
        };
        
        ListBonus[1].Do = (GridModel model, int i, GridModel newModel, GridController gridController)=>
        {
            if (model != null && ListBonus[1].GetActive())
            {
                bonusRemoveNineCell.Activate(model,i, newModel, gridController);
                ListBonus[1].Count--;
                ListBonus[1].SetActive(false);
            }
            else
            {
                ListBonus[1].SetActive(false);
            }
        };

        ListBonus[2].Do = delegate(GridModel model, int i, GridModel newModel, GridController gridController)
        {
            if (ListBonus[2].GetActive())
            {
                ListBonus[2].Count--;
                ListBonus[2].SetActive(false);
                bonusRemoveAllDigits.Activate(model,i, newModel, gridController);
            }
            else
            {
                ListBonus[2].SetActive(false);
            }
        };
        ListBonus[3].Do = delegate(GridModel model, int i, GridModel newModel, GridController gridController)
        {
            if (model != null && ListBonus[3].GetActive())
            {
                ListBonus[3].Count--;
                ListBonus[3].SetActive(false);
                bonusUndo.Activate(model, i, newModel, gridController);
            }
            else
            {
                ListBonus[3].SetActive(false);
            }
        };
        
        // ListBonus[0].Count = 99;
        // ListBonus[1].Count = 99;
        // ListBonus[2].Count = 1;
        // ListBonus[3].Count = 99;
    }

    public void SetBonusActive(Bonus bonus)
    {
        foreach (var item in ListBonus)
        {
            item.SetActive(false);
        }

        bonus.SetActive(true);
    }
    
    public Bonus GetBonusActive()
    {
        foreach (var item in ListBonus)
        {
            if (item.GetActive())
            {
                return item;
            }
        }

        return null;
    }
    
    public Bonus GetRandomBonus()
    {
        return ListBonus[Random.Range(0, ListBonus.Count)];
    }
}
