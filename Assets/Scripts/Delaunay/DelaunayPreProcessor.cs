using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;

namespace AP
{
    public class DelaunayPreProcessor
    {

        

        /// <summary>
        /// UNTESTED: converts 3d space points into 2d for faster delaunay triangulation.
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static Vector2[] Flatten(Vector3[] vertices)
        {
            Vector2[] returnList = new Vector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                returnList[i] = new Vector2(vertices[i].x, vertices[i].y);
            }

            return returnList;
        }

        /// <summary>
        /// UNTESTED: returns a list of 3d space points
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static Vector3[] Solidify(Vector2[] vertices)
        {
            Vector3[] returnList = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                returnList[i] = new Vector3(vertices[i].x, vertices[i].y, 0);
            }

            return returnList;
        }

        /// <summary>
        /// Every three edges is reversed as follows
        /// 0=2, 1=1, 2=0
        /// Essentially every triangle (assuming it was originally counter clockwise)
        /// is made clockwise without change for edges nor vertices.
        /// </summary>
        /// <param name="triangles"></param>
        /// <returns></returns>
        public static int[] ReverseTriangles(int[] triangles)
        {
            int[] returnTriangles = new int[triangles.Length];

            for (int i = 0; i < triangles.Length; i += 3)
            {
                returnTriangles[i] = triangles[i + 2];
                returnTriangles[i + 1] = triangles[i + 1];
                returnTriangles[i + 2] = triangles[i];
            }

            return returnTriangles;
        }

        public static void CreateFlatFromCloud(Vector3[] vertices, Material creationMaterial)
        {
            DelaunayCalculator dc = new DelaunayCalculator();
            DelaunayTriangulation dt = dc.CalculateTriangulation(Flatten(vertices));

            GameObject newFlat = new GameObject();
                
            newFlat.AddComponent<MeshFilter>();
            newFlat.AddComponent<MeshRenderer>();

            newFlat.GetComponent<MeshFilter>().sharedMesh = new Mesh();

            newFlat.GetComponent<MeshFilter>().sharedMesh.vertices = Solidify(dt.Vertices.ToArray());
            newFlat.GetComponent<MeshFilter>().sharedMesh.triangles = ReverseTriangles(dt.Triangles.ToArray());

            newFlat.GetComponent<MeshRenderer>().material = creationMaterial;


        }
        
        public static void CreateFlatFromCloud(GameObject[] markers, Material creationMaterial)
        {
            Vector3[] vertices = new Vector3[markers.Length];
            for (int i = 0; i < markers.Length; i++)
            {
                vertices[i] = markers[i].transform.position;
            }
            
            DelaunayCalculator dc = new DelaunayCalculator();
            DelaunayTriangulation dt = dc.CalculateTriangulation(Flatten(vertices));

            GameObject newFlat = new GameObject();
                
            newFlat.AddComponent<MeshFilter>();
            newFlat.AddComponent<MeshRenderer>();

            newFlat.GetComponent<MeshFilter>().sharedMesh = new Mesh();

            newFlat.GetComponent<MeshFilter>().sharedMesh.vertices = Solidify(dt.Vertices.ToArray());
            newFlat.GetComponent<MeshFilter>().sharedMesh.triangles = ReverseTriangles(dt.Triangles.ToArray());

            newFlat.GetComponent<MeshRenderer>().material = creationMaterial;


        }

/*
    void Start()
    {
        Mesh borderMesh = border.sharedMesh;
        Vector2[] preDelaunayVertices = Flatten(borderMesh.vertices);
        
        
        
        
        
        DelaunayCalculator delaunayCalculator = new DelaunayCalculator();
        DelaunayTriangulation dt = delaunayCalculator.CalculateTriangulation(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0,1),
            new Vector2(1,0),
            new Vector2(1,1)
        });


        DelaunayTriangulation borderDT = delaunayCalculator.CalculateTriangulation(preDelaunayVertices);
        
        print(borderDT);
        
        GameObject a = new GameObject();
        a.AddComponent<MeshFilter>();
        a.AddComponent<MeshRenderer>();

        a.GetComponent<MeshFilter>().sharedMesh = new Mesh();

        a.GetComponent<MeshFilter>().sharedMesh.vertices = Solidify(borderDT.Vertices.ToArray());
        a.GetComponent<MeshFilter>().sharedMesh.triangles = reverseTriangles(borderDT.Triangles.ToArray());

        a.GetComponent<MeshRenderer>().material = borderMaterial; 
        
        //print(dt.Verify().ToString());

    }
*/
        // Update is called once per frame
        void Update()
        {

        }
    }
}