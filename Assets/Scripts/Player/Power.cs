using System.Collections;
using UnityEngine;

public class Power : MonoBehaviour
{
    [Header("Script")]

    [SerializeField]
    private PlayerGun _playerGunScript;

    [Header("GameObject")]
    [SerializeField]
    private GameObject _shieldUp;
    [SerializeField]
    private GameObject _unlimitedUp;
    [SerializeField]
    private GameObject _playerGameObject;

    [Header("UI")]
    [SerializeField]
    private GameObject _unlimitedIcon;
    [SerializeField]
    private GameObject _unlimitedIconCoolDown;
    [SerializeField]
    private GameObject _shieldIcon;
    [SerializeField]
    private GameObject _shieldIconCoolDown;

    public void ActivateUnlimited()
    {
        _unlimitedIconCoolDown.SetActive(true);
        _unlimitedIcon.SetActive(false);
        _unlimitedUp.SetActive(true);

        _playerGunScript._shootDelay = 0f;

        StartCoroutine(UnlimitedCooldown());
    }

    public void ActivateShield()
    {
        _shieldIconCoolDown.SetActive(true);
        _shieldIcon.SetActive(false);
        _shieldUp.SetActive(true);

        _playerGameObject.layer = 12;

        StartCoroutine(ShieldCooldown());
    }

    IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(9f);
        StartCoroutine(TimerSkill(_shieldUp));
        yield return new WaitForSeconds(1f);

        _shieldIconCoolDown.SetActive(false);
        _shieldIcon.SetActive(true);
        _playerGameObject.layer = 8;
    }

    IEnumerator UnlimitedCooldown()
    {
        yield return new WaitForSeconds(9f);
        StartCoroutine(TimerSkill(_unlimitedUp));
        yield return new WaitForSeconds(1f);

        _unlimitedIconCoolDown.SetActive(false);
        _unlimitedIcon.SetActive(true);
        _playerGunScript._shootDelay = 0.5f;
    }

    IEnumerator TimerSkill(GameObject GameObject)
    {
        GameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GameObject.GetComponent<MeshRenderer>().enabled = true;
        GameObject.SetActive(false);
    }
}
