using UnityEngine;

public class MeshGeneration : MonoBehaviour
{

    public int worldX, worldZ;
    //Create mesh to be our new mesh
    private Mesh mesh;

    //Define array needed for mesh
    private int[] triangles;
    private Vector3[] verticies;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateMesh();
        UpdateMesh();
    }
    //Generate mesh
    void GenerateMesh()
    {
        triangles = new int[worldX * worldZ * 6];
        verticies = new Vector3[(worldX + 1) * (worldZ + 1)];

        for (int i = 0, z = 0; z <= worldZ; z++)
        {
            for (int x = 0; x <= worldX; x++)
            {
                verticies[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        int tries = 0;
        int verts = 0;
        for (int z = 0; z < worldZ; z++)
        {
            for (int x = 0; x < worldX; x++)
            {
                triangles[tries + 0] = verts + 0;
                triangles[tries + 1] = verts + worldZ + 1;
                triangles[tries + 2] = verts + 1;

                triangles[tries + 3] = verts + 1;
                triangles[tries + 4] = verts + worldZ + 1;
                triangles[tries + 5] = verts + worldZ + 2;

                verts++;
                tries += 6;
            }
            // assign and load mesh

        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
