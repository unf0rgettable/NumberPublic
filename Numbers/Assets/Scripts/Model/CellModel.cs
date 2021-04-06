using System;
using Controllers;
using UI;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class CellModel
    {
        [SerializeField]
        private int _weight;
        public int Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                if(_cellView) _cellView.Weight = value;
            }
        }
        [SerializeField]
        private int _value;
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                if (value == -1)
                {
                    _cellView.Disable();
                }

                if (value == 1)
                {
                    if(_cellView) _cellView.Enable();
                }
            }
        }
        [SerializeField]
        private int _currentIndex;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                if(_cellView)
                    _cellView.Index = value;
            }
        }

        [SerializeField]
        private CellView _cellView;
        public CellModel(int value, int currentIndex)
        {
            Value = value;
            CurrentIndex = currentIndex;
        }

        public void SetView(CellView cellView)
        {
            _cellView = cellView;
            _currentIndex = _cellView.Index;
        }

        public CellView GetView()
        {
            return _cellView;
        }
        
        public void Remove()
        {
            Value = -1;
        }

        public void DestroyCell()
        {
            _cellView.DestroyCurrView();
            _cellView = null;
        }
        
        public static bool operator ==(CellModel cellModel1, CellModel cellModel2)
        {
            return cellModel1.Value == cellModel2.Value;
        }

        public static bool operator !=(CellModel cellModel1, CellModel cellModel2)
        {
            return !(cellModel1 == cellModel2);
        }
    }
}