using UnityEngine;

public class RegenerateHp : MonoBehaviour
{
    [Header("Scripts")]
    private Health _playerHpScript;

    [Header("Particle")]
    [SerializeField] private ParticleSystem _explosionPrefab;

    private void Start()
    {
        _playerHpScript = GameObjectManager.instance.allObjects[0].GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHpScript.HealUp();
            Instantiate(_explosionPrefab, transform.position, _explosionPrefab.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
