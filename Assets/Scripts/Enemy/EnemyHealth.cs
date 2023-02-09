using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Scripts")]

    private UIManager _uiManager;
    [SerializeField]
    private AudioManager _audioEnemy;

    [Header("Materials")]

    [SerializeField]
    private MeshRenderer _enemyMeshRenderer;
    [SerializeField]
    private Material _enemyMaterial, _damageMaterial;

    [Header("Objects")]

    [SerializeField]
    private GameObject _bossEnemy;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private Transform _ParticlePoint;
    [SerializeField]
    private ParticleSystem _explosionParticle;
    [SerializeField]
    private GameObject _winUiMenu;

    [Header("Options")]

    [SerializeField]
    private int _healthEnemy;
    [SerializeField]
    private int PointValue;
    [SerializeField]
    private Rigidbody _enemyRb;
    [SerializeField]
    private Transform[] _enemyPosition;
    [SerializeField]
    private string _enemyID;

    void Start()
    {
        _uiManager = GameObjectManager.instance.allObjects[1].GetComponent<UIManager>();
        _enemyMeshRenderer.material = _enemyMaterial;
    }

    public void TakeDamage()
    {
        _healthEnemy--;

        StartCoroutine(Damage());
        _audioEnemy.TakeDamageAudioPlay();

        if (_healthEnemy <= 0)
        {
            _audioEnemy.DeathAudioPlay();
            _uiManager.UpdateScore(PointValue);
            Destroy(gameObject);
        }

        if (_enemyID == "Boss")
        {
            Instantiate(_explosionParticle, _ParticlePoint.position, _explosionParticle.transform.rotation);

            Instantiate(_enemy, _enemyPosition[0].position, _enemy.transform.rotation);
            Instantiate(_enemy, _enemyPosition[1].position, _enemy.transform.rotation);

            _shield.SetActive(true);
            _bossEnemy.layer = 12;

            StartCoroutine(FiveSecondsTimer());
        }
        if (_enemyID == "Boss" && _healthEnemy <= 0)
        {
            _uiManager.Winner();
        }
    }

    public int CheckHealth()
    {
        return _healthEnemy;
    }

    IEnumerator FiveSecondsTimer()
    {
        yield return new WaitForSeconds(5f);
        _shield.SetActive(false);
        _bossEnemy.layer = 9;
    }

    IEnumerator Damage()
    {
        AI target = transform.gameObject.GetComponent<AI>();
        target.BooleanFindValue(false);

        _enemyMeshRenderer.material = _damageMaterial;
        _enemyRb.AddForce(-transform.forward * 200f, ForceMode.Force);

        yield return new WaitForSeconds(0.5f);

        _enemyMeshRenderer.material = _enemyMaterial;

        _enemyRb.isKinematic = true;
        _enemyRb.isKinematic = false;

        target.BooleanFindValue(true);
    }
}
