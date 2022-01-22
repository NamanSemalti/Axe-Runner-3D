using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollisiion : MonoBehaviour
{
    public static bool Collided;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if (collision.gameObject.tag == "Axe")
        {
            Debug.Log("AxeCollision");
            Collided = true;
        }
        else
            Collided = false;
           
        
    }
    

}
