using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Link")]
    [SerializeField] private AudioSource _shootAudioSource, _damageAudioSource, _deathAudioSource, _dashAudioSource, _bounceAudioSource;

    public void ShootAudioPlay()
    {
        _shootAudioSource.volume = Random.Range(0.1f, 0.5f);
        _shootAudioSource.Play();
    }

    public void TakeDamageAudioPlay()
    {
        _damageAudioSource.Play();
    }

    public void DeathAudioPlay()
    {
        _deathAudioSource.Play();
    }

    public void DashAudioPlay()
    {
        _dashAudioSource.Play();
    }

    public void BounceBulletAudioPlay()
    {
        _bounceAudioSource.Play();
    }
}
