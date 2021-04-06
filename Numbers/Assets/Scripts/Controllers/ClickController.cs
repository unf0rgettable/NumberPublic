using System;
using System.Collections.Generic;
using Controllers;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour
{
    private Queue<CellView> _queueIndexQlicks = new Queue<CellView>();
    public Action<CellView, CellView> OnSelectedTwoCell;
    public BonusController BonusController;
    public void OnClick(CellView cellView)
    {
        Debug.Log("OnClick");
        _queueIndexQlicks.Enqueue(cellView);

        cellView.Activate();
        
        if (_queueIndexQlicks.Count == 1)
        {
            if (BonusController.Instance.ListBonus[1].GetActive())
            {
                CellView firstCell = _queueIndexQlicks.Dequeue();
            }

            else if (BonusController.Instance.ListBonus[2].GetActive())
            {
                CellView firstCell = _queueIndexQlicks.Dequeue();
                GridController.Instance.OnBonusRemoveNumbersActivate(BonusController.Instance.ListBonus[2], firstCell);
            }
        }
        
        if (_queueIndexQlicks.Count == 2)
        {
            CellView firstCell = _queueIndexQlicks.Dequeue();
            CellView secondCell = _queueIndexQlicks.Dequeue();


            if (firstCell.Index != secondCell.Index)
            {
                firstCell.Deactivate();
                secondCell.Deactivate();
                OnSelectedTwoCell?.Invoke(firstCell, secondCell);
            }
            else
            {
                firstCell.DeactivateImmediate();
            }
            
        }
        
    }
}
