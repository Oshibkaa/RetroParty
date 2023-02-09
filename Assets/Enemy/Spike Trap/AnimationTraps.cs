using System.Collections;
using UnityEngine;

public class AnimationTraps : MonoBehaviour {

    [SerializeField] 
    Animator _spikeAnimator;
    [SerializeField]
    private float _makeTime = 2f;

    void Awake()
    {
        _spikeAnimator = GetComponent<Animator>();
        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        _spikeAnimator.SetTrigger("open");
        yield return new WaitForSeconds(_makeTime);
        _spikeAnimator.SetTrigger("close");
        yield return new WaitForSeconds(_makeTime);
        StartCoroutine(OpenCloseTrap());

    }
}