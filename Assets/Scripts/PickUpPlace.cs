using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlace : MonoBehaviour
{
    bool allowedPicking = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // near pickup location
            if (other.GetComponent<Player>().currentSpeed <1f)
            {
                allowedPicking = true;
               
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HeadFollower.setSpawnRequest(allowedPicking);
        }
    }
}
