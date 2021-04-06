using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Model;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]private Transform grid;
    [SerializeField]private CellView cellPref;
    [SerializeField]private List<CellView> cellsUI;
    [SerializeField] private int numRow;

    private int Size => numRow * 10;

    public ClickController ClickController { get; private set; }
    private void Awake()
    {
        ClickController = GetComponent<ClickController>();
        cellsUI = FindObjectsOfType<CellView>().ToList();
    }

    public List<CellView> GenerateCellViews(GridModel gridModel, Action <CellView, CellView> OnSelectedTwoCell)
    {
        cellsUI = FindObjectsOfType<CellView>().ToList();
        if (cellsUI != null)
        {
            foreach (var cell in cellsUI)
            {
                Destroy(cell.gameObject);
            }
        }

        if (ClickController)
        {
            //Debug.Log("ClickController");
        }
        ClickController.OnSelectedTwoCell = OnSelectedTwoCell;
        
        cellsUI = new List<CellView>();
        for (int index = 0; index < gridModel.Size; index++)
        {
            CellView cellView = CreateCellView(gridModel.GetCellByIndex(index));
            cellsUI.Add(cellView);
        }

        return cellsUI;
    }

    public CellView CreateCellView(CellModel cellModel)
    {
        grid.GetComponent<GridLayoutGroup>().enabled = true;
        grid.GetComponent<ContentSizeFitter>().enabled = true;
        cellsUI = FindObjectsOfType<CellView>().ToList();
        CellView cellView = Instantiate(cellPref, grid);
        cellView.Index = cellModel.CurrentIndex;
        cellModel.SetView(cellView);
        cellView.Weight = cellModel.Value;
        cellModel.Weight = cellView.Weight;
        
        //Debug.LogWarning(cellView.Index + " Index, " + cellView.Weight + " Weight, " + cellModel.Value + " Value ");
        cellView.AddClickListener((cell) =>
        {
            ClickController.OnClick(cell);
        });
        return cellView;
    }

    public CellView CreateCellViewWithCustomParametrs(CellModel cellModel, [CanBeNull]int weight, [CanBeNull]int index)
    {
        grid.GetComponent<GridLayoutGroup>().enabled = true;
        cellsUI = FindObjectsOfType<CellView>().ToList();
        CellView cellView = Instantiate(cellPref, grid);
        cellView.Index = index;
        cellModel.SetView(cellView);
        cellModel.Weight = weight;
        cellView.Weight = weight;
        
        cellView.AddClickListener((cell) =>
        {
            ClickController.OnClick(cell);
        });
        return cellView;
    }
    
    public void AddNewCellViews(List<CellModel> cellModels)
    {
        cellsUI = FindObjectsOfType<CellView>().ToList();
        for (int i = 0; i < cellModels.Count; i++)
        {
            CellView cellView = CreateCellView(cellModels[i]);
            cellsUI.Add(cellView);
        }
    }
}
