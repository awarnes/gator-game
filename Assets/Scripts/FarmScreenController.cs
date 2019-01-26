using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmScreenController : MonoBehaviour
{

    private Rect growOutBounds;
    // Start is called before the first frame update
    void Start()
    {
        growOutBounds = new Rect(-62.16f, -58, -30.4f, -27.4f);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Left!");            
        }
    }
}
