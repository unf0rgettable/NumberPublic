using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using Localization;
using Model;
using Tutorial;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Screen;

namespace Controllers
{
    public class GridController : MonoBehaviour
    {
        [SerializeField]private GameObject DynamicScorePrefab;
        
        [SerializeField] private Button restart;
        [SerializeField] private Button addNewNumbers;
        [SerializeField] private Button undo;
        [SerializeField] private Button tip;

        [SerializeField]public Transform GridTransform;
        private GridGenerator _gridGenerator;
        public SavedGrids SavedGrids;
        
        public static GridController Instance;
        public GridModel gridModel;
        public int ColumnCount => GridTransform.GetComponent<GridLayoutGroup>().constraintCount;
        public int RowsCount => gridModel.Rows;
        private List<CellView> _cellViews = new List<CellView>();
        private void Awake()
        {
            if (!Instance) Instance = this;
            _gridGenerator = GetComponent<GridGenerator>();
            SavedGrids = new SavedGrids();
            undo.onClick.AddListener(delegate
            {
                OnBonusUndoActivate(undo.GetComponent<Bonus>(), SavedGrids.UndoGridModel());
            });
            tip.onClick.AddListener(delegate
            {
                OnBonusTipsActivate(tip.GetComponent<Bonus>(), gridModel);
            });
            restart.onClick.AddListener(delegate
            {
                SavedGrids.StringListGrid = new List<string>();
                Alerts.AlertCall.CallYesNoAlert(null,
                    Start, Res.lang.ReloadLvlConfirm, null, null, 
                    Res.lang.GridClearAlert, Res.lang.Denial[1], Res.lang.Confirmation[7]);
            });
            addNewNumbers.onClick.AddListener(AddNewNumbers);
        }
        private void Start()
        {
            if (PlayerPrefsController.Tutorial == 0)
            {
                gridModel = TutorialController.CreateTutorGridModel();
                _cellViews = _gridGenerator.GenerateCellViews(gridModel, OnSelectedTwoCells);
            }
            else
            {
                gridModel = new GridModel(3, ColumnCount);
                _cellViews = _gridGenerator.GenerateCellViews(gridModel, OnSelectedTwoCells);
                PlayerPrefsController.LevelsReload += 1;
            }
        }
        
        private void OnSelectedTwoCells(CellView cellView1, CellView cellView2)
        {
            DOTween.defaultAutoKill = true;
            if (gridModel.Calculate(cellView1.Index, cellView2.Index))
            {
                PlusScore(cellView2.transform.position, 30);
                RemoveRow(true);
                //Debug.LogWarning(cellView1.Index + " Index, " + cellView1.Weight + " Weight, ");
                SavedGrids.UpdateListGrid(gridModel);
            }

            if (gridModel.Grid.Count <= 1728)
            {
                addNewNumbers.interactable = true;
            }
        }

        public void OnBonusRemoveNumbersActivate(Bonus bonus, CellView cellView)
        {
            bonus.Do?.Invoke(gridModel, cellView.Index, null, this);
            RemoveRow(false);
            SavedGrids.UpdateListGrid(gridModel);
        }

        public void OnBonusUndoActivate(Bonus bonus, GridModel newGridModel)
        {
            bonus.Do?.Invoke(newGridModel, 0, null, this);
        }

        public void OnBonusTipsActivate(Bonus bonus, GridModel newGridModel)
        {
            bonus.Do?.Invoke(newGridModel, 0, null, this);
        }
        
        public void DifferenceBetweenGrid(GridModel newGridModel)
        {
            int temp = gridModel.Grid.Count;

            if (newGridModel != null)
            {
                if (newGridModel.Grid.Count > gridModel.Grid.Count)
                {
                    for (int i = newGridModel.Grid.Count - (newGridModel.Grid.Count - temp); i < newGridModel.Grid.Count; i++)
                    {
                        gridModel.AddCell(newGridModel.Grid[i]);
                        gridModel.Grid[i].SetView(_gridGenerator.CreateCellViewWithCustomParametrs(newGridModel.Grid[i], newGridModel.Grid[i].Weight, newGridModel.Grid[i].CurrentIndex));
                    }            
                }
                else if(newGridModel.Grid.Count < gridModel.Grid.Count)
                {
                    for (int i = gridModel.Grid.Count - 1; i > temp - (temp - newGridModel.Grid.Count + 1); i--)
                    {
                        gridModel.Grid[i].DestroyCell();
                        gridModel.Grid.RemoveAt(i);
                    }
                    gridModel.CalculateRowsCount();
                }

                for (int i = 0; i < gridModel.Grid.Count; i++)
                {
                    gridModel.Grid[i].Value = newGridModel.Grid[i].Value;
                    gridModel.Grid[i].Weight = newGridModel.Grid[i].Weight;

                    if (gridModel.Grid[i].Value != -1)
                    {
                        gridModel.Grid[i].GetView().Enable();
                    }
                    else
                    {
                        gridModel.Grid[i].GetView().Disable();
                    }
                }
                
                gridModel.CalculateRowsCount();
            }
        }
        
