using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public Rigidbody RbPlatform;
    private Vector3 _currentPosition;
    private bool _moveingBack;
    private void Start()
    {
        _currentPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_moveingBack == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPosition, 5f * Time.deltaTime);

            if (transform.position.y == _currentPosition.y)
            {
                _moveingBack = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Invoke("FallPlatform", 0.5f);
    }

    void OnCollisionStay(Collision collision)
    {
        Invoke("FallPlatform", 0.5f);
    }

    void OnCollisionExit(Collision collision)
    {
        Invoke("FallPlatform", 0.5f);
    }

    void FallPlatform()
    {
        RbPlatform.isKinematic = false;
        RbPlatform.useGravity = true;
        Invoke("BackPlatform", 1.2f); 
    }

    void BackPlatform()
    {
        RbPlatform.velocity = Vector3.up;
        RbPlatform.isKinematic = true;
        RbPlatform.useGravity = false;
        _moveingBack = true;
    }
}