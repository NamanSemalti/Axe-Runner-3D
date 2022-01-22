using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpener : MonoBehaviour
{
    public GameObject Door;
    Animator anim;
    public GameObject Villager;
    Animator VillAgeAnim;
    public ParticleSystem effect;
    public GameObject Player;
    CoinCollection coinScript;
    public int CoinToBeAdded;
    public GameObject Emojis;
    //public GameObject BamEffect;
    public ParticleSystem BamEffect;
    AudioSource audioSource;
    public AudioClip cageSound;
    public AudioClip thnakyousound;
    bool CageOpened;
    // Start is called before the first frame update
    void Start()
    {
        CageOpened = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        Emojis.SetActive(false);
        coinScript = Player.GetComponent<CoinCollection>();
        VillAgeAnim = Villager.GetComponent<Animator>();
        anim = Door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Axe")
        {
            if(!CageOpened)
            {
                if(!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(cageSound);
                    audioSource.PlayOneShot(thnakyousound);
                    CageOpened = true;
                }
            }
            BamEffect.Play();
            Emojis.SetActive(true);
            anim.SetBool("Open", true);
            VillAgeAnim.SetBool("Cheer", true);
            effect.Play();
            coinScript.Coins += CoinToBeAdded;
            Invoke("DestroyBamEffect", 0.1f);
        }
    }
    void DestroyBamEffect()
    {
        Destroy(BamEffect);
        
    }
}
