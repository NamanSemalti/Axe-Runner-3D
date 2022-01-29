using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    bool isChasing;
    //public GameObject particle;
    public ParticleSystem DeathParticle;
    Animator anim;
    //public Transform Axe;
    NavMeshAgent Agent;
    public GameObject player;
     float AttackRadius = 2f;
     float chaseRadius = 12f;
    float Distance;
    public static bool isAttacking;
    CoinCollection coinScript;
   
   public static bool playerisDead;
    Animator playerAnim;
   
    bool particleMoving;
    Collider collider;
    AudioSource audioSource;
    public AudioClip deathSound;
    bool isdead;
    public float speed ;
    void Start()
    {
        speed = 0.3f;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        EnableItems();
        collider = GetComponent<Collider>();
        isChasing = false;
       // particle.SetActive(false);

        playerisDead = false;
        //Axe.tag = "Axe";
        isAttacking = false;  
        anim = gameObject.GetComponent<Animator>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
       
        anim.speed = speed;
    }

    
    void Update()

    {

        
        player = GameObject.FindWithTag("Player");
        coinScript = player.GetComponent<CoinCollection>();
        playerAnim = player.GetComponent<Animator>();
        // Agent.SetDestination(player.position);
        if (playerisDead)
        {
            collider.enabled = false;
            Agent.enabled = false;
            anim.SetBool("Idle", true);
            isAttacking = false;
        }
       // AttackSequence();
        Distance = Vector3.Distance(player.transform.position, transform.position);
        
            if (Distance <= AttackRadius)
            {
                isChasing = false;
                isAttacking = true;
                anim.speed = 1f;
                anim.SetTrigger("Attack");
                
            }
            if(Distance <= chaseRadius)
            {
                isChasing = true;
            }
         
         
         if(isChasing)
        {
            anim.SetBool("Run", true);
        }
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Axe")
        {
            if(!isdead)
            {
                if (!audioSource.isPlaying)
                {

                    audioSource.PlayOneShot(deathSound);
                    isdead = true;
                }
            }

            //Axe.tag = "Untagged";
            coinScript.Coins++;
            DeathParticle.Play();
           // particle.SetActive(true);
            //particle.Play(true);
            particleMoving = true;
            AttackRadius = 0f;
            GetComponent<Collider>().enabled = false;
            //player.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            Agent.enabled = false;
            anim.SetBool("Death", true);
            anim.speed = 1f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
    public void AttackSequence()
    {
        if (isAttacking)
        {
           
            playerisDead = true;
            Agent.enabled = false;
            playerAnim.SetBool("Dead", true);
           // Invoke("UI", 2f);
        }
        
        
        


    }
    
    public void EnableItems()
    {
       // particle = transform.Find("ParticleFX (1)").gameObject;
        //deathpannel = GameObject.FindWithTag("DeathPannel");
    }

}
