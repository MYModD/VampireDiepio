using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI _text;

    public float _updateInterval = 0.5f;
    private float _cashTime = 0f;
    private void Awake()
    {
    }

    
    void Update()
    {
        if(Time.time - _cashTime > _updateInterval)
        {
            float fps = 1f / Time.deltaTime;
            int fpsSpecial = (int)fps;
            if (fpsSpecial > 99)
            {
                _text.text = $"FPS : {fpsSpecial.ToString()}";

            }
            else
            {
                _text.text = $"FPS : 0{fpsSpecial.ToString()}";

            }

            _cashTime = Time.time;
        }

        


    }
}
