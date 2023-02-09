using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRooms : MonoBehaviour
{
    [SerializeField]
    private Direction _direction;
    [SerializeField]
    private GameObject _bossPrefab;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    private RoomsOptions _options;
    private int _randomValue;
    private bool _isSpawned = false;
    private float _waitTime = 2f;

    private void Start()
    {
        _options = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsOptions>();
        Destroy(gameObject, _waitTime);
        Invoke(nameof(Spawn), 0.2f);
    }

    public void Spawn()
    {
        if (!_isSpawned)
        {
            if (_direction == Direction.Up)
            {
                _randomValue = Random.Range(0, _options.UpRooms.Length);
                Instantiate(_options.UpRooms[_randomValue], transform.position, _options.UpRooms[_randomValue].transform.rotation);
            }
            else if (_direction == Direction.Down)
            {
                _randomValue = Random.Range(0, _options.DownRooms.Length);
                Instantiate(_options.DownRooms[_randomValue], transform.position, _options.DownRooms[_randomValue].transform.rotation);
            }
            else if(_direction == Direction.Left)
            {
                _randomValue = Random.Range(0, _options.LeftRooms.Length);
                Instantiate(_options.LeftRooms[_randomValue], transform.position, _options.LeftRooms[_randomValue].transform.rotation);
            }
            else if(_direction == Direction.Right)
            {
                _randomValue = Random.Range(0, _options.RightRooms.Length);
                Instantiate(_options.RightRooms[_randomValue], transform.position, _options.RightRooms[_randomValue].transform.rotation);
            }
        }
        _isSpawned = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SpawnPoint") && other.GetComponent<SpawnerRooms>()._isSpawned)
        {
            Destroy(gameObject);
        }
    }
}
