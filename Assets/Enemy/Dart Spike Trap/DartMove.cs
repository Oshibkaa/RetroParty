using System.Collections;
using UnityEngine;

public class DartMove : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f; // время жизни объекта

    private void OnEnable()
    {
        StartCoroutine("LifeRoutine"); // запускаем
    }

    private void OnDisable()
    {
        StopCoroutine("LifeRoutine"); // выключаем
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(lifeTime); // после окончание времени, отключаем объект
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // отключаем
    }    

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5); // задаём движение и кручение
        transform.Rotate(0, 0, 10, Space.Self);
    }
}
