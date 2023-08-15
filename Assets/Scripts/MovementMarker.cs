using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MovementMarker : MonoBehaviour
{
    public GameObject movementMarker;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 75; i += 1)
        {
            for (int j = 0; j < 38; j += 1)
            {

                Instantiate(movementMarker, new Vector3(i * 64, 1, j * 64), movementMarker.transform.rotation, transform);
            }
        }
        
        Destroy(movementMarker);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
