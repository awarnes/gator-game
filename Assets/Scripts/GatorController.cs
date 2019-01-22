using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatorController : MonoBehaviour
{
    public float mooveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5 || Input.GetAxisRaw("Horizontal") < -0.5) {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * mooveSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetAxisRaw("Vertical") > 0.5 || Input.GetAxisRaw("Vertical") < -0.5) {
            transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * mooveSpeed * Time.deltaTime, 0));
        }
    }
}
