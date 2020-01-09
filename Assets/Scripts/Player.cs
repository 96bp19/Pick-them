using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Waypoint Related")]
    public List<Transform> waypoints;
    public int currentIndex = 0;
    public float rotateSmoothness = 0.1f;
    


    [Header(" Speed Related")]
    public float maxSpeed = 20f;
    public float currentSpeed;
    public float acceleration = 60;
    public float deacceleration = 30f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        InvokeRepeating("checkForWaypoints", 0.04f, 0.04f);
    }
    public void initializePlayerWaypoints( List<Transform> waypoints)
    {
       
        this.waypoints = new List<Transform>();
        this.waypoints = waypoints;
    }

    
    void checkForWaypoints()
    {
        if (waypoints == null)
        {
            Debug.Log("no waypoints found");
          
        }

        if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 10f)
        {
            currentIndex++;
            currentIndex = currentIndex % waypoints.Count;
        }
        
    }

    private void Update()
    {
      
        CalculateDesiredSpeed();
        HeadFollower.SetCurrentHeadSpeed(currentSpeed);
    }

    private void FixedUpdate()
    {
        if (waypoints != null)
        {
            
             RotateTowardsWaypoint();
             MoveTowardsWaypoint();

        }
    }
    void RotateTowardsWaypoint()
    {
        if (!InputHandler.Inputs.screenTouched)
        {
            return;
        }
        Vector3 lookDirection = (waypoints[currentIndex].position - transform.position);
        lookDirection.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(lookDirection.normalized);
        Quaternion newLookRot = Quaternion.Lerp(transform.rotation, lookRot, rotateSmoothness);
        _rb.MoveRotation(newLookRot);
    }

    void MoveTowardsWaypoint()
    {

        Debug.DrawRay(transform.position, transform.forward, Color.green);
        Vector3 velocity = waypoints[currentIndex].position - transform.position;
        velocity.Normalize();
        // _rb.velocity = velocity * 3f;
        //transform.position += velocity*0.04f;
        _rb.MovePosition(transform.forward * currentSpeed * Time.fixedDeltaTime+ transform.position);
    }


   
    void CalculateDesiredSpeed()
    {
        float desiredSpeed =  maxSpeed;
        if (InputHandler.Inputs.screenTouched)
        {
            // moves
            currentSpeed = Mathf.MoveTowards(currentSpeed, desiredSpeed, acceleration * Time.deltaTime);
            
        }
        else
        {
            // deaccelerate
           
            desiredSpeed = 0;
            currentSpeed = Mathf.MoveTowards(currentSpeed, desiredSpeed, deacceleration * Time.deltaTime);
        }
    }
}
