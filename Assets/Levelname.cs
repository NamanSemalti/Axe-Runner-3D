using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelname : MonoBehaviour
{
    Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        levelText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Scene currScene = SceneManager.GetActiveScene();
        levelText.text = currScene.name; 
    }
}
