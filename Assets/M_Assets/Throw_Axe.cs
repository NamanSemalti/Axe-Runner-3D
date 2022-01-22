using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw_Axe : MonoBehaviour
{
    public Material rayMaterial;
    public Rigidbody axe;
    public GameObject kulladhi;
    public float throwForce = 50;
    public Transform RightHand;
    public Transform curve_point;
    private Vector3 old_pos;
    private bool isReturning = false;
    private float time = 0.0f;
    public LayerMask Enemy;
    public GameObject AxeObject;
    public Animator KartosAnim;
    Animator playerAnimator;
    public GameObject Player;
    public Collider axeCollider;
    public Transform throwTarget;
    public bool canThrow;
    public Vector3 position_ray_colision;
    public LineRenderer lineRenderer;
    Vector3 AxeStartPosition;
    Quaternion AxeStartRotation;
     TrailRenderer trail;
    bool hasReturned;
    Vector3 initialPos;
    bool AxeAimReturn;



    [Header("Audio")]
    AudioSource audiosource;
    public AudioClip AxeThrowSound;
    
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        AxeAimReturn = false;
        hasReturned = true;
        axe.tag = "Axe";
        trail = axe.GetComponent<TrailRenderer>();
        trail.enabled = false;
        axeCollider.enabled = false;
        AxeStartPosition = axe.transform.position;
        AxeStartRotation = axe.transform.rotation;
        //lineRenderer.enabled = false;
        playerAnimator = Player.GetComponent<Animator>();
    }
    void Update()
    {
        initialPos =   new Vector3(throwTarget.position.x, 0, throwTarget.position.z);
        lineRenderer.transform.position = transform.position;
        canThrow = false;
        if(AxeAimReturn)
        {
            Debug.Log("Returning Axe");
            throwTarget.position = Vector3.MoveTowards(throwTarget.position, initialPos, 0.1f * Time.deltaTime);
          // throwTarget.position = new Vector3(Mathf.Lerp(throwTarget.position.x , initialPos.x,1f), 0, throwTarget.position.z);
        }
        if(hasReturned)
        {
            FireSequence();
        }
        if (isReturning)
        {
            if (time < 1.0f)
            {
                // Update its position by using the Bezier formula based on the current time
                axe.position = getBQCPoint(time, old_pos, curve_point.position, RightHand.position);
                // Reset its rotation (from current to the targets rotation) with 50 units/s
                axe.rotation = Quaternion.Slerp(axe.transform.rotation, RightHand.rotation, 50 * Time.deltaTime);
                // Increase our timer, if you want the axe to return faster, then increase "time" more
                // With something like:
                // time += Timde.deltaTime * 2;
                // It will return as twice as fast
                time += Time.deltaTime;
            }
            else
            {
                ResetAxe();
            }
        }
    }
    void FireSequence()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, throwTarget.position, out hit, Enemy))
        {
            canThrow = true;
            position_ray_colision = hit.point;
            //Debug.Log(position_ray_colision);
            lineRenderer.SetPosition(0, lineRenderer.transform.position);
            lineRenderer.SetPosition(1, throwTarget.position);
            if(hit.transform.tag == "Enemy")
            {
                rayMaterial.color = Color.green;
            }
            else
            {
                rayMaterial.color = Color.red;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch first = Input.GetTouch(0);
            if (first.phase == TouchPhase.Stationary)
            {
                lineRenderer.enabled = true;
                KartosAnim.speed = 0.2f;
            }
            if (first.phase == TouchPhase.Ended)
            {
                
                KartosAnim.speed = 1f;
                lineRenderer.enabled = false;
                if (canThrow && hasReturned)
                {
                    playerAnimator.SetTrigger("Throw");
                    Invoke("ThrowAxe", 1f);
                    //KartosAnim.speed = 1f;


                    
                }
                hasReturned = false;
                //  ThrowAxe();
                //  KartosAnim.speed = 1f;
                //  playerAnimator.SetTrigger("Throw");

            }
        }
        
    }
    void ThrowAxe()
    {
        if(!audiosource.isPlaying)
        {
            audiosource.volume = 0.5f;
            audiosource.PlayOneShot(AxeThrowSound);
        }
        
        trail.enabled = true;
        
        Invoke("EnableCollider", 0.1f);
        axe.constraints = RigidbodyConstraints.FreezePositionY;
       // axe.constraints = RigidbodyConstraints.FreezePositionZ;
        Vector3 dir = position_ray_colision - axe.transform.position;
        isReturning = false;
        axe.transform.parent = null;
        axe.isKinematic = false;
        axe.useGravity = false;
        //axe.AddForce(-throwForce * Time.deltaTime,0, 0,ForceMode.Impulse);
        //StartCoroutine(ThrowKulladhi());
        axe.AddForce(dir * 2, ForceMode.Impulse);
        //axe.AddForce(position_ray_colision * throwForce * Time.deltaTime, ForceMode.Impulse);
        axe.AddTorque(axe.transform.TransformDirection(Vector3.forward) * 100, ForceMode.Impulse);
        Invoke("ReturnAxe", .5f);

    }
    void ReturnAxe()
    {
        AxeAimReturn = true;
        axeCollider.enabled = false;
        axe.constraints = RigidbodyConstraints.None;
        time = 0.0f;
        old_pos = axe.position;
        isReturning = true;
        axe.velocity = Vector3.zero;
        axe.isKinematic = true;
        axe.useGravity = true;
     
        
    }

    // Reset axe
    void ResetAxe()
    {
        hasReturned = true;
        axe.tag = "Axe";
        trail.enabled = false;
        isReturning = false;
        axe.transform.parent = RightHand;
        axe.position = RightHand.position;
        axe.rotation = RightHand.rotation;
    }

    void EnableCollider()
    {
        axeCollider.enabled = true;
    }
    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // "t" is always between 0 and 1, so "u" is other side of t
        // If "t" is 1, then "u" is 0
        float u = 1 - t;
        // "t" square
        float tt = t * t;
        // "u" square
        float uu = u * u;
        // this is the formula in one line
        // (u^2 * p0) + (2 * u * t * p1) + (t^2 * p2)
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, throwTarget.transform.position, Color.red);
    }

 


}