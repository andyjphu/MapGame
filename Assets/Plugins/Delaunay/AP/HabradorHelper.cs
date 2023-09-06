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
        ///
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public static Vector2[] GKFlatten(Vector3[] vertices)
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
        public static Vector3[] GKSolidify(Vector2[] vertices)
        {
            Vector3[] returnList = new Vector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                returnList[i] = new Vector3(vertices[i].x, 0, vertices[i].y);
            }

            return returnList;
        }
        
        /// <summary>
        /// The following is a simple binding to convert my 3d points into 2d points for delaunay triangulation
        /// (Throws away the y axis of the 3d object)
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates a Sloan-based delaunay triangulation without constrained edges
        ///
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public static GameObject MeshFromPoints(Vector3[] vertices, Material material)
        {
            MyVector2[] delaunayVerticesList = FlattenHabrador(vertices);
            
            List<MyVector2> hullVertices = null;
            
            HashSet<MyVector2> delaunayVertices = delaunayVerticesList.ToHashSet();

            HalfEdgeData2 triangulated =
                ConstrainedDelaunaySloan.GenerateTriangulation(delaunayVertices, null, null, true, new HalfEdgeData2());
            //TODO: if performance is an issue, replace the above with a non constrained delaunay (habrador)

            HashSet<Triangle2> triangles = _TransformBetweenDataStructures.HalfEdge2ToTriangle2(triangulated);

            Mesh triangulatedMesh = _TransformBetweenDataStructures.Triangles2ToMesh(triangles, false, 0 );


            Debug.Log("Actual Result has " + triangulatedMesh.triangles.Length);
            return CreateFlatFromMesh(triangulatedMesh, material);
            
            

        }

        
        
        public static int[] RotateConstrainedEdges(int[] outerEdges, int rotations)
        {

            //return outerEdges[(rotations * 2)..outerEdges.Length].Concat(outerEdges[0..(rotations * 2)]);

            int[] returnArray = new int[outerEdges.Length];
            
            outerEdges[(rotations * 2)..outerEdges.Length].CopyTo(returnArray,0);
            outerEdges[0..(rotations * 2)].CopyTo(returnArray,rotations*2);
            return returnArray;
        }
        
        /// <summary>
        /// Looks for the triangulation with the most triangles
        /// (IT CANNOT ACCEPT ConstrainedEdges = null)
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="material"></param>
        /// <param name="constrainedEdges"></param>
        /// <returns></returns>
        public static GameObject MaxConstrainedMeshFromPoints(Vector3[] vertices, Material material, int[] constrainedEdges)
        {

            MyVector2[] delaunayVerticesList = FlattenHabrador(vertices);
            HashSet<MyVector2> delaunayVertices = delaunayVerticesList.ToHashSet();

            int maximumTriangles = -1;

            HashSet<Triangle2> triangles;
            HashSet<Triangle2> bestTriangles; 
            
            
            List<MyVector2> hullVertices = new List<MyVector2>();
            foreach (int edgePoint in constrainedEdges)
            {
                hullVertices.Add(delaunayVerticesList[edgePoint]);
            }
            
            for (int j = 0; j < constrainedEdges.Length/2; j++){
                HalfEdgeData2 triangulated =
                    ConstrainedDelaunaySloan.GenerateTriangulation(delaunayVertices, hullVertices, null, true,
                        new HalfEdgeData2());

                //TODO: place for performance improvement by getting triangles from HalfEdgeData2 directly
                triangles = _TransformBetweenDataStructures.HalfEdge2ToTriangle2(triangulated);
                
                if (triangles.Count > maximumTriangles)
                {
                    bestTriangles = triangles;
                    maximumTriangles = triangles.Count;
                }

                hullVertices = RotateConstrainedEdges(constrainedEdges, 1);




            }
            
//https://stackoverflow.com/questions/21122143/what-is-the-time-complexity-for-copying-list-back-to-arrays-and-vice-versa-in-ja      

            Mesh triangulatedMesh = _TransformBetweenDataStructures.Triangles2ToMesh(triangles, false, 0 );


            Debug.Log("Actual Result has " + triangulatedMesh.triangles.Length);
            return CreateFlatFromMesh(triangulatedMesh, material);
            
            

        }
        
        
    }
}