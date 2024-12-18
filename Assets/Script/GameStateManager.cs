using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameStateManager : Diepio.Singleton<GameStateManager>
{

    public Action<string> GameStart;

    private const string GameStartSceneName = "StartScene";
    private const string GameOverSceneName = "GameOverScene";
    private const string GamePlaySceneName = "VampireDemo";


    public void ChangeGameStartState()
    {
        SceneManager.LoadScene(GamePlaySceneName);
    }

    public void  ChengeGameOverState()
    {
        int score = ScoreManager.Instance._score;

        SceneManager.LoadScene(GameOverSceneName);

    }




}
