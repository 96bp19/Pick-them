using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenrator : MonoBehaviour
{

    public AI_movement AIPrefab;
    public float AI_spawnDelay = 2;
    List<Transform> pathnodes;

    void Start()
    {
        Paths path = GetComponent<Paths>();
        if (path)
        {
            pathnodes = path.pathNodes;
        }
        InvokeRepeating("SpawnAI", 0, AI_spawnDelay);
    }

    void SpawnAI()
    {
        Transform spawnedObj = Instantiate(AIPrefab).transform;
        spawnedObj.rotation = Quaternion.Euler(0, 90, 0);
        spawnedObj.position = pathnodes[0].position;
        spawnedObj.GetComponent<AI_movement>().StartMoving(pathnodes);
        spawnedObj.SetParent(transform);
    }

   
}
