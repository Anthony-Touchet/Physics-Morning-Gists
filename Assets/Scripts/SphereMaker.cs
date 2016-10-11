using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
    Requirements:
    1.Script must be placed on an object.
    2.Must have a prefab for the script. (Advise using a sphere with x,y,z scales of .5 as the prefab)
    
    Instructions:
    1.Apply the script to a GameObject
    2.Click on the object to bring up the inspector.
    3.Enter values and prefab from the inspector.
    4.Hit play, will generate around scene origin point. 
*/

public class SphereMaker : MonoBehaviour
{
    public GameObject prefab;
    public int radius;
    public int verts;
    public int halfCircles;

    public List<Vertex> vertices;

    public struct Vertex
    {
        public Vector3 position;
    }

    // Use this for initialization
    void Start()
    {

        vertices = new List<Vertex>();

        List<Vertex> halfCircleVerts = GenHalfCircleVertexes(verts, radius);
        vertices = GenSphereVerts(verts, halfCircles, halfCircleVerts);

        ShowSphere(vertices);
    }

    List<Vertex> GenHalfCircleVertexes(int p, int rad)  //Make half circle templete
    {
        List<Vertex> verts = new List<Vertex>();

        for (int i = 0; i < p; i++)
        {
            float angle = Mathf.PI * i / (p - 1);
            Vertex temp = new Vertex();
            temp.position = new Vector3(rad * Mathf.Cos(angle), rad * Mathf.Sin(angle), 0);
            verts.Add(temp);
        }
        return verts;
    }

    List<Vertex> GenSphereVerts(int sides, int mirid, List<Vertex> halfCircle)  //make the shpere based on the half circle
    {
        int cont = 0;
        List<Vertex> verts = new List<Vertex>();

        //Lathe Half sphere.
        for (int i = 0; i < mirid; i++)
        {
            float phi = 2.0f * Mathf.PI * (float)i / (float)(mirid);
            for (int j = 0; j < sides; j++, cont++)
            {
                float x = halfCircle[j].position.x;
                float y = halfCircle[j].position.y * Mathf.Cos(phi) - halfCircle[j].position.z * Mathf.Sin(phi);
                float z = halfCircle[j].position.z * Mathf.Cos(phi) + halfCircle[j].position.y * Mathf.Sin(phi);
                Vertex temp = new Vertex();

                temp.position = new Vector3(x, y, z);
                verts.Add(temp);
            }
        }
        return verts;
    }

    void ShowSphere(List<Vertex> verticies)     //No indicies needed, just draw a sphere on each vertex
    {
        foreach (Vertex v in verticies)
        {
            Instantiate(prefab, v.position, Quaternion.identity);
        }
    }
}