using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameStart : MonoBehaviour
{
   

    
    public void GameStartButton()
    {
        Debug.Log("��������");
        GameStateManager.Instance.ChangeGameStartState();
    }
}
