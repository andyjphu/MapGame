using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Parabox.CSG;

public class Intersect : MonoBehaviour
{
    public GameObject wld;
    public Material setterMat;

    // Start is called before the first frame update
    private void Start()
    {

        
        Model result = CSG.Intersect(wld, gameObject);

        
        var composite = new GameObject();
        composite.AddComponent<MeshFilter>().sharedMesh = result.mesh;
        composite.AddComponent<MeshRenderer>().material = setterMat;
        
        //Worlks

        Model result2 = CSG.Subtract(wld, composite);
        
        var composite2 = new GameObject();
        composite2.AddComponent<MeshFilter>().mesh= result2.mesh;
        composite2.AddComponent<MeshRenderer>().material = setterMat;
  
       
    }

    
    void GenerateBarycentric(GameObject go)
    {
        Mesh m = go.GetComponent<MeshFilter>().sharedMesh;

        if (m == null) return;

        int[] tris = m.triangles;
        int triangleCount = tris.Length;

        Vector3[] mesh_vertices = m.vertices;
        Vector3[] mesh_normals = m.normals;
        Vector2[] mesh_uv = m.uv;

        Vector3[] vertices = new Vector3[triangleCount];
        Vector3[] normals = new Vector3[triangleCount];
        Vector2[] uv = new Vector2[triangleCount];
        Color[] colors = new Color[triangleCount];

        for (int i = 0; i < triangleCount; i++)
        {
            vertices[i] = mesh_vertices[tris[i]];
            normals[i] = mesh_normals[tris[i]];
            uv[i] = mesh_uv[tris[i]];

            colors[i] = i % 3 == 0 ? new Color(1, 0, 0, 0) : (i % 3) == 1 ? new Color(0, 1, 0, 0) : new Color(0, 0, 1, 0);

            tris[i] = i;
        }

        Mesh wireframeMesh = new Mesh();

        wireframeMesh.Clear();
        wireframeMesh.vertices = vertices;
        wireframeMesh.triangles = tris;
        wireframeMesh.normals = normals;
        wireframeMesh.colors = colors;
        wireframeMesh.uv = uv;

        go.GetComponent<MeshFilter>().sharedMesh = wireframeMesh;
    }
    // Update is called once per frame
    private void Update()
    {
    }
}