using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;


    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
    }


    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        }
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
