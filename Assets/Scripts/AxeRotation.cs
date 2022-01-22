using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Aim.axeisActivated)
        {
            transform.Rotate(0, -200 * Time.deltaTime, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Aim.axeisActivated = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
