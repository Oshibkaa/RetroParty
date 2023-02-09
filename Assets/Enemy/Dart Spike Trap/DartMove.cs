using System.Collections;
using UnityEngine;

public class DartMove : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f; // ����� ����� �������

    private void OnEnable()
    {
        StartCoroutine("LifeRoutine"); // ���������
    }

    private void OnDisable()
    {
        StopCoroutine("LifeRoutine"); // ���������
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSecondsRealtime(lifeTime); // ����� ��������� �������, ��������� ������
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // ���������
    }    

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 5); // ����� �������� � ��������
        transform.Rotate(0, 0, 10, Space.Self);
    }
}
