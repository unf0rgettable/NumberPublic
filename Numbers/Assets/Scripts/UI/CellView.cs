using System;
using Controllers;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [Serializable]
    public class CellView : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]private Image background;
        [SerializeField]private TextMeshProUGUI weight;
        private Image _borderImage;
        [SerializeField]
        public int Index = 0;
        private Button _button;
        private Color _defaultColor;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _borderImage = GetComponent<Image>();
            _defaultColor = background.color;
            weight = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }

        [SerializeField]
        private int _weight;
        public int Weight
        {
            set
            {
                weight.text = value.ToString();
                _weight = value;
            }
            get => _weight;
        }

        public void Activate()
        {
            background.gameObject.SetActive(true);
        }

        public void DeactivateImmediate()
        {
            if(background)
                background.gameObject.SetActive(false);
        }
        public void Deactivate()
        {
            Wait.ForSeconds(0.5f, () =>
            {
                DeactivateImmediate();
            });
        }

        public void AddClickListener(Action<CellView> action)
        {
            _button.onClick.AddListener(() => action.Invoke(this));
        }

        public void Disable()
        {
            background.gameObject.SetActive(false);
            weight.color = new Color(weight.color.r, weight.color.g, weight.color.b, 0.25f);
            _button.interactable = false;
        }

        public void Enable()
        {
            background.gameObject.SetActive(false);
            weight.color = new Color(weight.color.r, weight.color.g, weight.color.b, 1);
            _button.interactable = true;
        }
        
        public void DestroyCurrView()
        {
            GridController.Instance.RemoveView(this);
            Wait.NextFrame(() =>
            {
                if (gameObject)
                    Destroy(gameObject);
            });
        }
        TweenerCore<Vector3, Vector3, VectorOptions> tweenerMove;

        public void DoMove(Vector3 endVlaue,float duration)
        {
            tweenerMove?.Kill();
            tweenerMove = transform.DOLocalMove(endVlaue, duration).OnComplete(delegate
            {
                GridController.Instance.GridTransform.GetComponent<GridLayoutGroup>().enabled = true;
                GridController.Instance.GridTransform.GetComponent<ContentSizeFitter>().enabled = true;
                tweenerMove?.Kill();
            });
        }
        
        
        public void SetSprite(Sprite sprite)
        {
            _borderImage.sprite = sprite;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}
