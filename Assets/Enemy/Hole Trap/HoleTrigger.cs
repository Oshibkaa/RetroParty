using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameObjectManager.instance.allObjects[1].layer == 3)
        {
            GameObjectManager.instance.allObjects[1].layer = 6;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameObjectManager.instance.allObjects[1].layer == 3)
        {
            GameObjectManager.instance.allObjects[1].layer = 6;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObjectManager.instance.allObjects[1].layer = 3;
    }
}