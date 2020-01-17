using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // collided with enemy
            Destroy(collision.gameObject);
            HeadFollower.setRemoveRequest(true);
        }
    }
}
