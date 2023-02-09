using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IControlable
{
    [Header("Scripts")]

    [SerializeField]
    private PlayerGun _gunPlayer;
    [SerializeField]
    private Power _skillPlayer;
    [SerializeField]
    private AudioManager _audioPlayer;

    [Header("Objects")]

    [SerializeField]
    private GameObject _firePoint;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private ParticleSystem _dashParticle;

    [Header("Buff/Debuff")]

    [SerializeField]
    private GameObject _freezeDebuff;

    [Header("Options")]

    [SerializeField]
    private float _speed = 6f;
    [SerializeField]
    private float _shootDelay = 0.5f;
    [SerializeField]
    private float _dash = 500f;

    private Vector3 _direction;
    private float _lastDashTime;

    private void FixedUpdate()
    {
        Move();
        RotationLogic();
    }

    public void MovementLogic(Vector3 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        transform.Translate(_speed * Time.fixedDeltaTime * _direction);
    }

    public void RotationLogic()
    {
        RaycastHit _hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            _firePoint.transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

    public void ActiveShield()
    {
        _skillPlayer.ActivateShield();
    }

    public void ActiveUnlimited()
    {
        _skillPlayer.ActivateUnlimited();
    }

    public void DashLogic()
    {
        if (_lastDashTime + _shootDelay < Time.time)
        {
            if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Dash(new Vector3(0, 0, 1)));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(new Vector3(-1, 0, 0)));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Dash(new Vector3(0, 0, -1)));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(new Vector3(1, 0, 0)));
            }

            _lastDashTime = Time.time;
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        _audioPlayer.DashAudioPlay();

        _rigidbody.AddForce(direction * _dash, ForceMode.Force);
        _dashParticle.Play();
        yield return new WaitForSeconds(0.2f);
        _dashParticle.Stop();

        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    public void ShootLogic()
    {
        _gunPlayer.Shoot();
    }

    public void SlowingSpeed(float speedValue)
    {
        _speed = speedValue;
        _dash = 250f;
        _freezeDebuff.SetActive(true);
        StartCoroutine(FiveSecondsTimer());
    }

    IEnumerator FiveSecondsTimer()
    {
        yield return new WaitForSeconds(5f);
        _freezeDebuff.SetActive(false);
        _dash = 500f;
        _speed = 6f;
    }
}
