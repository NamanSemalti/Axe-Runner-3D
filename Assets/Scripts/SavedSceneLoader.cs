using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedSceneLoader : MonoBehaviour
{
    int LevelsUnlocked;
    // Start is called before the first frame update
    void Start()
    {

        LevelsUnlocked = PlayerPrefs.GetInt("currlevle");
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("LoadSavedScene", 3f);
    }
    void LoadSavedScene()
    {
        if(LevelsUnlocked==1)
        {
            SceneManager.LoadScene(LevelsUnlocked+1);
        }
        else
        {
            SceneManager.LoadScene(LevelsUnlocked);
        }
        
    }
}
