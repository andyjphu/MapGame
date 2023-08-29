//This script detects mouse clicks on a plane using Plane.Raycast.
//In this example, the plane is set to the Camera's x and y position, but you can set the z position so the plane is in front of your Camera.
//The normal of the plane is set to facing forward so it is facing the Camera, but you can change this to suit your own needs.

//In your GameObject's Inspector, set your clickable distance and attach a cube GameObject in the appropriate fields

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using AP;
using GK;

public class DelaunayDraw : MonoBehaviour
{
    //Attach a cube GameObject in the Inspector before entering Play Mode
    public GameObject m_Cube;
 
    public Material newBorderMaterial; 
    public GameObject m_Plane;

    
    private List<Vector3> newBorder = new List<Vector3>(); // Point Cloud
    private List<GameObject> tempMarkers = new List<GameObject>();
    private List<int> outerEdges = new List<int>();
    private int edgeCounter = -1; 
    
    
    private RaycastHit lastHit = new RaycastHit(); 
    //Vector3 m_DistanceFromCamera;

    void Start()
    {
        
    }

    void Update()
    {
        
        if (true)
        {
            //Create a ray from the Mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (m_Plane.GetComponent<MeshCollider>().Raycast(ray, out RaycastHit hitPoint, 10000))
            {

                m_Cube.transform.position = new Vector3(
                    Mathf.RoundToInt(hitPoint.point.x), 
                    0, 
                    Mathf.RoundToInt(hitPoint.point.z)
                    );

                lastHit = hitPoint;
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            GameObject wayMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wayMarker.transform.position = new Vector3(lastHit.point.x, 0, lastHit.point.z);

            if (edgeCounter >= 0 )
            {
                outerEdges.Add(edgeCounter);
                outerEdges.Add(edgeCounter + 1);
            }
            
            tempMarkers.Add(wayMarker);
            newBorder.Add(wayMarker.transform.position);
            edgeCounter += 1; 
            
            



        }

        if (Input.GetMouseButtonUp(1))
        {
            //DelaunayPreProcessor.CreateFlatFromCloud(newBorder.ToArray(), newBorderMaterial);
            outerEdges.Add(edgeCounter);
            outerEdges.Add(0);

            int[] oe = outerEdges.ToArray();
            tempMarkers.Add(HabradorHelper.ConstrainedMeshFromPoints(newBorder.ToArray(), newBorderMaterial, oe));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            newBorder.Clear();
            foreach (GameObject w in tempMarkers)
            {
                Destroy(w);
            }

            edgeCounter = -1; 
            outerEdges.Clear();

        }
        
    }

    void OnMouseUp()
    {
        
    }
}