using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Scripts")]

    [SerializeField]
    private AudioManager _audioPlayer;
    private UIManager _uiManager;

    [Header("HP")]

    public int _health;

    [SerializeField]
    private Material _normalHp, _lowHp;
    [SerializeField]
    private MeshRenderer _playerHPMaterial;

    [Header("Player")]

    [SerializeField]
    private GameObject _playerGameObject;
    [SerializeField]
    private GameObject _skinPlayer;

    void Start()
    {
        _uiManager = GameObjectManager.instance.allObjects[1].GetComponent<UIManager>();
        _playerHPMaterial.material = _normalHp;
    }

    public void CheckHPValue()
    {
        if (_health >= 2)
        {
            _playerHPMaterial.material = _normalHp;
        }
        if (_health == 1)
        {
            _playerHPMaterial.material = _lowHp;
        }
        if (_health <= 0)
        {
            _audioPlayer.DeathAudioPlay();
            _skinPlayer.SetActive(false);

            _playerGameObject.layer = 12;
            Time.timeScale = 0.3f;

            StartCoroutine(ActiveDeathUi());
        }
    }

    IEnumerator ActiveDeathUi()
    {
        yield return new WaitForSeconds(1f);
        _uiManager.Death();
    }

    public void TakeDamage()
    {
        _health--;
        _audioPlayer.TakeDamageAudioPlay();
        CheckHPValue();
    }

    public void HealUp()
    {
        _health++;
        CheckHPValue();
    }
}
