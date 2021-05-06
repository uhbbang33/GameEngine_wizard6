using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    
    private bool collided;
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Bullet" && col.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            var impact = Instantiate(impactVFX, col.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, 2);
            Destroy(gameObject);
        }
    }
}
