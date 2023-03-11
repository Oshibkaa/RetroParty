using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioEnemy;

    [Header("Link")]
    [SerializeField] private GameObject _aiBullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;

    [Header("Options")]
    [SerializeField] private float _lookRadius = 20f;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackReset;

    private float _distance;
    private bool _alreadyAttacked;
    private bool _findTarget;
    private bool _isAttacking;

    private void Start()
    {
        _target = GameObjectManager.instance.allObjects[0].transform;
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(StartFindPlayerTimer());
    }

    private void Update()
    {
        if (_isAttacking)
        {
            AttackPlayer();
        }
    }

    private void FixedUpdate()
    {
        Ray playerCenterRay = new Ray(transform.position, transform.forward);
        Ray playerLeftRay = new Ray(transform.position + transform.right * -0.2f, transform.forward);
        Ray playerRightRay = new Ray(transform.position + transform.right * 0.2f, transform.forward);

        _distance = Vector3.Distance(_target.position, transform.position);

        if (_findTarget)
        {
            if (_distance < _lookRadius && gameObject.activeSelf)
            {
                transform.LookAt(_target);

                if (Physics.Raycast(playerCenterRay, out RaycastHit hit, _attackRadius) &&
                    Physics.Raycast(playerLeftRay, out RaycastHit hitLeft, _attackRadius) &&
                    Physics.Raycast(playerRightRay, out RaycastHit hitRight, _attackRadius))
                {
                    if (hit.transform.CompareTag("Player") && hitLeft.transform.CompareTag("Player") &&
                        hitRight.transform.CompareTag("Player") && gameObject.activeSelf && gameObject != null)
                    {
                        _agent.SetDestination(transform.position);
                        _isAttacking = true;
                    }
                    else
                    {
                        _agent.SetDestination(_target.position);
                        _isAttacking = false;
                    }
                }
            }
        }
    }

    private void AttackPlayer()
    {
        if (!_alreadyAttacked && gameObject.activeSelf && gameObject != null)
        {
            _audioEnemy.ShootAudioPlay();

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

    public void SetFindTarget(bool value)
    {
        _findTarget = value;
    }

    private IEnumerator StartFindPlayerTimer()
    {
        yield return new WaitForSeconds(0.2f);
        _findTarget = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);
    }
}
