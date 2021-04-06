using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actions : MonoBehaviour
{
    /*#region Settings
    [Header("When 2 Cell Selected")]
    [SerializeField] UnityEvent on2CellSelected;
    [Header("When Grid Update")]
    [SerializeField] UnityEvent onGridUpdate;
    [Header("When Cell Update")]
    [SerializeField] UnityEvent onCellUpdate;

    #endregion
	
	
    //public static UnityActions
    #region Variables
	
    //for call use Actions.On2CellSelected?.Invoke();
    public static UnityAction On2CellSelected;
    public static UnityAction OnGridUpdate;
    public static UnityAction<int> OnCellUpdate;

    #endregion
	
    #region UnityMethods
    private void Awake()
    {
        On2CellSelected += On2CellSelectedHandler;
        OnGridUpdate += OnGridUpdateHandler;
        OnCellUpdate += delegate(int index)
        {
            OnCellUpdateHandler(index);
        };
    }
    private void OnDestroy()
    {
        On2CellSelected -= On2CellSelectedHandler;
        OnGridUpdate -= OnGridUpdateHandler;
        OnCellUpdate -= OnCellUpdateHandler;
    }
    #endregion
	
    #region ActionsMethods
    private void On2CellSelectedHandler()
    {
        on2CellSelected?.Invoke();
    }    
    private void OnGridUpdateHandler()
    {
        onGridUpdate?.Invoke();
    }
    private void OnCellUpdateHandler(int index)
    {
        onCellUpdate?.Invoke();
    }
    #endregion*/
}
