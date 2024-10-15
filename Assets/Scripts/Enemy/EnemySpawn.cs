using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    int waveCount = 0;
    int waveMax = 3;
    public static int spawnScore;
    //uses the enemy prefab
    [SerializeField] private GameObject[] enemy = new GameObject[3];
    // uses gameobject in Start point prefab as a spawn point
    [SerializeField] private Transform spawnPoint;

    // used as a condition for enemies to spawn
    private bool SpawnE = true;


    // Update is called once per frame
    void Start()
    {
        //initially spawns the enenmy at the begining of the game 
        SpawnEnemy();
        //spawns the enemy in intervals 
        StartCoroutine(Spawn());
    }
    //instantiates the enemy
    void SpawnEnemy()
    {
        int spawnNum = 0;
        GameObject enemyObj = null;
        float rand = Random.Range(0, 10);
        float x, y, z;
        GameObject[] tempEnemy = enemy;
        int score = spawnScore;
        if(score > 15) { score = 15; }
        x = spawnPoint.position.x;
        y = spawnPoint.position.y;
        z = spawnPoint.position.z;

        //spawns the enmy on the starting node 
        //GameObject enemyObj = Instantiate(enemy, new Vector3(x, y, z), Quaternion.identity);


        while (score > -1 )
        {
            if (score < 0)
            {
                enemyObj = Instantiate(tempEnemy[0], new Vector3(x, y, z), Quaternion.identity);
                spawnNum ++;
                score = score - 2;
            }
            if (score < 10 && spawnScore > 5)
            {
                if (rand > 7)
                {
                    enemyObj = Instantiate(tempEnemy[2], new Vector3(x, y, z), Quaternion.identity);
                    spawnNum++;
                    score = score - 6;
                }
                else
                {
                    enemyObj = Instantiate(tempEnemy[1], new Vector3(x, y, z), Quaternion.identity);
                    spawnNum++;
                    score = score - 4;
                }
            }
            else
            {
                if (rand > 3)
                {
                    enemyObj = Instantiate(tempEnemy[2], new Vector3(x, y, z), Quaternion.identity);
                    spawnNum++;
                    score = score - 6;
                }
                else
                {
                    enemyObj = Instantiate(tempEnemy[1], new Vector3(x, y, z), Quaternion.identity);
                    spawnNum++;
                    score = score - 4;
                }
            }
        }
       
    }
    //Spawns enemys after a certain amount of time
    IEnumerator Spawn()
    {
        while (SpawnE == true)
        {
            if (waveCount < 2)
            {
                yield return new WaitForSeconds(10f);
                SpawnEnemy();
                waveCount++;
            }
            if (waveCount >= 2 && waveCount < 5)
            {
                yield return new WaitForSeconds(15f);
                SpawnEnemy();
                waveCount++;
            }
            else
            {
                yield return new WaitForSeconds(5f);
                SpawnEnemy();
                waveCount++;
            }

        }

    }
}