using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TapToStart;

    private void Awake()
    {
       
    }
    void Start()
    {
        TapToStart.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            TapToStart.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
