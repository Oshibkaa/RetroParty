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
    private int _bounceValue = 3;

    void DestroySelf()
    {
        Invoke(nameof(DestroySelf), 0f);
        HihgParticle();
        Destroy(_trail);
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth target = other.transform.gameObject.GetComponent<EnemyHealth>();
            target.TakeDamage();

            if (target.CheckHealth() > 0)
            {
                LowParticle();
            }
            if (target.CheckHealth() <= 0)
            {
                HihgParticle();
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
                LowParticle();
            }
            if (target._health <= 0)
            {
                HihgParticle();
            }

            DestroySelf();
        }*/
        if (other.gameObject.CompareTag("Wall"))
        {
            _bounceValue--;
            Debug.Log(_bounceValue);
            if (_bounceValue <= 0)
            {
                Invoke(nameof(DestroySelf), 0f);
                HihgParticle();
            }
        }
        /*else if (other.gameObject.CompareTag("Spike"))
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
        }*/
    }

    private void LowParticle()
    {
        Instantiate(_colorParticle, transform.position, _colorParticle.transform.rotation);
    }

    private void HihgParticle()
    {
        Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
    }
}
