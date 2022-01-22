using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clampTarget : MonoBehaviour
{
    public float leftLimit;
    public float RightLimit;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPos = transform.position;
       
        if (TargetPos.z <= player.position.z)
        {
            TargetPos.z = player.position.z;
        }
        transform.position = TargetPos;
    }
    void Hello()
    {

    }
}
