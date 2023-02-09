using UnityEngine;

public class DartSpawn : MonoBehaviour
{
    /*
    [SerializeField] private int poolCount = 1; //кол-во объектов в пуле
    [SerializeField] private bool autoExpand = false; // проверка, если объектов потребуется больше
    [SerializeField] private DartMove dartPrefab; // объект
    [SerializeField] private Transform spawnPos; // позиция спавна

    private float nextSpawnTime = 0f; //след. спавн 
    //private PoolMono<DartMove> pool; // ссылка на пул

    private void Start()
    {
        //pool = new PoolMono<DartMove>(this.dartPrefab, this.poolCount, this.transform);
        //this.pool.autoExpand = this.autoExpand;
    }

    void FixedUpdate()
    {
        if (nextSpawnTime < Time.time) // создание и спавн по времени
        {
            this.CreateDart();
            nextSpawnTime = Time.time + 4;
        }
    }

    private void CreateDart()
    {
        //var dart = this.pool.GetFreeElement(); // создание объекта
        //dart.transform.position = spawnPos.position;
    }
    */
}
