using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSystemManger : MonoBehaviour
{

    public DefalutEnemyControll _gameobject;
    public Player _player;

    public int enemyCount = 1000; // “G‚Ì”

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var hoge = Instantiate(_gameobject, Random.insideUnitSphere * 10f, Quaternion.identity);
            hoge.player = _player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
