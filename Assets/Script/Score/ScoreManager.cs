using Diepio;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int _score;
    [SerializeField, Tag] private string _enemyTag;
    [SerializeField, Tag] private int _enemyScore;
    [SerializeField, Tag] private string _debriTag;
    [SerializeField, Tag] private int _debriScore;


    // イベントシステム
    public Action<int> OnScoreChanged;

    protected override void Awake()
    {
        base.Awake();

    }

    public void AddScore(string objectTag)
    {
        Debug.Log($"{objectTag} : カウントしてるよ".BoldYellow());

        if (objectTag == _enemyTag)
        {
            _score += _enemyScore;
            OnScoreChanged.Invoke(_score);
        }
        else if (objectTag == _debriTag)
        {
            _score += _debriScore;
            OnScoreChanged.Invoke(_score);
        }
    }
}
