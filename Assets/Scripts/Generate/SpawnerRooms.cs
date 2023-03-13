using UnityEngine;

public class SpawnerRooms : MonoBehaviour
{
    [Header("Links")]
    private RoomsOptions _options;

    [Header("Options")]
    [SerializeField] private Direction _direction;
    private bool _isSpawned = false;
    private int _randomValue;
    private float _waitTime = 1.5f;

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
        Invoke(nameof(Spawn),Random.Range(0.2f, 1.4f));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("None"))
        {
            Destroy(gameObject);
        }
    }

     public void Spawn()
    {
        if (!_isSpawned)
        {
            if (_options.Rooms.Count < 9)
            {
                if (_direction == Direction.Up)
                {
                    _randomValue = Random.Range(0, _options.UpRooms.Length);
                    GameObject room = Instantiate(_options.UpRooms[_randomValue], transform.position, _options.UpRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
                else if (_direction == Direction.Down)
                {
                    _randomValue = Random.Range(0, _options.DownRooms.Length);
                    GameObject room = Instantiate(_options.DownRooms[_randomValue], transform.position, _options.DownRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
                else if(_direction == Direction.Left)
                {
                    _randomValue = Random.Range(0, _options.LeftRooms.Length);
                    GameObject room = Instantiate(_options.LeftRooms[_randomValue], transform.position, _options.LeftRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
                else if(_direction == Direction.Right)
                {
                    _randomValue = Random.Range(0, _options.RightRooms.Length);
                    GameObject room = Instantiate(_options.RightRooms[_randomValue], transform.position, _options.RightRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
            }
            else
            {
                if (_direction == Direction.Up)
                {
                    _randomValue = 0;
                    GameObject room = Instantiate(_options.CloseUpRooms[_randomValue], transform.position, _options.CloseUpRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
                else if (_direction == Direction.Down)
                {
                    _randomValue = 0;
                    GameObject room = Instantiate(_options.CloseDownRooms[_randomValue], transform.position, _options.CloseDownRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
                else if (_direction == Direction.Left)
                {
                    _randomValue = 0;
                    GameObject room = Instantiate(_options.CloseLeftRooms[_randomValue], transform.position, _options.CloseLeftRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
                else if (_direction == Direction.Right)
                {
                    _randomValue = 0;
                    GameObject room = Instantiate(_options.CloseRightRooms[_randomValue], transform.position, _options.CloseRightRooms[_randomValue].transform.rotation);
                    room.transform.parent = _options.transform;
                }
            }
        }
        _isSpawned = true;
    }
}
