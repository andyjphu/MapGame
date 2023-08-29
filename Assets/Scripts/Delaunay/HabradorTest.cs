using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Habrador_Computational_Geometry; 
using AP;


public class HabradorTest : MonoBehaviour
{
    public Material mat; 
    /*private void Start()
    {
        MyVector2[] p = new MyVector2[]
        {
            new MyVector2(0, 0),
            new MyVector2(0, 1),
            new MyVector2(1, 0),
            new MyVector2(1, 1)
        };

        HashSet<MyVector2> h = new HashSet<MyVector2>(p);


        HalfEdgeData2 f = new HalfEdgeData2();
        HalfEdgeData2 e = ConstrainedDelaunaySloan.GenerateTriangulation(h, null, null, false, f);

        HashSet<Triangle2> x = _TransformBetweenDataStructures.HalfEdge2ToTriangle2(e);

        Mesh m = _TransformBetweenDataStructures.Triangles2ToMesh(x, false, 0);
        
        DelaunayPreProcessor.CreateFlatFromMesh(m, mat);

    }*/

    private void Start()
    {
        
        Vector3[] p = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1 ),
            new Vector3(1, 0, 0 ),
            new Vector3(1, 0, 1)
        };
        
        HabradorHelper.ConstrainedMeshFromPoints(p, mat);
    }
}
