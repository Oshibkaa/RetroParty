using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingRooms : MonoBehaviour
{
    [Header("Link")]

    private Transform _camera;
    private Transform _player;

    [Header("Options")]

    [SerializeField]
    private int _gateDirection;

    void Start()
    {
        _camera = GameObjectManager.instance.allObjects[2].transform;
        _player = GameObjectManager.instance.allObjects[0].transform;
        _camera.transform.position = new Vector3(0f, 7.35f, -1.75f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_gateDirection)
            {
                case 0: //North
                    _camera.transform.position += new Vector3(0f, 0f, 13.3f);
                    _player.transform.position += new Vector3(0f, 0f, 3f);
                    break;
                case 1: //East
                    _camera.transform.position += new Vector3(19.55f, 0f, 0f);
                    _player.transform.position += new Vector3(3f, 0f, 0f);
                    break;
                case 2: //South
                    _camera.transform.position += new Vector3(0f, 0f, -13.3f);
                    _player.transform.position += new Vector3(0f, 0f, -3f);
                    break;
                case 3: //West
                    _camera.transform.position += new Vector3(-19.55f, 0f, 0f);
                    _player.transform.position += new Vector3(-3f, 0f, 0f);
                    break;
            }
        }
    }
}