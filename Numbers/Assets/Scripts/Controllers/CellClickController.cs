using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

public class CellClickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CellView thisCellView;
    private bool activate;
    private void Awake()
    {
        thisCellView = GetComponent<CellView>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BonusController.Instance.ListBonus[1].GetActive() && GridController.Instance.gridModel.Grid[thisCellView.Index].Value != -1)
        {
            activate = true;
            GridController.Instance.GridTransform.parent.GetComponent<ScrollRect>().enabled = false;
            var indexCell = thisCellView.Index;

            for (int j = -1; j < 2; j++)
            {
                if (indexCell % 9 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        try
                        {
                            if (indexCell + i + 9 * j >= 0 && indexCell + i + 9 * j <=
                                GridController.Instance.gridModel.Grid.Count - 1 && GridController.Instance.gridModel.Grid[indexCell + i + 9 * j].Value != -1)
                                GridController.Instance.gridModel.Grid[indexCell + i + 9 * j].GetView().Activate();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                            throw;
                        }

                    }
                }
                else if ((indexCell + 1) % 9 == 0)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        try
                        {
                            if (indexCell + i + 9 * j >= 0 && indexCell + i + 9 * j <=
                                GridController.Instance.gridModel.Grid.Count - 1 && GridController.Instance.gridModel.Grid[indexCell + i + 9 * j].Value != -1)
                                GridController.Instance.gridModel.Grid[indexCell + i + 9 * j].GetView().Activate();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                            throw;
                        }

                    }
                }
                else
                {
                    for (int i = -1; i < 2; i++)
                    {
                        try
                        {
                            if (indexCell + i + 9 * j >= 0 && indexCell + i + 9 * j <=
                                GridController.Instance.gridModel.Grid.Count - 1 && GridController.Instance.gridModel.Grid[indexCell + i + 9 * j].Value != -1)
                                GridController.Instance.gridModel.Grid[indexCell + i + 9 * j].GetView().Activate();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(e);
                            throw;
                        }

                    }
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (BonusController.Instance.ListBonus[1].GetActive())
        {
            foreach (var item in GridController.Instance.gridModel.Grid)
            {
                item.GetView().DeactivateImmediate();
            }            
        }

        Wait.NextFrame(() =>
        {
            activate = false;
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    private void Update()
    {
#if !UNITY_EDITOR
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (BonusController.Instance.ListBonus[1].GetActive() && activate)
                {
                    Vibration.Heavy();
                    BonusController.Instance.ListBonus[1].Do?.Invoke(GridController.Instance.gridModel, thisCellView.Index,
                        null, null);
                    GridController.Instance.RemoveRow(false);
                    GridController.Instance.SavedGrids.UpdateListGrid(GridController.Instance.gridModel);
                    GridController.Instance.GridTransform.parent.GetComponent<ScrollRect>().enabled = true;
                    foreach (var item in GridController.Instance.gridModel.Grid)
                    {
                        item.GetView().DeactivateImmediate();
                    }
                }
            }
        }
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0) && activate)
        {
            if (BonusController.Instance.ListBonus[1].GetActive())
            {
                Vibration.Heavy();
                BonusController.Instance.ListBonus[1].Do?.Invoke(GridController.Instance.gridModel, thisCellView.Index,
                    null, null);
                GridController.Instance.RemoveRow(false);
                GridController.Instance.SavedGrids.UpdateListGrid(GridController.Instance.gridModel);
                GridController.Instance.GridTransform.parent.GetComponent<ScrollRect>().enabled = true;
                foreach (var item in GridController.Instance.gridModel.Grid)
                {
                    item.GetView().DeactivateImmediate();
                }   
            }
        }
#endif
    }
}
