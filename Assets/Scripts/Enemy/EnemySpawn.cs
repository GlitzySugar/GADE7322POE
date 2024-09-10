using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //uses the enemy prefab
    [SerializeField] private GameObject enemy;
    // uses gameobject in Start point prefab as a spawn point
    [SerializeField] private Transform spawnPoint;

    // used as a condition for enemies to spawn
    private bool SpawnE = true;


    // Update is called once per frame
    void Start()
    {
        SpawnEnemy();
        StartCoroutine(Spawn());
    }
    //instantiates the enemy
    void SpawnEnemy()
    {
        float x, y, z;
        x = spawnPoint.position.x;
        y = spawnPoint.position.y;
        z = spawnPoint.position.z;

        GameObject enemyObj = Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);

    }
    //Spawns enemys after a certain amount of time
    IEnumerator Spawn()
    {
        while (SpawnE == true)
        {
            yield return new WaitForSeconds(20f);
            SpawnEnemy();
        }
       
    }
}
