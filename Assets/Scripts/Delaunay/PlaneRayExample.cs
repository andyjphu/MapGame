//This script detects mouse clicks on a plane using Plane.Raycast.
//In this example, the plane is set to the Camera's x and y position, but you can set the z position so the plane is in front of your Camera.
//The normal of the plane is set to facing forward so it is facing the Camera, but you can change this to suit your own needs.

//In your GameObject's Inspector, set your clickable distance and attach a cube GameObject in the appropriate fields

using UnityEngine;

public class PlaneRayExample : MonoBehaviour
{
    //Attach a cube GameObject in the Inspector before entering Play Mode
    public GameObject m_Cube;


    public GameObject m_Plane;

    private RaycastHit lastHit = new RaycastHit(); 
    //Vector3 m_DistanceFromCamera;

    void Start()
    {
        //This is how far away from the Camera the plane is placed
        //m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - m_DistanceZ);

        //Create a new plane with normal (0,0,1) at the position away from the camera you define in the Inspector. This is the plane that you can click so make sure it is reachable.
       // m_Plane = new Plane(Vector3.forward, m_DistanceFromCamera);
    }

    void Update()
    {
        
        //Detect when there is a mouse click
        if (true)//Input.GetMouseButton(0))
        {
            //Create a ray from the Mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (m_Plane.GetComponent<MeshCollider>().Raycast(ray, out RaycastHit hitPoint, 10000))
            {
                
 

                //Move your cube GameObject to the point where you clicked
                
                
                m_Cube.transform.position = new Vector3(
                    Mathf.RoundToInt(hitPoint.point.x), 
                    0, 
                    Mathf.RoundToInt(hitPoint.point.z)
                    );

                lastHit = hitPoint;
                //m_Cube.transform.position = hitPoint.point; 
            }
        }

        if (Input.GetMouseButton(0))
        {
            GameObject wayMarker = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wayMarker.transform.position = lastHit.point; 
            

        }
    }
}