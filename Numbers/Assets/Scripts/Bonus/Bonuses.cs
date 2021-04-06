using System;
using System.Collections.Generic;
using Controllers;
using JetBrains.Annotations;
using Model;
using UnityEngine;

public abstract class Bonuses
{
    public virtual void Activate(GridModel grid, [CanBeNull]int indexCell, [CanBeNull]GridModel newGrid = null, [CanBeNull]GridController gridController = null)
    {
        
    }
}

public class BonusRemoveAllDigits : Bonuses
{
    public void Activate(GridModel grid, [CanBeNull]int indexCell, [CanBeNull]GridModel newGrid = null, [CanBeNull]GridController gridController = null)
    {
        base.Activate(grid, indexCell);
        
        int tempValue = grid.Grid[indexCell].Value;

        for (int i = 0; i < grid.Grid.Count; i++)
        {
            if (grid.Grid[i].Value == tempValue)
            {
                grid.Grid[i].Remove();
            }
        }
    }
}

public class BonusUndo : Bonuses
{
    public void Activate(GridModel grid, [CanBeNull]int indexCell, [CanBeNull]GridModel newGrid = null, [CanBeNull]GridController gridController = null)
    {
        base.Activate(grid, indexCell);

        if (gridController != null)
        {
            gridController.DifferenceBetweenGrid(grid);
        }
    }
}

public class BonusTips : Bonuses
{
    public bool Activate(GridModel grid, [CanBeNull]int indexCell, [CanBeNull]GridModel newGrid = null, [CanBeNull]GridController gridController = null)
    {
        base.Activate(grid, indexCell);

        List<CellModel> tempGridIndex = new List<CellModel>();
        
        for (int i = 0; i < grid.Grid.Count; i++)
        {
            if (grid.Grid[i].Value > 0)
            {
                tempGridIndex.Add(grid.Grid[i]);
            }
        }
        
        for (int i = 0; i < tempGridIndex.Count; i++)
        {
            for (int j = i + 1; j < tempGridIndex.Count; j++)
            {
                if (grid.CalculateWithoutDelete(tempGridIndex[i].CurrentIndex, tempGridIndex[j].CurrentIndex))
                {
                    tempGridIndex[i].GetView().Activate();
                    tempGridIndex[j].GetView().Activate();
                    return true;
                }
            }
        }

        return false;
    }
}

public class BonusRemoveNineCell : Bonuses
{
    public void Activate(GridModel grid, [CanBeNull]int indexCell, [CanBeNull]GridModel newGrid = null, [CanBeNull]GridController gridController = null)
    {
        base.Activate(grid, indexCell);

        for (int j = -1; j < 2; j++)
        {
            if (indexCell % 9 == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    try
                    {
                        if (indexCell + i + 9 * j >= 0 && indexCell + i + 9 * j <= grid.Grid.Count - 1)
                            grid.Grid[indexCell + i + 9 * j].Value = -1;
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
                        if (indexCell + i + 9 * j >= 0 && indexCell + i + 9 * j <= grid.Grid.Count - 1)
                            grid.Grid[indexCell + i + 9 * j].Value = -1;
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
                        if (indexCell + i + 9 * j >= 0 && indexCell + i + 9 * j <= grid.Grid.Count - 1)
                            grid.Grid[indexCell + i + 9 * j].Value = -1;
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