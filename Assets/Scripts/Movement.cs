using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;



public class Movement : MonoBehaviour
{
    //Car Speed Movement
    public float speed = 10.0f;
    //Left Right Car Movement Speed
    public float rotationSpeed = 100.0f;
    void Start()
    {
        
    }
    void Update()
    {
        //Get Joystick Control
        float translation = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
        //float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
       

        transform.Translate(translation, 0, 0);
        
    }


}
