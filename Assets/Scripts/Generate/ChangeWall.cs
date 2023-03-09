using System.Collections;
using UnityEngine;

public class ChangeWall : MonoBehaviour
{
    [Header("Objects")]

    [SerializeField]
    private GameObject _block;
    [SerializeField]
    private MeshRenderer _gateMaterial;
    [SerializeField]
    private Material _wallMaterial;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            //StartCoroutine(OneSecondsTimer());
        }
    }

    IEnumerator OneSecondsTimer()
    {
        yield return new WaitForSeconds(3f);
        _block.SetActive(true);
        _gateMaterial.material = _wallMaterial;
    }
}
