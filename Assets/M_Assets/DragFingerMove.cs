using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFingerMove : MonoBehaviour
{
    private Touch touch;
    private float moveSpeed = 0.05f;
    public Transform Cube;
    bool isReturning;
    float smooth = 2;
    // Use this for initialization
    private void Start()
    {

    }
    
    // Update is called once per frame
    private void Update()
    {
        if(isReturning)
        {

            Quaternion position = Quaternion.Lerp(Cube.rotation, Quaternion.Euler(0, 0, 0), smooth*Time.deltaTime);
            Cube.rotation = position;
        }
        if (Input.touchCount > 0)
        {
            isReturning = false;
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
               
                Cube.Rotate(0, touch.deltaPosition.x * moveSpeed, 0 );
            }
            if(touch.phase == TouchPhase.Ended)
            {
                Invoke("EndedPhase", 0.7f);
            }

        }
    }
    void EndedPhase()
    {
        isReturning = true;
        
       
    }
}

