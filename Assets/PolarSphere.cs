using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarSphere : MonoBehaviour
{
    public GameObject toSpawn;
    public GameObject centre;
    public int numHorizontalSegments = 10;
    public int numVerticalSegments = 10;
    private int lastHorizontalSegments = 0;
    private int lastVerticalSegments = 0;

    public float radius = 5f;
    private float lastRadius = 0;

    //Vector2[] newUV;
    //int[] newTriangles;

    private List<Vector3> vertices;

    void Start()
    {
        Instantiate(centre, Vector3.zero, Quaternion.identity);
        GenerateSphere();

        //Vector3[] vertices = new Vector3[originalPoints.Count];
        //for (int i = 0; i < originalPoints.Count; i++)
        //{
        //    vertices[i] = originalPoints[i];
        //}

        //Mesh mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = mesh;
        //mesh.vertices = vertices;
        //mesh.uv = newUV;
        //mesh.triangles = newTriangles;
    }

    void Update()
    {
        Mathf.Clamp(radius, 0.01f, float.MaxValue);
        Mathf.Clamp(numHorizontalSegments, 2, int.MaxValue);
        Mathf.Clamp(numVerticalSegments, 2, int.MaxValue);

        if (radius != lastRadius || numHorizontalSegments != lastHorizontalSegments || numVerticalSegments != lastVerticalSegments)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Dot");
            for (int i = 0; i < obj.Length; i++)
            {
                Destroy(obj[i]);
            }
            GenerateSphere();
            lastHorizontalSegments = numHorizontalSegments;
            lastVerticalSegments = numVerticalSegments;
            lastRadius = radius;
        }
    }

    public void GenerateSphere()
    {
        vertices = new List<Vector3>();

        for (int h = 0; h <= numHorizontalSegments; h++)
        {
            float angle1 = h * (Mathf.PI / numHorizontalSegments);

            for (int v = 0; v < numVerticalSegments; v++)
            {
                float angle2 = v * (2 * Mathf.PI / numVerticalSegments);

                float x = Mathf.Sin(angle1) * Mathf.Cos(angle2);
                float y = Mathf.Cos(angle1);
                float z = Mathf.Sin(angle1) * Mathf.Sin(angle2);

                vertices.Add(new Vector3(x, y, z) * radius);
            }
        }

        foreach (Vector3 point in vertices)
        {
            Instantiate(toSpawn, point, Quaternion.identity);
        }
    }
}
