using UnityEngine;

public class DestroyGarbage : MonoBehaviour
{
    [Header("Options")]
    
    [SerializeField]
    private float _timeToDestruction = 0.5f;

    void LateUpdate()
    {
        Destroy(gameObject, _timeToDestruction);
    }
}
