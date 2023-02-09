using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    private float _lastDashTime;
    private float _dash = 500f;
    private bool _isPlayerDeath = false;
    private bool _isPlayerPause = false;

    void Update()
    {
        if (_isPlayerDeath == false || _isPlayerPause == false)
        {
            MovementLogic();
            RotationLogic();
            ShootLogic();
            DashController();
            SkillController();
        }
    }

    public void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(_speed * Time.fixedDeltaTime * movement);
    }

    private void RotationLogic()
    {
        RaycastHit _hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            _firePoint.transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

    private void SkillController()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _skillPlayer.ActivateShield();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            _skillPlayer.ActivateUnlimited();
        }
    }

    private void DashController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_lastDashTime + _shootDelay < Time.time)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    StartCoroutine(Dash(new Vector3(0,0,1)));
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    StartCoroutine(Dash(new Vector3(-1, 0, 0)));
                }
                else if(Input.GetKey(KeyCode.S))
                {
                    StartCoroutine(Dash(new Vector3(0, 0, -1)));
                }
                else if(Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(Dash(new Vector3(1, 0, 0)));
                }

                _lastDashTime = Time.time;
            }
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        _audioPlayer.DashAudioPlay();

        _player.layer = 12;

        _rigidbody.AddForce(direction * _dash, ForceMode.Force);
        _dashParticle.Play();
        yield return new WaitForSeconds(0.2f);
        _dashParticle.Stop();
        _player.layer = 8;

        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
    }

    private void ShootLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gunPlayer.Shoot();
        }
    }

    public void SlowingSpeed(float speedValue)
    {
        _speed = speedValue;
        _dash = 250f;
        _freezeDebuff.SetActive(true);
        StartCoroutine(FiveSecTimer());
    }

    IEnumerator FiveSecTimer()
    {
        yield return new WaitForSeconds(5f);
        _freezeDebuff.SetActive(false);
        _dash = 500f;
        _speed = 6f;
    }

    public void PlayerDeath(bool variants)
    {
        _isPlayerDeath = variants;
    }

    public void PlayerPause(bool variants)
    {
        _isPlayerPause = variants;
        _speed = 0f;
    }

    public void PlayerResume()
    {
        _isPlayerPause = false;
        _speed = 6f;
    }
}
