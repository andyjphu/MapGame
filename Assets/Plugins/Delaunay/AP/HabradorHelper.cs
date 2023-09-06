using System.Collections.Generic;
using Unity; 

using Habrador_Computational_Geometry;
using Unity.VisualScripting;
using UnityEngine;

namespace AP
{
    public static class HabradorHelper
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
                returnList[i] = new Vector2(vertices[i].x, vertices[i].z);
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
                returnList[i] = new Vector3(vertices[i].x, 0, vertices[i].y);
            }

            return returnList;
        }
        
        public static MyVector2[] FlattenHabrador (Vector3[] vertices)         {
            MyVector2[] returnList = new MyVector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                returnList[i] = new MyVector2(vertices[i].x, vertices[i].z);
            }

            return returnList;
        }
        
        public static GameObject CreateFlatFromMesh(Mesh mesh, Material mat)
        {
            GameObject newFlat = new GameObject();
                
            newFlat.AddComponent<MeshFilter>();
            newFlat.AddComponent<MeshRenderer>();

            newFlat.GetComponent<MeshFilter>().sharedMesh = mesh;
            newFlat.GetComponent<MeshRenderer>().material = mat;

            return newFlat;

        }

        public static GameObject ConstrainedMeshFromPoints(Vector3[] vertices, Material material, int[] constrainedEdges = null)
        {
            MyVector2[] delaunayVerticesList = FlattenHabrador(vertices);
            
            List<MyVector2> hullVertices = null;
            if (constrainedEdges != null)
            {
                hullVertices = new List<MyVector2>();
                foreach (int edgePoint in constrainedEdges)
                {
                    hullVertices.Add(delaunayVerticesList[edgePoint]);
                }
            }
            
            
            HashSet<MyVector2> delaunayVertices = delaunayVerticesList.ToHashSet();

            HalfEdgeData2 triangulated =
                ConstrainedDelaunaySloan.GenerateTriangulation(delaunayVertices, hullVertices, null, true, new HalfEdgeData2());


            HashSet<Triangle2> triangles = _TransformBetweenDataStructures.HalfEdge2ToTriangle2(triangulated);

            Mesh triangulatedMesh = _TransformBetweenDataStructures.Triangles2ToMesh(triangles, false, 0 );
            
            return CreateFlatFromMesh(triangulatedMesh, material);
            
            

        }
    }
}