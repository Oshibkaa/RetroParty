using System.Collections;
using UnityEngine;

public class BossAI : AI
{
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioEnemy;

    [Header("Boss Options")]
    [SerializeField] private float _forceFieldRadius = 5f;
    [SerializeField] private int _numberOfMinions = 2;
    [SerializeField] private GameObject _minionPrefab, _iceShieldPrefab;
    [SerializeField] private float _attackReset;
    private bool _alreadyAttacked;

    [Header("Materials")]
    [SerializeField] private MeshRenderer _bossMeshRenderer;
    [SerializeField] private Material _standartMaterial, _frozenMaterial;

    [Header("Link")]
    [SerializeField] private GameObject _aiBullet;
    [SerializeField] private Transform _firePoint;

    private void Awake()
    {
        StartCoroutine(SpecialAttack());
    }

    private void Update()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        if (!_alreadyAttacked && gameObject.activeSelf && gameObject != null)
        {
            _audioEnemy?.ShootAudioPlay();

            GameObject bullet = Instantiate(_aiBullet, _firePoint.position, _firePoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f, ForceMode.Force);

            _agent.SetDestination(transform.position);
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _attackReset);
        }
    }

    public void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(5f);

        if (Random.value < 0.5f)
        {
            ForceFieldAttack();
        }
        else
        {
            SummonAttack();
        }

        StartCoroutine(SpecialAttack());
    }

    public void ForceFieldAttack()
    {
        _bossMeshRenderer.material = _frozenMaterial;
        StartCoroutine(FrozenAttack());
    }

    IEnumerator FrozenAttack()
    {
        yield return new WaitForSeconds(1f);

        _iceShieldPrefab.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _forceFieldRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController player = collider.GetComponent<PlayerController>();
                Health hp = collider.GetComponent<Health>();
                hp.TakeDamage();
                player.SlowingSpeed(2f);
            }
        }

        _iceShieldPrefab.SetActive(false);
        _bossMeshRenderer.material = _standartMaterial;
    }

    public void SummonAttack()
    {
        for (int i = 0; i < _numberOfMinions; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
            Instantiate(_minionPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
