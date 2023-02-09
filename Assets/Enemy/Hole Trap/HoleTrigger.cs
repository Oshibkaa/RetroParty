using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    Power shieldUp;

    private void Start()
    {
        shieldUp = FindObjectOfType<Power>();
    }

    private void FixedUpdate()
    {
        /*if (shieldUp.HasShieldUp == true)
        {
            GameObjectManager.instance.allObjects[1].layer = 9;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameObjectManager.instance.allObjects[1].layer == 3)
        {
            GameObjectManager.instance.allObjects[1].layer = 6; //меняем слой при попадании в триггер
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameObjectManager.instance.allObjects[1].layer == 3)
        {
            GameObjectManager.instance.allObjects[1].layer = 6; //меняем слой при попадании в триггер
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObjectManager.instance.allObjects[1].layer = 3;
    }
}