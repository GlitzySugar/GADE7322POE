using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.AI.Navigation;
public class WorldGeneration : MonoBehaviour
{
    public static List<GameObject> GeneratedTiles = new List<GameObject>();

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject tilePrefab2, tilePrefab3, tilePrefab4, startObj, endObj;
    //[SerializeField] private GameObject ground;
    private int noiseHeight = 2;
    private float gridOffset = 1.8f;
    private int radius = 16;

    // Start is called before the first frame update
    void Start()
    {
        Path pathGenerator = new Path(radius);
       
        //create a grid of a certain radias that will act as our map 
        for (int x = 0; x < radius; x++)
        {
            for(int z = 0; z < radius; z++)
            {
                //instantiates the barrier around the map 
                GameObject bar = Instantiate(barrier);

                //instantiates the map with node prefabs
                GameObject tile = Instantiate(tilePrefab,
                    new Vector3(x * gridOffset, 1.8f /*GenerateNoise(x, z, 8f) * noiseHeight*/, z  * gridOffset),
                    Quaternion.identity);

               
               
                pathGenerator.AssignTopAndBottomTiles(z,x, tile);
                pathGenerator.AssignLeftAndRightTiles(x, tile);
                GeneratedTiles.Add(tile);
            }
        }

        //World Generated
        pathGenerator.GeneratePath();
       
        //intantiates the path locations with pathprefabs
        foreach(var pObject in pathGenerator.GetGeneratedPath)
        {
            float x = pObject.transform.position.x;
            float y = pObject.transform.position.y; float z = pObject.transform.position.z;
            pObject.SetActive(false);
            GameObject walk = Instantiate(tilePrefab2, new Vector3(x,y,z), Quaternion.identity);
        }
        foreach (var pObject in pathGenerator.GetGeneratedPath2)
        {
            float x = pObject.transform.position.x;
            float y = pObject.transform.position.y; float z = pObject.transform.position.z;
            pObject.SetActive(false);
            GameObject walk = Instantiate(tilePrefab3, new Vector3(x, y, z), Quaternion.identity);
        }
        foreach (var pObject in pathGenerator.GetGeneratedPath3)
        {
            float x = pObject.transform.position.x;
            float y = pObject.transform.position.y; float z = pObject.transform.position.z;
            pObject.SetActive(false);
            GameObject walk = Instantiate(tilePrefab4, new Vector3(x, y, z), Quaternion.identity);
        }

        foreach (var pObject in pathGenerator.GetGeneratedStart)
        {
            float x = pObject.transform.position.x;
            float y = pObject.transform.position.y; float z = pObject.transform.position.z;
            pObject.SetActive(false);
            GameObject walk = Instantiate(startObj, new Vector3(x, y, z), Quaternion.identity);
        }

        foreach (var pObject in pathGenerator.GetGeneratedEnding)
        {
            float x = pObject.transform.position.x;
            float y = pObject.transform.position.y; float z = pObject.transform.position.z;
            pObject.SetActive(false);
            GameObject walk = Instantiate(endObj, new Vector3(x, y, z), Quaternion.identity);
        }
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    //Generates map height (not used cause it looks messy and confuses players)
    private float GenerateNoise(int x, int z, float detailScale)
    {
        float xNoise = (x + this.transform.position.x) / detailScale;
        float zNoise = (z + this.transform.position.z) / detailScale;
        return Mathf.PerlinNoise(xNoise, zNoise);
    }
}