using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Aim : MonoBehaviour
{
    public GameObject Player;
    Animator playerAnimator;
    public Rigidbody Axe;
    public Transform AxeTransform;
    public Transform ray;
    public int ThrowPower;
    public float rotationSpeed;
    public static bool axeisActivated;
    private bool isReturning;
    public Transform target, curve_Point;
    private Vector3 old_pos;
    public Animator KartosAnim;
    [Header("Laser")]
    public static bool canBemoved;
    public GameObject LaserTarget;
    public LineRenderer lineRenderer;
    public bool useLaser;
    private float time = 0.0f;
    Vector3 startpos;
    private void Start()
    {
        startpos = LaserTarget.transform.position;
        canBemoved = false;
        lineRenderer.enabled = false;
        useLaser = false;
        axeisActivated = false;
        playerAnimator = Player.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isReturning)
        {
            if (time < 1.0f)
            {
                // Update its position by using the Bezier formula based on the current time
                Axe.position = getBQCPoint(time, old_pos, curve_Point.position, target.position);
                // Reset its rotation (from current to the targets rotation) with 50 units/s
                Axe.rotation = Quaternion.Slerp(Axe.transform.rotation, target.rotation, 50 * Time.deltaTime);
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
        if (Input.touchCount > 0)
        {
            Touch first = Input.GetTouch(0);
            if (first.phase == TouchPhase.Stationary)
            {
                canBemoved = true;
                KartosAnim.speed = 0.2f;
                useLaser = true;


            }
            if (first.phase == TouchPhase.Ended)
            {
                useLaser = false;
                KartosAnim.speed = 1f;
                playerAnimator.SetTrigger("Throw");
                LaserTarget.transform.position = startpos;
            }
            if (canBemoved)
            {
                if (first.phase == TouchPhase.Moved)
                {
                    LaserTarget.transform.Translate(LaserTarget.transform.position.x + first.deltaPosition.x * 0.1f, transform.position.y, transform.position.z);
                }
            }
        }
        Laser();
    }
    public void AxeThrow()
    {
        isReturning = false;
        axeisActivated = true;
        Axe.isKinematic = false;
        Axe.useGravity = false;
        Axe.transform.parent = null;
        Axe.AddForce(ray.forward * ThrowPower * Time.deltaTime, ForceMode.Impulse);

    }
    public void AxeReturn()
    {
        old_pos = Axe.transform.position;
        isReturning = true;
        Axe.velocity = Vector3.zero;
        Axe.isKinematic = true;
    }
    void ResetAxe()
    {
        isReturning = false;
        Axe.transform.parent = transform;
        Axe.position = target.position;
        Axe.rotation = target.rotation;
    }
    void Laser()
    {
        if (useLaser)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, lineRenderer.transform.position);
            lineRenderer.SetPosition(1, LaserTarget.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, LaserTarget.transform.position, Color.red);
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
}
