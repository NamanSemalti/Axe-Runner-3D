using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathPannel;
    public GameObject Aim;
    AudioSource audiosource;
    public AudioClip DeathAudio;
    public AudioClip LevelFailedSound;
    bool isDead;
    
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        audiosource = transform.parent.GetComponent<AudioSource>();
        deathPannel.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {

        if(Enemy.isAttacking)
        {
            if(!isDead)
            {
                if (!audiosource.isPlaying)
                {

                    audiosource.PlayOneShot(DeathAudio);
                    Enemy.isAttacking = false;
                    isDead = true;

                }
            }
            
            
            Invoke("UI", 2f);
        }
    }
    void UI()
    {
        if(!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(LevelFailedSound);
        }
        deathPannel.SetActive(true);
        transform.parent.GetComponent<Throw_Axe>().enabled = false;
        Aim.GetComponent<DragFingerMove>().enabled = false;
    }
}
