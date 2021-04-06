using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UI;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

[Serializable]
public class GridModel
{
    public List<CellModel> Grid = new List<CellModel>();
    public int Cols { get; set; }
    public int Rows { get; set; }
    
    public int Size => Rows * Cols;

    public void AddCell(CellModel cellModel)
    {
        Grid.Add(cellModel);
    }

    private void AddCells(List<CellModel> cellModels)
    {
        foreach (var cellModel in cellModels)
        {
            AddCell(cellModel);
        }
    }
    
    public GridModel(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;

        AddRows(rows, cols);
    }

    public CellModel GetCellByIndex(int index)
    {
        return Grid[index];
    }
    
    public  List<CellModel>  AddNewNumbers()
    {
        List<int> newValues = GetAllActiveCells();
        List<CellModel> newCellsModel = new List<CellModel>();
        
        for (int i = 0; i < newValues.Count; i++)
        {
            CellModel cellModel = new CellModel(newValues[i], i + Grid.Count);
            newCellsModel.Add(cellModel);
        }
        AddCells(newCellsModel);
        CalculateRowsCount();

        Debug.Log("GridCount after add new cell: " + Grid.Count);
        return newCellsModel;
    }

    public void CalculateRowsCount()
    {
        Cols = 9;
        Rows = Grid.Count / Cols;
        Rows = Grid.Count % Cols > 0 ? Rows + 1: Rows;
    }

    public void AddRows(int rows, int cols)
    {
        string values = "";
        for (int i = 0; i < rows * cols; i++)
        {
            int value = Random.Range(1, 10);
            values += value + " ";
            CellModel newCellModel = new CellModel(value, i);
            AddCell(newCellModel);
        }
        Debug.Log(values);
    }

    public void AddCell(int value)
    {
        CellModel newCellModel;
        if (Grid.Count > 0)
        {
            newCellModel = new CellModel(value, Grid[Grid.Count - 1].CurrentIndex + 1);
        }
        else
        {
            newCellModel = new CellModel(value, 0);
        }
        AddCell(newCellModel);
        CalculateRowsCount();
    }
    
    private List<int> GetAllActiveCells()
    {
        List<int> activeCells = new List<int>();
        for (int i = 0; i < Grid.Count; i++)
        {
            if(Grid[i].Value > 0)
                activeCells.Add(Grid[i].Value);
        }
        return activeCells;
    }

    private bool IsCellsInCol(int minIndex, int maxIndex)
    {
        for (int i = maxIndex; i >= 0; i -= Cols)
        {
            if (i == minIndex)
                return true;
        }

        return false;
    }

