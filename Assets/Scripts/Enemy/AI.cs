using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [Header("Link")]
    [SerializeField] protected Transform _target;
    [SerializeField] protected NavMeshAgent _agent;

    [Header("Options")]
    [SerializeField] private float _lookRadius = 20f;
    [SerializeField] private float AttackRadius;
    private float _distance;
    private bool _findTarget = false;
    protected bool _isAttacking = false;

    private void Start()
    {
        _target = GameObjectManager.instance.allObjects[0].transform;
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(StartFindPlayerTimer());
    }

    private void FixedUpdate()
    {
        Ray PlayerCenterRay = new(transform.position, transform.forward);
        Ray PlayerLeftRay = new(transform.position + transform.right * -0.2f, transform.forward);
        Ray PlayerRightRay = new(transform.position + transform.right * 0.2f, transform.forward);

        _distance = Vector3.Distance(_target.position, transform.position);

        if (_findTarget == true)
        {
            transform.LookAt(_target);
            _agent.SetDestination(_target.position);

            if (_distance < _lookRadius && gameObject.activeSelf)
            {
                if (Physics.Raycast(PlayerCenterRay, out RaycastHit hit, AttackRadius) && Physics.Raycast(PlayerLeftRay, out RaycastHit hitLeft, AttackRadius)
                && Physics.Raycast(PlayerRightRay, out RaycastHit hitRight, AttackRadius))
                {
                    if (hit.transform.CompareTag("Player") && hitLeft.transform.CompareTag("Player") && hitRight.transform.CompareTag("Player")
                        && gameObject.activeSelf && gameObject != null)
                    {
                        _agent.SetDestination(transform.position);
                        _isAttacking = true;
                    }
                    else
                    {
                        _isAttacking = false;
                    }
                }
            }
        }
    }

    public void SetFindTarget(bool variants)
    {
        _findTarget = variants;
    }

    IEnumerator StartFindPlayerTimer()
    {
        yield return new WaitForSeconds(0.2f);
        _findTarget = true;
    }
}
