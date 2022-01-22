using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BossFight : MonoBehaviour
{
    public GameObject GameOverPannel;
    public ParticleSystem enemyPower;
    public ParticleSystem SpiderDeathEffect;
    public TrailRenderer AxePower;
    public Button[] buttons;
    public GameObject[] dummyButtons;
    public GameObject Player;
    Animator anim;
    bool Jump;
    bool Attack;
    bool ThrowAxe;
    int Phase;
    bool isThrowing;
    bool isJumping;
    bool isAttckaing;
    bool enemyAttacking;
    public Transform Axe;
    public Transform DummyAxe;
    public Transform Target;
    public Transform PlayerTarget;
    public Transform Target1;
    public Transform CamTarget;
    public Transform Camera;
    public GameObject Spider;
    Animator SpiderAnim;
    public GameObject Winpannel;
    public int TimeLimit;
    public float AttackSpeed;
    public float time = 0f;
    [SerializeField] float Timedelay = 0.6f;
    public float TImeTOBeIncreased;
    void Start()
    {
        TimeLimit = 0;
        TImeTOBeIncreased = 0f;
        Winpannel.SetActive(false);
        foreach(GameObject dummyButton in dummyButtons)
        {
            dummyButton.SetActive(false);
        }
        
       // SpiderDeathEffect.Stop(true);
        GameOverPannel.SetActive(false);
        enemyPower.Stop(true);
        AxePower.enabled = false;
        SpiderAnim = Spider.GetComponent<Animator>();
        CamTarget.gameObject.SetActive(false);
        // AxeReturning = false;
        enemyAttacking = false;
        isThrowing = false;
        isJumping = false;
        isAttckaing = false;
        Phase = 5;
        Jump = false;
        Attack = false;
        ThrowAxe = false;
        anim = Player.GetComponent<Animator>();
        foreach(Button button in buttons)
        {
            //button.gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
            
            time = time + TImeTOBeIncreased * Time.deltaTime; //isse time everysecond 1 plus increase hota rahega
            if (time > Timedelay)
            {
                EnemyAttack();
            }
            if (Phase == 0)
            {
                TImeTOBeIncreased = 1f;
                dummyButtons[0].SetActive(true);
                dummyButtons[1].SetActive(false);
                dummyButtons[2].SetActive(false);
            }
            if (Phase == 1)
            {
                
                dummyButtons[1].SetActive(true);
                dummyButtons[0].SetActive(false);
                dummyButtons[2].SetActive(false);

            }
            if (Phase == 2)
            {
                

                dummyButtons[2].SetActive(true);
                dummyButtons[0].SetActive(false);
                dummyButtons[1].SetActive(false);
        }   

        Debug.Log(Time.deltaTime);
        if (isThrowing)
        {
            Axe.position = Vector3.MoveTowards(Axe.position, Target.position, 20f*Time.deltaTime);
            Axe.Rotate(0,0, 5f);
        }
        if (isJumping)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerTarget.position, 10f * Time.deltaTime);
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CamTarget.position, 20f * Time.deltaTime);
            
            
        }
        if (isAttckaing)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, Target1.position, AttackSpeed * Time.deltaTime);
            Axe.transform.position = Vector3.MoveTowards(Axe.transform.position, DummyAxe.position, AttackSpeed * Time.deltaTime);
            Axe.rotation = DummyAxe.rotation;
            if (Axe.transform.position == DummyAxe.position)
            {
                //AxePower.enabled = false;
            }
        }
        
        if (Player.transform.position == PlayerTarget.position)
        {
            CamTarget.parent = Player.transform;
            CamTarget.gameObject.SetActive(true);
            Camera.gameObject.SetActive(false);
        }
        if (Axe.position == Target.position)
        {
            SpiderAnim.SetBool("LegBreak", true);
        }
        if (Player.transform.position == Target1.position)
        {
            Winpannel.SetActive(true);
            SpiderAnim.SetBool("HeadBreak", true);
            //SpiderDeathEffect.Play();
        }

        if(enemyAttacking)
        {
            enemyPower.transform.position = Vector3.MoveTowards(enemyPower.transform.position, Player.transform.position, 10f*Time.deltaTime);
            if(enemyPower.transform.position == Player.transform.position)
            {
                PlayerDeadSquence();   
            }
        }
        
    }
        //JumpSequence();
       // AttackSequence();
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            Phase = 0;
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(true);
                anim.speed = 0.2f;
               
            }
            foreach(GameObject dummybutton in dummyButtons)
            {
                dummybutton.SetActive(true);
            }
        }
    }
    public void OnAttackButtonDown()
    {
        if (Phase == 2) 
        {
            TImeTOBeIncreased = 0f;
            time = 0f;
            Phase = 3;
            Attack = true;
            AttackSequence();
        }
        else
            EnemyAttack();
    }
    public void OnJumpButtonDown()
    {
        if (Phase == 1)
        {
            time = 0f;
            Phase = 2;
            Jump = true;
            JumpSequence();
            
        }
        else
            EnemyAttack();
       
    }
    public void OnThrowButtonDown()
    {
        if(Phase == 0)
        {
            time = 0f;
            Phase = 1;
            ThrowAxe = true;
            ThrowAxeSequence();
            
        }
        else
        {
            EnemyAttack();
        }
      
    }
    void ToIdle()
    {
        anim.SetBool("Attack", false);
    }
    void  JumpSequence()
    {
        if(Phase == 2 && Jump )
        {
            isJumping = true;
            anim.SetBool("Jump", true);
        }
       
    }
    void AttackSequence()
    {
        isAttckaing = true;
        
        anim.SetBool("JumpAttack", true);
        
    }
    
    void ThrowAxeSequence()
    {
        if (Phase == 1 && ThrowAxe)
        {
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }
            anim.speed = 1f;
            anim.SetBool("Attack", true);
            Invoke("Throwaxe", 1f);
            Invoke("ToIdle", .5f);
        }
    }
    public void Throwaxe()
    {
        AxePower.enabled = true;
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
        Axe.parent = null;
        isThrowing = true;
       
    } 

    void EnemyAttack()
    {
        enemyAttacking = true;
        enemyPower.Play();
    }

    void PlayerDeadSquence()
    {
        anim.speed = 1f;
        anim.SetBool("Death", true);
        enemyPower.Stop(true);
        GameOverPannel.SetActive(true);
    }

    public void Restart()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.buildIndex);
    }
    public void NextScene ()
    {
        
        int currscene = SceneManager.GetActiveScene().buildIndex;
        if (currscene >= PlayerPrefs.GetInt("currlevle"))
        {
            PlayerPrefs.SetInt("currlevle", currscene + 1);
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("currlevle"));
    }
}
