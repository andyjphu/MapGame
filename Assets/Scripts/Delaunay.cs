using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;

public class Delaunay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DelaunayCalculator delaunayCalculator = new DelaunayCalculator();
        DelaunayTriangulation dt = delaunayCalculator.CalculateTriangulation(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(1,1)
        });
        
        print(dt);
        print(dt.Verify().ToString());

        GameObject a = GameObject.CreatePrimitive(PrimitiveType.Quad);

        
        int i = 0;
        foreach (Vector3 vert in a.GetComponent<MeshFilter>().sharedMesh.vertices)
        {
            print($"{i.ToString()}  {vert.x.ToString()} {vert.y.ToString()}");
            i++;
        }
        
        i = 0;
        foreach (int what in a.GetComponent<MeshFilter>().sharedMesh.triangles)
        {
            print($"{i.ToString()}  {what.ToString()}");
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
