using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class SavedGrids
    {
        public List<string> StringListGrid = new List<string>();

        public void UpdateListGrid(GridModel gridModel)
        {
            if (StringListGrid.Count < 21)
            {
                Debug.Log("StringListGrid Add");
                StringListGrid.Add(JsonUtility.ToJson(gridModel));
            }
            else
            {
                for (int i = 0; i < StringListGrid.Count - 1; i++)
                {
                    StringListGrid[i] = StringListGrid[i + 1];
                }

                StringListGrid[StringListGrid.Count - 1] = JsonUtility.ToJson(gridModel);
            }
        }

        public GridModel UndoGridModel()
        {
            if (StringListGrid.Count > 1)
            {
                StringListGrid.RemoveAt(StringListGrid.Count - 1);
                return JsonUtility.FromJson<GridModel>(StringListGrid[StringListGrid.Count - 1]);
            }
            else
            {
                return null;
            }
        }
    }
}