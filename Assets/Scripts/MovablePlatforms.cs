using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatforms : MonoBehaviour
{
    public GameObject Pivot;
    Animator anim;
    public GameObject[] enemies;
    public GameObject UI;
    public ParticleSystem effect;
   
    // Start is called before the first frame update
    void Start()
    {
       
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
        anim = Pivot.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Axe")
        {
            effect.Play();
            UI.SetActive(false);
            anim.SetBool("Moved", true);
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
            
        }
    }
    
}
