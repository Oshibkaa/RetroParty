using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnLevelScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyTraine;
    [SerializeField] private Transform[] _enemyPosition;
    private int _enemyCount;

    private void Update()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (_enemyCount == 0)
        {
            Spawn();
        }    
    }

    private void Spawn()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(_enemyTraine[Random.Range(0, _enemyTraine.Length)], _enemyPosition[i].transform.position, Quaternion.identity);
        }
    }
}
