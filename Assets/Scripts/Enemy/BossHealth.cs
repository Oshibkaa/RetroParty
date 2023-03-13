using System.Collections;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    [Header("Objects")]
    [SerializeField]
    private GameObject _bossEnemy, _shield;

    void Start()
    {
        DamageEvent += OnDamage;
    }

    void OnDamage()
    {
        if (HealthCheck <= 0)
        {
            UIManager _uiManager = GameObjectManager.instance.allObjects[1].GetComponent<UIManager>();
            _uiManager.Winner();
        }
        else
        {
            StartCoroutine(ThreeSecondsTimer());
        }
    }

    IEnumerator ThreeSecondsTimer()
    {
        _bossEnemy.layer = 12;
        yield return new WaitForSeconds(3f);
        _bossEnemy.layer = 9;
    }
}
