using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FibonacciSphere : MonoBehaviour
{
    public GameObject toSpawn;
    public GameObject centre;

    public int n = 50;
    private int lastN = 0;

    private float epsilon;

    private readonly float goldenRatio = (1 + Mathf.Pow(5f, 0.5f)) / 2;

    private Vector3[] vertices;

    public float radius = 1;
    private float lastRadius = 0;

    // Start is called before the first frame update
    void Start()
    {
        vertices = new Vector3[n];

        if (n >= 60000)
            epsilon = 214f;
        else if (n >= 400000)
            epsilon = 75f;
        else if (n >= 11000)
            epsilon = 27f;
        else if (n >= 890)
            epsilon = 10f;
        else if (n >= 177)
            epsilon = 3.33f;
        else if (n >= 24)
            epsilon = 1.33f;
        else
            epsilon = 0.33f;

        Instantiate(centre, Vector3.zero, Quaternion.identity);
        GenerateSphere();
    }

    // Update is called once per frame
    void Update()
    {
        if (n != lastN || radius != lastRadius)
        {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Dot");
            for (int i = 0; i < obj.Length; i++)
            {
                Destroy(obj[i]);
            }
            GenerateSphere();
        }

        lastRadius = radius;
        lastN = n;
    }

    public void GenerateSphere()
    {
        vertices = new Vector3[n];
        for (int i = 0; i < n; i++)
        {
            float theta = Mathf.PI * 2 * i / goldenRatio;
            //Mathf.Acos = arccos
            float phi = Mathf.Acos(1 - 2 * (i + epsilon) / (n - 1 + 2 * epsilon));

            float x = Mathf.Cos(theta) * Mathf.Sin(phi);
            float y = Mathf.Sin(theta) * Mathf.Sin(phi);
            float z = Mathf.Cos(phi);

            vertices[i] = radius * new Vector3(x, y, z);
        }

        foreach (Vector3 point in vertices)
        {
            Instantiate(toSpawn, point, Quaternion.identity);
        }
    }
}
