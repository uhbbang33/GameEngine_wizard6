using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Animator anim;
    private RaycastHit hitInfo;

    public int damage;
    public float attackDelay;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void Attack()
    {
        if (Input.GetButton("Fire1"))
        {
            
        }
    }
}
