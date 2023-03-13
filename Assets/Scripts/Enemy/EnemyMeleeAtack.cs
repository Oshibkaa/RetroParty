using UnityEngine;

public class EnemyMeleeAtack : AI
{
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioMelee;

    [Header("Objects")]
    [SerializeField] private ParticleSystem _explosionParticle;

    [Header("Options")]
    [SerializeField] private float _meleeRadius;
    private bool _alreadyAttackedMelee;

    private void Update()
    {
        if (_isAttacking)
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        if (!_alreadyAttackedMelee && gameObject.activeSelf && gameObject != null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _meleeRadius);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("Player"))
                {
                    _audioMelee?.ShootAudioPlay();
                    collider.GetComponent<Health>().TakeDamage();
                    EnemyHealth target = transform.gameObject.GetComponent<EnemyHealth>();
                    target.TakeDamage();
                    HighParticle();
                    break;
                }
            }
        }
    }

    private void HighParticle()
    {
        Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
    }
}
