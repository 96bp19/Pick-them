using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_movement : MonoBehaviour
{
    List<Transform> paths =  new List<Transform>();
    bool beginPlay;
    private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    int currentPathIndex = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void StartMoving( List<Transform> path)
    {
        paths = path;
        beginPlay = true;
        
    }

    private void Update()
    {
        if (beginPlay)
        {
            
            rb.MovePosition(transform.forward * moveSpeed * Time.fixedDeltaTime + transform.position);
            if (Vector3.Distance(transform.position , paths[currentPathIndex].position) <5)
            {
                currentPathIndex++;
            }

            if (currentPathIndex == paths.Count-1)
            {
                Debug.Log("shit destroyed");
             //   beginPlay = false;
                Destroy(this.gameObject);
            }
           
        }
    }
}
