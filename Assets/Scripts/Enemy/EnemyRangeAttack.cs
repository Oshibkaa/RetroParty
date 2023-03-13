using UnityEngine;

public class EnemyRangeAttack : AI
{
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioEnemy;

    [Header("Link")]
    [SerializeField] private GameObject _aiBullet;
    [SerializeField] private Transform _firePoint;

    [Header("Options")]
    [SerializeField] private float _attackReset;
    private bool _alreadyAttacked;

    private void Update()
    {
        if (_isAttacking)
        {
            AttackPlayer();
        }
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
}
