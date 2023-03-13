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

    [Header("Options")]
    [SerializeField] private int _healthEnemy, _pointValue;
    [SerializeField] private Rigidbody _enemyRb;

    public delegate void DamageEventHandler();
    public event DamageEventHandler DamageEvent;

    void Start()
    {
        _uiManager = GameObjectManager.instance.allObjects[1].GetComponent<UIManager>();
        _enemyMeshRenderer.material = _enemyMaterial;

        if (_uiManager == null)
            _uiManager = FindObjectOfType<UIManager>();
        if (_audioEnemy == null)
            _audioEnemy = FindObjectOfType<AudioManager>();
    }

    public void TakeDamage()
    {
        _healthEnemy--;

        StartCoroutine(Damage());
        _audioEnemy?.TakeDamageAudioPlay();

        if (DamageEvent != null)
            DamageEvent();

        if (_healthEnemy <= 0)
        {
            _audioEnemy?.DeathAudioPlay();
            _uiManager?.UpdateScore(_pointValue);
            Destroy(gameObject);
        }
    }

    public int HealthCheck
    {
        get { return _healthEnemy; }
        protected set { _healthEnemy = value; }
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
