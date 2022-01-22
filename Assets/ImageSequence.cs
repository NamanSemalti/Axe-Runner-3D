using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSequence : MonoBehaviour
{
    public GameObject[] images;
    public float time = 0f;
    [SerializeField] float Timedelay = 0.6f;
    public float TImeTOBeIncreased;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + TImeTOBeIncreased * Time.deltaTime;
        for (int i = 0 ; i < images.Length; i++ )
        {
            
            if(time>0.4f)
            {
                GameObject image = images[i];
                image.SetActive(true);
                i++;
                time = 0f;
            }
            
            
        }
    }
}
