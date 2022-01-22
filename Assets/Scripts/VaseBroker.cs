using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseBroker : MonoBehaviour
{
    
    public GameObject Vase; 
    //public GameObject coin;
    public GameObject brokenVass;
    public GameObject Player;
    CoinCollection coinScript;
    public ParticleSystem effect;
    
    bool collided;
    float Distance;
     int coinsToBeAdded;
    public GameObject enemyPrefab;
    Enemy script;
    bool plustencollected;
    public GameObject Canvas;

    AudioSource audiosource;
    public AudioClip breakingSound;
    Animator PlayerAnim;
    
    // public GameObject plusonecoin;
    // Start is called before the first frame update
    void Start()

    {
        
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerAnim = Player.GetComponent<Animator>();
        audiosource = Player.GetComponent<AudioSource>();
        coinScript = Player.GetComponent<CoinCollection>();
        collided = false;
        if(enemyPrefab.name == "Wraith (1)")
        {
           
            script = enemyPrefab.GetComponent<Enemy>();
            coinsToBeAdded = 1;
            
            
        }
        else
        {
           
            coinsToBeAdded = 10;
            
            
        }


        //plusten.SetActive(false);
        
        gameObject.SetActive(true);
        brokenVass.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
       
       
                
                
            
            
        
            
        
    }
    void DestroyParticle()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Axe")
        {
            if(enemyPrefab.name == "Wraith (1)")
            {
                
                plustencollected = true;
                
            }
            coinScript.Coins += coinsToBeAdded;

            collided = true;
            
                audiosource.volume = 0.2f;
                audiosource.PlayOneShot(breakingSound);
            
            
            effect.Play();
            Vase.GetComponent<MeshRenderer>().enabled = false;
            brokenVass.SetActive(true);
           // pluscoin.SetActive(true);
           Instantiate(enemyPrefab, Vase.transform.position,Vase.transform.rotation);
            //enemyPrefab.transform.eulerAngles = new Vector3(90, 90, 0);
            


            Invoke("Destroy", 1.5f);
            //script.EnableItems();



        }
        if(collision.gameObject.tag == "Player")
        {
            Enemy.isAttacking = true;
            PlayerAnim.SetBool("Dead", true);
            Player.GetComponent<Throw_Axe>().enabled = false;
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
        Destroy(brokenVass);
    }
}
