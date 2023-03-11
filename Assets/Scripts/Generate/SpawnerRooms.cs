using UnityEngine;

public class SpawnerRooms : MonoBehaviour
{
    [Header("Links")]
    private RoomsOptions _options;

    [Header("Options")]
    [SerializeField] private Direction _direction;
    private bool _isSpawned = false;
    private int _randomValue;
    private float _waitTime = 2f;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private void Start()
    {
        _options = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsOptions>();
        Destroy(gameObject, _waitTime);
        Invoke(nameof(Spawn),Random.Range(0.5f, 1.5f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
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
}
