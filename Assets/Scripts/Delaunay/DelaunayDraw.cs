//EXPERIMENTAL AND BUGGY
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
                    hitPoint.point.x - (hitPoint.point.x % 0.005f), 
                    0, 
                    hitPoint.point.z - (hitPoint.point.z % 0.005f)
                    );

                lastHit = hitPoint;
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            
            GameObject wayMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wayMarker.transform.position = new Vector3(lastHit.point.x, 0, lastHit.point.z);
            wayMarker.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
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

            if (edgeCounter < 4)
            {
                oe = null;
            }
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