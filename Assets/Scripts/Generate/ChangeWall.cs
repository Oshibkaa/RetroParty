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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _block.SetActive(true);
            _gateMaterial.material = _wallMaterial;
        }
    }
}
