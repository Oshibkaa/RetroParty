using System.Collections;
using UnityEngine;

public class AnimationTraps : MonoBehaviour {

    [SerializeField] Animator Trap; //Аниматор для ловушки;
    public int senodsActivate = 2;

    void Awake()
    {
        //извлекаем компонент Аниматора из ловушки;
        Trap = GetComponent<Animator>();
        //начинаем открывать и закрывать ловушку;
        StartCoroutine(OpenCloseTrap());
    }

    IEnumerator OpenCloseTrap()
    {
        //воспроизведение открытия;
        Trap.SetTrigger("open");
        //ждём 2 секунды;
        yield return new WaitForSeconds(senodsActivate);
        //воспроизведение закрытия;
        Trap.SetTrigger("close");
        //ждём 2 секунды;
        yield return new WaitForSeconds(senodsActivate);
        //повторяем снова;
        StartCoroutine(OpenCloseTrap());

    }
}