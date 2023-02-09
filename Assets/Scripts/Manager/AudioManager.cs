using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Shoot")]

    [SerializeField]
    private AudioSource _shootAudioSource;

    [Header("Damage")]

    [SerializeField]
    private AudioSource _damageAudioSource;

    [Header("Death")]

    [SerializeField]
    private AudioSource _deathAudioSource;

    [Header("Dash")]

    [SerializeField]
    private AudioSource _dashAudioSource;

    [Header("Bounce")]

    [SerializeField]
    private AudioSource _bounceAudioSource;

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
