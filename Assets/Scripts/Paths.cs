using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{

    public List<Transform> pathNodes = new List<Transform>();
    bool GameStarted = false;

    public Color pathColor;

    public float myXoffset;


    private void Awake()
    {
        GameStarted = true;
        GetPathNodes();
    }

    private void OnDrawGizmos()
    {
       Transform[] paths = transform.GetComponentsInChildren<Transform>();
       
       if (paths == null || paths.Length == 0 || GameStarted)
        {
            return;
        }
      
        Gizmos.color = pathColor;
        for (int i = 0; i < paths.Length-1; i++)
        {
            if (paths[i].transform != transform)
            {
                if (i == 1)
                {
                
                    Gizmos.DrawSphere(paths[paths.Length - 1].transform.position, 0.2f);
                }
                Gizmos.DrawSphere(paths[i].transform.position, 0.2f);
                Gizmos.DrawLine(paths[i].transform.position, paths[i + 1].transform.position);
                Debug.Log("ran : " + i);

            }
        }
        
    }
  

    void GetPathNodes()
    {
        Transform[] nodes = GetComponentsInChildren<Transform>();
        foreach (var item in nodes)
        {
            if (item.transform != transform && item.CompareTag("Paths"))
            {
                pathNodes.Add(item);
            }
        }
    }
}
