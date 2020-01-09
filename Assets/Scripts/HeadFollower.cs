using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollower : MonoBehaviour
{

    public int maxListAmount = 20;

    public Transform childPrefab;
    public Transform Child;

    
    Vector3 currentPos;
    bool allowedSpawning = true;

    public List<Vector3> previousPositions = new List<Vector3>();
    public List<Quaternion> previousRotations = new List<Quaternion>();

    public  delegate void OnPlayerAdded(GameObject addedObj);
    public static OnPlayerAdded playerAddedListeners;

    
    private static float CurrentMoveSpeed;
    
    public static void SetCurrentHeadSpeed(float speed)
    {
        CurrentMoveSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (CurrentMoveSpeed <=1f)
        {
          
            return;
        }

        if (Child != null)
        {
            if (previousPositions.Count >= maxListAmount)
            {
                Child.transform.position = Vector3.Slerp(previousPositions[0], Child.transform.position, 0.1f);
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
        if (Input.GetKeyDown(KeyCode.Z) && allowedSpawning)
        {
            Child = Instantiate(childPrefab);
            allowedSpawning = false;
            playerAddedListeners?.Invoke(Child.gameObject);
        }
    }


}
