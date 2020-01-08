using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> waypoints;
    public float rotateSmoothness = 0.1f;
    public int currentIndex = 0;
    public float moveSpeed = 2f;

    Rigidbody _rb;

  

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

        if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 0.9f)
        {
            currentIndex++;
            currentIndex = currentIndex % waypoints.Count;
        }
        
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
        Quaternion lookRot = Quaternion.LookRotation(waypoints[currentIndex].position - transform.position);
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
        _rb.MovePosition(transform.forward * moveSpeed * Time.fixedDeltaTime+ transform.position);
    }
}