        public void RemoveRow(bool addScore)
        {
            List<int> rowRemoves = gridModel.FindEmptyRow();
            List<float> tempPos = new List<float>();
            foreach (var VARIABLE in gridModel.Grid)
            {
                tempPos.Add(0);
            }
            for (int i = 0; i < rowRemoves.Count; i++)
            {
                for (int j = rowRemoves[i] * gridModel.Cols; j < gridModel.Grid.Count; j++)
                {
                    tempPos[j] += GridTransform.GetComponent<GridLayoutGroup>().cellSize.y;
                }
            }
            bool one = true;
            for (int i = 0; i < rowRemoves.Count; i++)
            {
                GridTransform.GetComponent<GridLayoutGroup>().enabled = false;
                GridTransform.GetComponent<ContentSizeFitter>().enabled = false;
                if (addScore)
                {
                    PlusScore(gridModel.Grid[rowRemoves[i] * gridModel.Cols].GetView().transform.position, 50);
                }
                gridModel.DestroyRowView(rowRemoves[i]);
                if (one)
                {
                    for (int j = 0; j < gridModel.Grid.Count; j++)
                    {
                        if (gridModel.Grid[j].GetView() != null)
                        {
                            gridModel.Grid[j].GetView().DoMove(
                                new Vector3(gridModel.Grid[j].GetView().transform.localPosition.x,
                                    gridModel.Grid[j].GetView().transform.localPosition.y +
                                    tempPos[j], 0), 0.2f);                            
                        }
                    }
                }
                one = false;
                    //_cellViews
                gridModel.ChangeIndexAfterRowDestroy(rowRemoves[i]);
                gridModel.RemoveBadIndex(rowRemoves[i]);
                gridModel.Rows--;
            }
            CheckGameEnd();
        }
        
        public void RemoveView(CellView cell)
        {
            for (int i = 0; i < _cellViews.Count; i++)
            {
                try
                {
                    if(_cellViews[i])
                        if (_cellViews[i].Index == cell.Index)
                        {
                            _cellViews.RemoveAt(i);
                            return;
                        }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }

            }
        }

        private void PlusScore(Vector2 spawnTransform, int plusScore)
        {
            GameObject temp = Instantiate(DynamicScorePrefab, spawnTransform, Quaternion.identity, GridTransform.parent.parent);
            temp.GetComponent<DynamicScore>().ScoreInt = plusScore;
            Score.Score.Instance.ScoreInt += plusScore;
            NearestAchive.Instance.LocalScore += plusScore;
        }
        
        private void AddNewNumbers()
        {
            List<CellModel> newCellModels = gridModel.AddNewNumbers();

            _gridGenerator.AddNewCellViews(newCellModels);
            SavedGrids.UpdateListGrid(gridModel);
            if (gridModel.Grid.Count >= 3456)
            {
                addNewNumbers.interactable = false;
            }
        }

        public void CheckGameEnd()
        {
            if (gridModel.Grid.Count == 0)
            {
                Alerts.AlertCall.CallWithText(null, () =>
                {
                    Start();
                    PlusScore(new Vector2(Screen.width / 2f, Screen.height / 2f), 500);
                    foreach (var bonus in BonusController.Instance.ListBonus)
                    {
                        bonus.Count++;
                    }
                    SavedGrids.UpdateListGrid(gridModel);
                }, Res.lang.CongratsLvlEnd, Res.lang.StartAgain);
                SavedGrids.StringListGrid = new List<string>();
                PlayerPrefsController.LastGrid = "";
                PlayerPrefsController.LevelsComplete += 1;
            }
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Quit();
            }
        }

        private void OnApplicationQuit()
        {
            Quit();
        }

        private void Quit()
        {
            if (SavedGrids.StringListGrid.Count >= 1)
            {
                PlayerPrefsController.LastGrid = SavedGrids.StringListGrid[SavedGrids.StringListGrid.Count - 1];
            }
            else
            {
                SavedGrids.UpdateListGrid(gridModel);
                PlayerPrefsController.LastGrid = SavedGrids.StringListGrid[SavedGrids.StringListGrid.Count - 1];
            }
            SaveBonuses();
            PlayerPrefs.Save();
        }
        
        private void SaveBonuses()
        {
            PlayerPrefs.SetInt("Bonus1", BonusController.Instance.ListBonus[0].Count);
            PlayerPrefs.SetInt("Bonus2", BonusController.Instance.ListBonus[1].Count);
            PlayerPrefs.SetInt("Bonus3", BonusController.Instance.ListBonus[2].Count);
            PlayerPrefs.SetInt("Bonus4", BonusController.Instance.ListBonus[3].Count);
        }
    }
}
