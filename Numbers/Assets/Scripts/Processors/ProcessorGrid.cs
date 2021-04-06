using UnityEngine;

namespace Processors
{
    public static class ProcessorGrid
    {
        public static bool IsLastRow(this GridModel gridModel, int index)
        {
            return index >= gridModel.Size - gridModel.Cols;
        }

        public static bool IsFirstRow(this GridModel gridModel, int index)
        {
            return index < gridModel.Cols;
        }

        public static bool IsLeftCol(this GridModel gridModel, int index)
        {
            return index % gridModel.Cols == 0;
        }

        public static bool IsRightCol(this GridModel gridModel, int index)
        {
            return index % gridModel.Cols == gridModel.Cols - 1;
        }

        public static int GetTypeCellInGrid(this GridModel gridModel, int index)
        {
            //0, 1, 1, 2
            //3, 4  4  5
            //6  7  7  8

            if (gridModel.IsFirstRow(index))
            {
                if (gridModel.IsLeftCol(index))
                {
                    return 0;
                }
                else if (gridModel.IsRightCol(index))
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else if (gridModel.IsLastRow(index))
            {
                if (gridModel.IsLeftCol(index))
                {
                    return 6;
                }
                else if (gridModel.IsRightCol(index))
                {
                    
                    return 8;
                }
                else
                {
                    Debug.Log(index + " isLastRow");
                    return 7;
                }
            }
            else if (gridModel.IsLeftCol(index))
            {
                return 3;
            }
            else if (gridModel.IsRightCol(index))
            {
                return 5;
            }
            else
            {
                return 4;
            }
        }
    }
}