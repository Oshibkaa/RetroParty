using System.Collections;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [Header("Script")]

    [SerializeField]
    private PlayerController _playerScript;
    [SerializeField]
    private AudioManager _audioPlayer;

    [Header("Links")]

    [SerializeField]
    private Animator _mainCamera;

    [Header("GunObjects")]

    [SerializeField]
    private GameObject _reloadObject;
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private TrailRenderer _bulletTrail;
    [SerializeField]
    private LayerMask _mask;
    [SerializeField]
    private ParticleSystem _shootParticle;

    [Header("GunOptions")]

    [SerializeField]
    private float _speed = 100;
    [SerializeField]
    public float _shootDelay = 1.5f;
    [SerializeField]
    private bool _bouncingBullets;
    [SerializeField]
    private float _bounceDistance = 10f;

    private float LastShootTime;

    public void Shoot()
    {
        if (LastShootTime + _shootDelay < Time.time)
        {
            _mainCamera.SetTrigger("Shake");
            _audioPlayer.ShootAudioPlay();

            Vector3 direction = transform.forward;
            TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawnPoint.position, Quaternion.identity);

            if (Physics.Raycast(_bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, _mask) && _bulletTrail != null)
            {
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, _bounceDistance, true));
            }
            else
            {
                StartCoroutine(SpawnTrail(trail, _bulletSpawnPoint.position + direction * 100, Vector3.zero, _bounceDistance, false));
            }
            StartCoroutine(Reload());
            LastShootTime = Time.time;
        }
    }

    IEnumerator Reload()
    {
        _shootParticle.Play();
        _reloadObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        _reloadObject.SetActive(true);
        _shootParticle.Stop();
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BounceDistance, bool MadeImpact)
    {
        if (Trail != null)
        {
            Vector3 startPosition = Trail.transform.position;
            Vector3 direction = (HitPoint - Trail.transform.position).normalized;

            float distance = Vector3.Distance(Trail.transform.position, HitPoint);
            float startingDistance = distance;

            while (distance > 0 && Trail != null)
            {
                Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (distance / startingDistance));
                distance -= Time.deltaTime * _speed;

                yield return null;
            }
            if (Trail != null)
            {
                Trail.transform.position = HitPoint;
            }

            if (MadeImpact)
            {
                if (_bouncingBullets && BounceDistance > 0)
                {
                    Vector3 bounceDirection = Vector3.Reflect(direction, HitNormal);

                    if (Physics.Raycast(HitPoint, bounceDirection, out RaycastHit hit, BounceDistance, _mask))
                    {
                        yield return StartCoroutine(SpawnTrail(
                            Trail,
                            hit.point,
                            hit.normal,
                            BounceDistance - Vector3.Distance(hit.point, HitPoint),
                            true
                        ));
                    }
                    else
                    {
                        yield return StartCoroutine(SpawnTrail(
                            Trail,
                            HitPoint + bounceDirection * BounceDistance,
                            Vector3.zero,
                            0,
                            false
                        ));
                    }
                }
                _audioPlayer.BounceBulletAudioPlay();
            }
        }

        if (Trail != null)
        {
            Destroy(Trail.gameObject, Trail.time);
        }
    }
}
