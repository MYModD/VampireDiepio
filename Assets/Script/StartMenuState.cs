using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartMenuState : MonoBehaviour
{
    
    public void OnStartButtonClicked()
    {
        GameStateManager.Instance.ChangeGameStartState();
    }
}
