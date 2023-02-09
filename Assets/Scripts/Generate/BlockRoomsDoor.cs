using System.Collections;
using UnityEngine;

public class BlockRoomsDoor : MonoBehaviour
{
    private RoomsOptions _options;

    [Header("Gate")]

    [SerializeField]
    private GameObject[] _blockGate;
    [SerializeField]
    private MeshRenderer[] _gateMaterial;
    [SerializeField]
    private Material _blue;

    [Header("Obstacle")]
    [SerializeField]
    private GameObject[] _obstaclesVariants;

    [Header("Enemy")]

    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private int _waveValue;

    [Header("Enemy Spawn Point")]

    [SerializeField]
    private Transform[] _spawnEnemyPoint;

    [Header("Object")]

    [SerializeField]
    private GameObject _hpPrefab;

    [Header("Options")]

    private bool _isBossRoom = false;
    private int _enemyCount;
    private bool _spawned = false;
    private bool _enemysNull = false;

    private void Start()
    {
        _options = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomsOptions>();
        _options.Rooms.Add(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_spawned)
        {
            _spawned = true;

            for (int i = 0; i < _blockGate.Length; i++)
            {
                _blockGate[i].SetActive(true);
            }

            if (!_isBossRoom)
            {
                _obstaclesVariants[Random.Range(0, _obstaclesVariants.Length)].SetActive(true);
                SpawnEnemy();
            }
            else
            {
                _obstaclesVariants[0].SetActive(true);
                SpawnBoss();
            }

            StartCoroutine(CheckEnemys());
            StartCoroutine(CheckEnemysLength());
        }
    }

    IEnumerator CheckEnemysLength()
    {
        if (_enemysNull == false)
        {
            _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            yield return new WaitForSeconds(1f);
            StartCoroutine(CheckEnemysLength());
        }
    }

    IEnumerator CheckEnemys()
    {
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => _enemyCount == 0);
        _enemysNull = true;
        OpenDoors();
    }

    private void SpawnEnemy()
    {
        _waveValue = Random.Range(3, 7);
        for (int i = 0; i < _waveValue; i++)
        {
            Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)], _spawnEnemyPoint[Random.Range(0, _spawnEnemyPoint.Length - 1)].position, Quaternion.identity);
        }

        if (_waveValue == 3)
        {
            Instantiate(_hpPrefab, _spawnEnemyPoint[4].position, Quaternion.identity);
        }
    }

    public void BossSpawn()
    {
        _isBossRoom = true;
    }

    private void SpawnBoss()
    {
        Instantiate(_bossPrefab, _spawnEnemyPoint[4].position, Quaternion.identity);
        Instantiate(_enemyPrefab[1], _spawnEnemyPoint[2].position, Quaternion.identity);
        Instantiate(_enemyPrefab[1], _spawnEnemyPoint[3].position, Quaternion.identity);
    }

    private void OpenDoors()
    {
        for (int i = 0; i < _gateMaterial.Length; i++)
        {
            _gateMaterial[i].material = _blue;
        }
        for (int i = 0; i < _blockGate.Length; i++)
        {
            _blockGate[i].SetActive(false);
        }
    }
}
