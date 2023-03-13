using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void MoveCamera(Vector3 offset)
    {
        transform.position += offset;
    }
}
