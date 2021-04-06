using Controllers;
using Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        if (PlayerPrefsController.Tutorial == 0)
        {
            
        }
        else
        {
            Debug.Log(PlayerPrefsController.LastGrid);
            if (PlayerPrefsController.LastGrid != "")
            {
                GridController.Instance.DifferenceBetweenGrid(JsonUtility.FromJson<GridModel>(PlayerPrefsController.LastGrid));
                GridController.Instance.SavedGrids.UpdateListGrid(GridController.Instance.gridModel);                    
            }
        }
    }
}
