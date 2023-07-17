using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        long h = 0; 
        foreach (var vertex in gameObject.GetComponent<MeshFilter>().mesh.vertices)
        {
            print(vertex);
            h++;
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Vector3 gpos = vertex;
            // Vector3 localScale = gameObject.transform.localScale;
            // print(localScale);
            // gpos = new Vector3(
            //     gpos.x * localScale.x, 
            //     gpos.y * localScale.y, 
            //     gpos.z * localScale.z);
            //
            // g.transform.position = gpos;
            //

            g.transform.position = vertex;
            
            
            if (h > 204096){
                break;
            }
        }
        print(h);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
