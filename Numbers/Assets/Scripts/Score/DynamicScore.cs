using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DynamicScore : MonoBehaviour
{
    private TextMeshProUGUI score;

    
    
    public int ScoreInt
    {
        set
        {
            score.text = "+ " + value.ToString();
        }
    }

    private void Awake()
    {
        DOTween.defaultAutoKill = true;
        score = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GetComponent<Animation>().clip.legacy = true;
        ScoreUpdate(Score.Score.Instance.transform);
        Destroy(gameObject,1.1f);
    }
    
    public void ScoreUpdate(Transform targeTransform)
    {
        transform.DOMove(targeTransform.position, 1);
    }
}
