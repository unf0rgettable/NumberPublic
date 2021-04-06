using System;
using System.Collections;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Score : MonoBehaviour
    {
        private TextMeshProUGUI _score;
        private int _scoreInt = 0;
        private int _scoreIntTo = 0;
        public int ScoreInt
        {
            get => _scoreIntTo;
            set
            {
                StartCoroutine(UpdateScore(value));
            }
        }

        public static Score Instance = null;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            _score = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            ScoreInt = PlayerPrefsController.CurrentScore;
        }

        private IEnumerator UpdateScore(int value)
        {
            float temp = 0;
            _scoreIntTo = value;
            yield return new WaitForSeconds(1f);
            DOVirtual.Float(_scoreInt, _scoreIntTo, 0.5f, temp =>
            {
                _score.text = Convert.ToInt32(temp).ToString();
                _scoreInt = Convert.ToInt32(temp);
            });
            PlayerPrefsController.CurrentScore = _scoreIntTo;
            NearestAchive.Instance.UpdateValue();
        }
    }
}
