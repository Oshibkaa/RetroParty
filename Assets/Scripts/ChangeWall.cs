using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWall : MonoBehaviour
{
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
            _block.SetActive(true);
            _gateMaterial.material = _wallMaterial;
        }
    }
}
