using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public GameObject WinScreenPannel;
    public GameObject player;
    Animator anim;
    public GameObject emojis;
    public ParticleSystem wineffect;
    AudioSource audioSource;
    public AudioClip VictorySound;
    //public GameObject ParticleFX;
   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = player.GetComponent<AudioSource>();
        emojis.SetActive(false);
       // ParticleFX.SetActive(false);
        anim = player.GetComponent<Animator>();
        WinScreenPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(VictorySound);
            }
            wineffect.Play();
            anim.SetBool("Victory", true);
            Invoke("CheersParticle", 1f);
            LoadNextLevel();
        }
    }
    void LoadNextLevel()
    {
        WinScreenPannel.SetActive(true);
        emojis.SetActive(true);
    }
    void CheersParticle()
    {
        //ParticleFX.SetActive(true);
    }
    public void NextLeveButton()
    {
        
        int currscene = SceneManager.GetActiveScene().buildIndex;
        if(currscene >= PlayerPrefs.GetInt("currlevle"))
        {
            PlayerPrefs.SetInt("currlevle", currscene + 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("currlevle"));
    }
    public void RestartLevel()
    {
        Scene currscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currscene.buildIndex);
    }
    public void OnButtonDownMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
