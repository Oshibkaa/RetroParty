using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsOptions : MonoBehaviour
{
    BlockRoomsDoor spawnEnemy;
    public GameObject[] UpRooms, RightRooms, DownRooms, LeftRooms;
    public GameObject Boss;
    public List<GameObject> Rooms;

    private int _randomValue;

    private void Start()
    {
        spawnEnemy = GetComponent<BlockRoomsDoor>();
        StartCoroutine(BossSpawner());
    }

    IEnumerator BossSpawner()
    {
        yield return new WaitForSeconds(5f);
        _randomValue = Random.Range(0, Rooms.Count - 2);

        var lastRoom = Rooms[Rooms.Count - 1].GetComponent<BlockRoomsDoor>();
        lastRoom.BossSpawn();
    }
}
