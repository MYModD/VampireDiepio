using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _multiplayValue = 1f;

    private float _movePostionX;
    private float _movePostionY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        _movePostionX = Input.GetAxis("Horizontal") * Time.deltaTime * _multiplayValue;
        _movePostionY = Input.GetAxis("Vertical") * Time.deltaTime * _multiplayValue;

        transform.position += new Vector3(_movePostionX, _movePostionY,0);
    }
}
