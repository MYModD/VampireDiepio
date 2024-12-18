using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DebugScore : MonoBehaviour
{
    
    private TextMeshProUGUI _scoreText;
    void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        _scoreText.text = ScoreManager.Instance._score.ToString();
    }

    
   
}
