using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    public GameObject FinishPannel;
    // Start is called before the first frame update
    void Start()
    {
        FinishPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FinishPannel.SetActive(true);
        }
    }
    public void OnButtonDownMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
