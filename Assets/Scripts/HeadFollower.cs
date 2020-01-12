using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollower : MonoBehaviour
{

    public int maxListAmount = 20;

    public Transform childPrefab;
    [HideInInspector]
    public Transform Child;
    [HideInInspector]
    public HeadFollower Parent = null;

    
    Vector3 currentPos;
    bool allowedSpawning = true;

    private List<Vector3> previousPositions = new List<Vector3>();
    private List<Quaternion> previousRotations = new List<Quaternion>();

  

    
    private static float CurrentMoveSpeed;
    private static Vector3 childSpawnPos;
    
    
    public static void SetCurrentHeadSpeed(float speed)
    {
        CurrentMoveSpeed = speed;
    }

    Vector3 moveVel;
    private void FixedUpdate()
    {
        if (CurrentMoveSpeed <=5f)
        {
          
            return;
        }

        if (Child != null)
        {
            if (previousPositions.Count >= maxListAmount)
            {
                // Child.transform.position = Vector3.Slerp(previousPositions[0], Child.transform.position, 0.99f);
                // Child.transform.position = Vector3.Lerp(previousPositions[0], Child.transform.position, 0.2f);
                Child.transform.position = Vector3.SmoothDamp(Child.transform.position, previousPositions[0], ref moveVel, 0.1f);


                Child.transform.rotation = previousRotations[0];
                previousPositions.RemoveAt(0);
                previousRotations.RemoveAt(0);
            }
            previousPositions.Add(transform.position);
            previousRotations.Add(transform.rotation);

        }
    }

    private void Update()
    {
        // u can remove this if statement if testing is not required
        if (Input.GetKeyDown(KeyCode.Z) && allowedSpawning)
        {
            Child = Instantiate(childPrefab);
            // set this as a parent of newly spawned child
            Child.GetComponent<HeadFollower>().Parent = this;
            Child.name = "Child";
            allowedSpawning = false;
            GameManager.AddPlayerFollower(Child.gameObject);
        }

        // this is actual method to spawn follower
        if (SpawnRequest)
        {
            SpawnNewBody();
        }

        // this is test only code
        if (Input.GetKeyDown(KeyCode.X) && allowedSpawning)
        {
            if (Parent)
            {
            // remove self from the list
                GameManager.RemovePlayerFollower(gameObject);
                Parent.Child = null;
                Parent.allowedSpawning = true;
                Destroy(gameObject);

            }
        }
    }

    private static bool SpawnRequest;
   
    public static void setSpawnRequest(Vector3 spawnPos , bool val = true )
    {
        childSpawnPos = spawnPos;
        SpawnRequest = val;
    }

    private void SpawnNewBody()
    {
        if (allowedSpawning)
        {
            Child = Instantiate(childPrefab);

            // set this as a parent of newly spawned child
            Child.GetComponent<HeadFollower>().Parent = this;
            Child.name = "Child";
            Child.transform.position = childSpawnPos;
            allowedSpawning = false;
            GameManager.AddPlayerFollower(Child.gameObject);
            SpawnRequest = false;
        }
    }


}
