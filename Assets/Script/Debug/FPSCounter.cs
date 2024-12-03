using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI _text;
    private void Awake()
    {
    }

    
    void Update()
    {
        float fps = 1f / Time.deltaTime;
        int fpsSpecial = (int)fps;
        if(fpsSpecial > 99)
        {
            _text.text = $"FPS : {fpsSpecial.ToString()}";

        }
        _text.text = $"FPS : 0{fpsSpecial.ToString()}";


    }
}
