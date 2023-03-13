using UnityEngine;

public class SwitchingRooms : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private RoomInfo _roomInfo;
    private CameraController _cameraController;
    private PlayerController _playerController;

    [Header("Options")]
    [SerializeField] private int _gateDirection;

    void Start()
    {
        _roomInfo = FindObjectOfType<RoomInfo>();
        _cameraController = GameObjectManager.instance.allObjects[2].GetComponent<CameraController>();
        _playerController = GameObjectManager.instance.allObjects[0].GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_gateDirection)
            {
                case 0:
                    _cameraController.MoveCamera(_roomInfo.CheckCameraUpOffset);
                    _playerController.MovePlayer(_roomInfo.CheckPlayerUpOffset);
                    break;
                case 1:
                    _cameraController.MoveCamera(_roomInfo.CheckCameraRightOffset);
                    _playerController.MovePlayer(_roomInfo.CheckPlayerRightOffset);
                    break;
                case 2:
                    _cameraController.MoveCamera(_roomInfo.CheckCameraDownOffset);
                    _playerController.MovePlayer(_roomInfo.CheckPlayerDownOffset);
                    break;
                case 3:
                    _cameraController.MoveCamera(_roomInfo.CheckCameraLeftOffset);
                    _playerController.MovePlayer(_roomInfo.CheckPlayerLeftOffset);
                    break;
            }
        }
    }
}
