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

    // �C�x���g�V�X�e��
    public Action<int> OnScoreChanged;

    [SerializeField]private ScoreData _scoreData;


    private const string SCOREDETA = "Deta/ScoreData";  // Resources�t�H���_����Data�t�H���_�ɂ���ꍇ

    protected override void Awake()
    {
        base.Awake();
        _scoreData = Resources.Load<ScoreData>(SCOREDETA);
        if (_scoreData == null)
        {
            Debug.LogError("ScoreData�̓ǂݍ��݂Ɏ��s���܂���");
        }
        else
        {
            Debug.Log("ScoreData�̓ǂݍ��݂ɐ������܂���");
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
