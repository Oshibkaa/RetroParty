using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Object")]

    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private GameObject _trail;
    [SerializeField]
    private ParticleSystem _explosionParticle, _colorParticle;

    [Header("Options")]

    [SerializeField]
    private string _bulletID;

    void DestroySelf()
    {
        Invoke(nameof(DestroySelf), 0.1f);
        Instantiate(_colorParticle, transform.position, _colorParticle.transform.rotation);
        Destroy(_trail);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth target = other.transform.gameObject.GetComponent<EnemyHealth>();
            target.TakeDamage();

            if (target.CheckHealth() > 0)
            {
                Instantiate(_colorParticle, transform.position, _colorParticle.transform.rotation);
            }
            if (target.CheckHealth() <= 0)
            {
                Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            }

            DestroySelf();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Health target = other.transform.gameObject.GetComponent<Health>();
            target.TakeDamage();

            if (_bulletID == "Freeze")
            {
                PlayerController player = other.transform.gameObject.GetComponent<PlayerController>();
                player.SlowingSpeed(2f);
            }

            if (target._health > 0)
            {
                Instantiate(_colorParticle, transform.position, _colorParticle.transform.rotation);
            }
            if (target._health <= 0)
            {
                Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            }

            DestroySelf();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Invoke(nameof(DestroySelf), 0.1f);
        }
        else if (other.gameObject.CompareTag("Spike"))
        {
            Invoke(nameof(DestroySelf), 0.1f);
        }
        else if (other.gameObject.CompareTag("Gate"))
        {
            Invoke(nameof(DestroySelf), 0.1f);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            DestroySelf();
            Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
        }
    }
}
