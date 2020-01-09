using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Paths[] levelPrefab;
   
   
    public bool debugMode = false;

    // list of generated platform
    private List<Transform> generatedPlatforms = new List<Transform>();

    // list of waypoints inside generated platforms
    public List<Transform> wayPoints = new List<Transform>();

    

    
    public void GenerateLevel(int noOfPlatformToGenerate)
    {
        // first remove any platforms from before
        DestroyPreviousPlatforms();
        Transform spawnedPlatform;
        Vector3 platformPosition = Vector3.zero;
        for (int i = 0; i < noOfPlatformToGenerate; i++)
        {
           platformPosition.z = i*40;

           spawnedPlatform =  Instantiate(levelPrefab[Random.Range(0,levelPrefab.Length)]).transform;
           spawnedPlatform.SetParent(transform);
           generatedPlatforms.Add(spawnedPlatform);
            // makes list of waypoints
           Paths path = spawnedPlatform.GetComponent<Paths>();
           platformPosition.x += path.myXoffset;
           AddWaypointsToList(path);
           spawnedPlatform.localPosition = platformPosition;
           
            
        }
    }

    void DestroyPreviousPlatforms()
    {
        foreach (var item in generatedPlatforms)
        {
            Destroy(item.gameObject);
        }
        generatedPlatforms.Clear();

    }

    void ClearPreviousPaths()
    {
        wayPoints = new List<Transform>();
    }

    void AddWaypointsToList(Paths path)
    {
        foreach (var item in path.pathNodes)
        {
            wayPoints.Add(item);
        }
    }

    public void initializePlayerPosition( Player player)
    {
        player.transform.position = wayPoints[0].transform.position+  new Vector3(0,1f,0);
        player.initializePlayerWaypoints(wayPoints);
    }

    private void OnDrawGizmos()
    {
        if (wayPoints == null || debugMode == false)
        {
            return;
        }
        Gizmos.color = Color.blue;
        for (int i = 0; i < wayPoints.Count - 1; i++)
        {

            if (i == 0)
            {

                Gizmos.DrawSphere(wayPoints[wayPoints.Count - 1].transform.position, 0.2f);
            }
            Gizmos.DrawSphere(wayPoints[i].transform.position, 0.2f);
            Gizmos.DrawLine(wayPoints[i].transform.position, wayPoints[i + 1].transform.position);



        }
    }
}
