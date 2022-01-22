using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCanvasEnabler : MonoBehaviour
{
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
            if (other.gameObject.tag == "Player")
            {
                UI.SetActive(true);
            }
        
    }
}
