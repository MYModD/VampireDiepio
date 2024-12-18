using Diepio;
using System;
using UnityEngine;




public class ScoreManager : Singleton<ScoreManager>
{
     public int _score;
    [SerializeField] private int _enemyAddScore;
    [SerializeField] private int _debriAddScore;

    private const string _enemyTag = "Enemy";
    private const string _debriTag = "Debri";

    // イベントシステム
    public Action<int> OnScoreChanged;

    [SerializeField]private ScoreData _scoreData;


    private const string SCOREDETA = "Deta/ScoreData";  // Resourcesフォルダ内のDataフォルダにある場合

    protected override void Awake()
    {
        base.Awake();
        _scoreData = Resources.Load<ScoreData>(SCOREDETA);
        if (_scoreData == null)
        {
            Debug.LogError("ScoreDataの読み込みに失敗しました");
        }
        else
        {
            Debug.Log("ScoreDataの読み込みに成功しました");
        }
        _enemyAddScore = _scoreData.enemyAddScore;
        _debriAddScore = _scoreData.debriAddScore;
    }

    public void AddScore(string objectTag)
    {

        if (objectTag == _enemyTag)
        {
            _score += _enemyAddScore;
            OnScoreChanged.Invoke(_score);
        }
        else if (objectTag == _debriTag)
        {
            _score += _debriAddScore;
            OnScoreChanged.Invoke(_score);
        }
    }
}
