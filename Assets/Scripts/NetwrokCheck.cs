using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NetwrokCheck : MonoBehaviour
{
    public GameObject connectionerrorimage;
    public Image loadingText;
    public Button tryAgainButton;
    public GameObject TapToStart;

    // public GameObject TapToStart;
    bool internetconneted;
    
    

    // Update is called once per frame
   
    // Start is called before the first frame update
    void Start()
    {

       
        StartCoroutine(CheckInternetConnection());
    }

    // Update is called once per frame
    void Update()
    {
        if(internetconneted)
        {
            if (Input.touchCount >= 1)
            {
                TapToStart.SetActive(false);
                
            }
        }
    }
    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            connectionerrorimage.SetActive(true);
            internetconneted = false;
            Debug.Log("Internet Off");
            
            loadingText.gameObject.SetActive(false);

            tryAgainButton.gameObject.SetActive(true);
        }
        else
        {
            int LevelUnlocked;
            LevelUnlocked = PlayerPrefs.GetInt("currlevle");
            internetconneted = true;
            if (LevelUnlocked == 0)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(LevelUnlocked);
            }
                
        }

                
                //loadingText.gameObject.SetActive(false);
                //TapToStart.SetActive(true);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
