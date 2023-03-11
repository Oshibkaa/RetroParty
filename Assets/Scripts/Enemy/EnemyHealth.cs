using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioEnemy;
    private UIManager _uiManager;

    [Header("Materials")]
    [SerializeField] private MeshRenderer _enemyMeshRenderer;
    [SerializeField] private Material _enemyMaterial, _damageMaterial;

    [Header("Objects")]
    [SerializeField]
    private GameObject _bossEnemy, _enemy, _shield, _winUiMenu;
    [SerializeField]
    private Transform _ParticlePoint;
    [SerializeField]
    private ParticleSystem _explosionParticle;

    [Header("Options")]
    [SerializeField] private int _healthEnemy, _pointValue;
    [SerializeField] private Rigidbody _enemyRb;
    [SerializeField] private Transform[] _enemyPosition;
    [SerializeField] private string _enemyID;

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
            _uiManager.UpdateScore(_pointValue);
            Destroy(gameObject);
        }

        if (_enemyID == "Boss")
        {
            Instantiate(_explosionParticle, _ParticlePoint.position, _explosionParticle.transform.rotation);
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
        target.SetFindTarget(false);

        _enemyMeshRenderer.material = _damageMaterial;
        _enemyRb.AddForce(-transform.forward * 200f, ForceMode.Force);

        yield return new WaitForSeconds(0.5f);

        _enemyMeshRenderer.material = _enemyMaterial;

        _enemyRb.isKinematic = true;
        _enemyRb.isKinematic = false;

        target.SetFindTarget(true);
    }
}
