using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera cam;
    public GameObject selected; 
    
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {   
                if (hit.transform.CompareTag("Province"))
                {
                    //if ()
                    {
                        selected.GetComponent<Outline>().enabled = false;
                    }
                    hit.transform.parent.GetComponent<Outline>().enabled = !hit.transform.parent.GetComponent<Outline>().enabled;
                }
            }
        }
    }
    
}
