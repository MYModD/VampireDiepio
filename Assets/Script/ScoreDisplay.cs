using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _scoreText;

    [Header("スコアの桁数")]
    [SerializeField,Range(1,10)] private int _scoreDigit = 10;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
    }


    private void OnDestroy()
    {

        // 終了時にScoremangerが生成されるのを防ぐため
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        }
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString($"d{_scoreDigit}");
    }
}
