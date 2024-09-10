using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreceduralGene : MonoBehaviour
{
    public static List<GameObject> GeneratedTiles = new List<GameObject>();

    public GameObject objectToSpawn;
    public GameObject blockGameObject;

    private int mapSizeX = 26;
    private int mapSizeZ = 26;
    private float gridOffset = 1.0f;
    private int noiseHeight = 3;

    private List<Vector3> blockPositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
       CreateGrid();
        SpawnObject();
    }
    //Creates the grid that serves as the terrain
    private void CreateGrid()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                Vector3 pos = new Vector3(x * gridOffset,
                    GenerateNoise(x,z,8f) * noiseHeight,
                    z * gridOffset);

                GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity) as GameObject;
                blockPositions.Add(block.transform.position);

                block.transform.SetParent(this.transform);
            }
        }
    }
    private float GenerateNoise(int x ,int z,  float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.z) / detailScale;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }

    private Vector3 ObjectToSpawnLocation()
    {
        int rndIndex = Random.Range(0, blockPositions.Count);

        Vector3 newPos = new Vector3(
            blockPositions[rndIndex].x,
            blockPositions[rndIndex].y + 0.5f,
             blockPositions[rndIndex].z
            );

        blockPositions.RemoveAt(rndIndex);
        return newPos;
    }

    private void SpawnObject()
    {
        for( int i = 0;i < 20 ;i++)
        {
            GameObject toPlaceObject = Instantiate(objectToSpawn, ObjectToSpawnLocation(), Quaternion.identity);
        }
    }
    

  
}  
