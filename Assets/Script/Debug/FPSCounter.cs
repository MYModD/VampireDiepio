using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI _text;
    private void Awake()
    {
        Debug.Log("hoge");
    }

    
    void Update()
    {
        float fps = 1f / Time.deltaTime;
        int fpsSpecial = (int)fps;
        _text.text =  $"FPS : {fpsSpecial.ToString()}";
        
    }
}
