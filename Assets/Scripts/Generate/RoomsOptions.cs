using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsOptions : MonoBehaviour
{
    [Header("Objects")]
    public GameObject[] UpRooms, RightRooms, DownRooms, LeftRooms;
    public List<GameObject> Rooms;

    private void Start()
    {
        StartCoroutine(BossSpawner());
    }

    IEnumerator BossSpawner()
    {
        yield return new WaitForSeconds(5f);

        var lastRoom = Rooms[Rooms.Count - 1].GetComponent<BlockRoomsDoor>();
        lastRoom.BossSpawn();
    }
}
