using UnityEngine;

public class SpikeDAmage : MonoBehaviour
{
    [Header("Object")]

    [SerializeField] private ParticleSystem _explosionParticle, _colorParticle;
    [SerializeField] private Transform _transformParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health target = other.transform.gameObject.GetComponent<Health>();
            target.TakeDamage();

            if (target.HealthCheck > 0)
            {
                Instantiate(_colorParticle, _transformParticle.position, _colorParticle.transform.rotation);
            }
            if (target.HealthCheck <= 0)
            {
                Instantiate(_explosionParticle, _transformParticle.position, _explosionParticle.transform.rotation);
            }
        }
    }
}
