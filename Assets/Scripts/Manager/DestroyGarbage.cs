using UnityEngine;

public class DestroyGarbage : MonoBehaviour
{
    [SerializeField]
    private float _timeDestroy = 0.5f;

    void LateUpdate()
    {
        Destroy(gameObject, _timeDestroy);
    }
}