    private bool CheckHorizontalCellsCanBeDestroyed(int minIndex, int maxIndex)
    {
        CellModel cellModel1 = Grid[minIndex];
        CellModel cellModel2 = Grid[maxIndex];

        if (IsEqualsOrSum(cellModel1, cellModel2) && cellModel1.CurrentIndex != cellModel2.CurrentIndex)
        {
            for (int i = minIndex + 1; i <= maxIndex; i++)
            {
                if (Grid[i].Value < 0)
                {
                    continue;
                }
                else if (!IsEqualsOrSum(Grid[i], cellModel2))
                {
                    return false;
                }
                else if (IsEqualsOrSum(Grid[i], cellModel2) && Grid[i].CurrentIndex == cellModel2.CurrentIndex)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
    
    private bool CheckVerticalCellsCanBeDestroyed(int minIndex, int maxIndex)
    {
        CellModel cellModel1 = Grid[minIndex];
        CellModel cellModel2 = Grid[maxIndex];

        if (IsEqualsOrSum(cellModel1, cellModel2) && IsCellsInCol(minIndex, maxIndex))
        {
            for (int i = maxIndex - Cols; i >= 0; i -= Cols)
            {
                if (Grid[i].Value < 0)
                {
                    continue;
                }
                else if (!IsEqualsOrSum(Grid[i], cellModel1))
                {
                    return false;
                }
                else if (IsEqualsOrSum(Grid[i], cellModel1) && Grid[i].CurrentIndex == cellModel1.CurrentIndex)
                {
                    return true;
                }
                else return false;
            }
        }

        return false;
    }

    private bool CheckFirstAndLastCanBeDestroyed(int indexCell1, int indexCell2)
    {
        CellModel cellModel1 = Grid[indexCell1];
        CellModel cellModel2 = Grid[indexCell2];
        
        if (IsFirstAndLast(cellModel1, cellModel2) && IsEqualsOrSum(cellModel1, cellModel2))
        {
            return true;
        }

        return false;
    }

    private bool IsFirstAndLast(CellModel cellModel1, CellModel cellModel2)
    {
        CellModel firstCell = Grid.First(cell => cell.Value > 0);
        CellModel lastCell = Grid.Last(cell => cell.Value > 0);

        return cellModel1.CurrentIndex == firstCell.CurrentIndex && cellModel2.Value == lastCell.Value && cellModel2.CurrentIndex == lastCell.CurrentIndex;
    }

    private bool IsEqualsOrSum(CellModel value1, CellModel value2)
    {
        return value1 == value2 || value1.Value + value2.Value == 10;
    }
    
    public bool Calculate(int cellView1Index, int cellView2Index)
    {
        int minIndex = Mathf.Min(cellView1Index, cellView2Index);
        int maxIndex = Mathf.Max(cellView1Index, cellView2Index);
        
        CellModel first = Grid[minIndex];
        CellModel second = Grid[maxIndex];
        
        if (CheckHorizontalCellsCanBeDestroyed(minIndex, maxIndex))
        {
            Enable(first, second);
            return true;
        }
        else if (CheckVerticalCellsCanBeDestroyed(minIndex, maxIndex))
        {
            Enable(first, second);
            return true;
        }
        else if (CheckFirstAndLastCanBeDestroyed(minIndex, maxIndex))
        {
            Enable(first, second);
            return true;
        }
        else
        {
            Vibration.Medium();
        }
        
        return false;
    }
    
    public bool CalculateWithoutDelete(int cellView1Index, int cellView2Index)
    {
        int minIndex = Mathf.Min(cellView1Index, cellView2Index);
        int maxIndex = Mathf.Max(cellView1Index, cellView2Index);

        if (CheckHorizontalCellsCanBeDestroyed(minIndex, maxIndex))
        {
            return true;
        }
        else if (CheckVerticalCellsCanBeDestroyed(minIndex, maxIndex))
        {
            return true;
        }
        else if (CheckFirstAndLastCanBeDestroyed(minIndex, maxIndex))
        {
            return true;
        }

        return false;
    }
    
#region remove row
    public List<int> FindEmptyRow()
    {
        List<int> RowsRemove = new List<int>();
        bool canDeleteRow = true;
        for (int i = 0; i < Rows; i++)
        {
            if (i == Rows - 1)
            {
                for (int j = 0; j < Ostatok(); j++)
                {
                    if (Grid[i * Cols + j].Value != -1)
                    {
                        canDeleteRow = false;
                        break;
                    }
                }                
            }
            else
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Grid[i * Cols + j].Value != -1)
                    {
                        canDeleteRow = false;
                        break;
                    }
                }
            }

            if (canDeleteRow)
            {
                RowsRemove.Add(i);
            }

            canDeleteRow = true;
        }

        if (RowsRemove.Count > 0)
        {
            RowsRemove.Reverse();
        }
        return RowsRemove;
    }

    public void DestroyRowView(int row)
    {
        if (row == Rows - 1)
        {
            for (int j = 0; j < Ostatok(); j++)
            {
                Grid[row * Cols + j].DestroyCell();
            }
        }
            
        else
        {
            for (int j = 0; j < Cols; j++)
            {
                Grid[row * Cols + j].DestroyCell();
            }

        }
    }
    
    public void ChangeIndexAfterRowDestroy(int row)
    {
        if (row == Rows - 1)
        {
            for (int j = row * Cols; j < Grid.Count; j++)
            {
                Grid[j].CurrentIndex = Grid[j].CurrentIndex - Ostatok();
            }
        }
            
        else
        {
            for (int j = row * Cols; j < Grid.Count; j++)
            {
                Grid[j].CurrentIndex = Grid[j].CurrentIndex - Cols;
            }
        }
    }
    
    public void RemoveBadIndex(int row)
    {
        if (row == Rows - 1)
        {
            Grid.RemoveRange(row * Cols, Ostatok());
        }
            
        else
        {
            Grid.RemoveRange(row * Cols, Cols);
        }
    }
    
    public int Ostatok()
    {
        int ost;
        if (Size - Grid.Count == 0)
        {
            ost = 9;
        }
        else
        {
            ost = Grid.Count - (Size - Cols);
        }

        return ost;
    }
    
    #endregion
    
    private void Enable(CellModel first, CellModel second)
    {
        first.Remove();
        second.Remove();
    }
}