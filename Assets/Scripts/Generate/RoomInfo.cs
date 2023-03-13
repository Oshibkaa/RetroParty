using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector3 UpCameraOffset;
    [SerializeField] private Vector3 RightCameraOffset;
    [SerializeField] private Vector3 DownCameraOffset;
    [SerializeField] private Vector3 LeftCameraOffset;

    [Header("Player")]
    [SerializeField] private Vector3 UpPlayerOffset;
    [SerializeField] private Vector3 RightPlayerOffset;
    [SerializeField] private Vector3 DownPlayerOffset;
    [SerializeField] private Vector3 LeftPlayerOffset;

    #region Camera info
    public Vector3 CheckCameraUpOffset
    {
        get { return UpCameraOffset; }
        set { UpCameraOffset = value; }
    }

    public Vector3 CheckCameraRightOffset
    {
        get { return RightCameraOffset; }
        set { RightCameraOffset = value; }
    }

    public Vector3 CheckCameraDownOffset
    {
        get { return DownCameraOffset; }
        set { DownCameraOffset = value; }
    }

    public Vector3 CheckCameraLeftOffset
    {
        get { return LeftCameraOffset; }
        set { LeftCameraOffset = value; }
    }
    #endregion

    #region Camera info
    public Vector3 CheckPlayerUpOffset
    {
        get { return UpPlayerOffset; }
        set { UpPlayerOffset = value; }
    }

    public Vector3 CheckPlayerRightOffset
    {
        get { return RightPlayerOffset; }
        set { RightPlayerOffset = value; }
    }

    public Vector3 CheckPlayerDownOffset
    {
        get { return DownPlayerOffset; }
        set { DownPlayerOffset = value; }
    }

    public Vector3 CheckPlayerLeftOffset
    {
        get { return LeftPlayerOffset; }
        set { LeftPlayerOffset = value; }
    }
    #endregion
}
