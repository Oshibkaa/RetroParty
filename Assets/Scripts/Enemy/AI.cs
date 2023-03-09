using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [Header("Scripts")]

    [SerializeField]
    private AudioManager _audioEnemy;

    [Header("Link")]

    [SerializeField]
    private GameObject AIBullet;
    [SerializeField]
    private Transform FirePoint;
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private NavMeshAgent Agent;

    [Header("Options")]

    [SerializeField]
    private float _lookRadius = 20f;
    [SerializeField]
    private float AttackRadius;
    [SerializeField]
    private float AttackReset;

    private float _distance;
    private bool _alreadyAttacked;
    private bool _findTarget = false;
    private bool _attack = false;

    private void Start()
    {
        Target = GameObjectManager.instance.allObjects[0].transform;
        Agent = GetComponent<NavMeshAgent>();
        StartCoroutine(StartFindPlayerTimer());
    }

    private void Update()
    {
        if (_attack == true)
        {
            AttackPlayer();
        }
    }

    private void FixedUpdate()
    {
        Ray PlayerCenterRay = new(transform.position, transform.forward);
        Ray PlayerLeftRay = new(transform.position + transform.right * -0.2f, transform.forward);
        Ray PlayerRightRay = new(transform.position + transform.right * 0.2f, transform.forward);

        _distance = Vector3.Distance(Target.position, transform.position);

        if (_findTarget == true)
        {
            if (_distance < _lookRadius && gameObject.activeSelf)
            {
                transform.LookAt(Target);

                if (Physics.Raycast(PlayerCenterRay, out RaycastHit hit, AttackRadius) && Physics.Raycast(PlayerLeftRay, out RaycastHit hitLeft, AttackRadius)
                && Physics.Raycast(PlayerRightRay, out RaycastHit hitRight, AttackRadius))
                {
                    if (hit.transform.CompareTag("Player") && hitLeft.transform.CompareTag("Player") && hitRight.transform.CompareTag("Player")
                        && gameObject.activeSelf && gameObject != null)
                    {
                        Agent.SetDestination(transform.position);
                        _attack = true;
                    }
                    else
                    {
                        Agent.SetDestination(Target.position);
                        _attack = false;
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

            GameObject bullet = Instantiate(AIBullet, FirePoint.position, FirePoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f, ForceMode.Force);

            Agent.SetDestination(transform.position);
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), AttackReset);
        }
    }

    public void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    public void BooleanFindValue(bool variants)
    {
        _findTarget = variants;
    }

    IEnumerator StartFindPlayerTimer()
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
