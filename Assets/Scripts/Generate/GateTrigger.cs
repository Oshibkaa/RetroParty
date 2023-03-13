using System.Collections;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private GameObject[] _triggerGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ActivetedTrigger());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DisabledTrigger());
        }
    }

    IEnumerator ActivetedTrigger()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _triggerGate.Length; i++)
        {
            _triggerGate[i].SetActive(true);
        }
    }

    IEnumerator DisabledTrigger()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _triggerGate.Length; i++)
        {
            _triggerGate[i].SetActive(false);
        }
    }
}
