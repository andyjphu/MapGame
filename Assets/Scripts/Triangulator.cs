using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangulator : MonoBehaviour
{
    public Material trigColor; 
    // Start is called before the first frame update
    void TestTriangle()
    {
        GameObject gb = new GameObject();
        gb.AddComponent<MeshFilter>();

        gb.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        
        gb.GetComponent<MeshFilter>().sharedMesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0) };
        gb.GetComponent<MeshFilter>().sharedMesh.triangles = new[] { 0, 1, 2 };

        gb.AddComponent<MeshRenderer>();
        gb.GetComponent<MeshRenderer>().material = trigColor;
        
        gb.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
        gb.GetComponent<MeshFilter>().sharedMesh.RecalculateBounds();


        GameObject gb0 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject gb1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject gb2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        gb0.transform.position = new Vector3(0, 0, 0);
        gb1.transform.position = new Vector3(1, 0, 0);
        gb2.transform.position = new Vector3(0, 0, 1);
    }

    private void Start()
    {
        DelaunayTriangulation dt = new DelaunayTriangulation(); 
        dt.Triangulate(new List<Vector2>()
            { new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1) });
        print(dt.TriangleSet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
