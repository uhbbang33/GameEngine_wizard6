using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform player;

    Rigidbody rigid;
    NavMeshAgent nav;
    Animator anim;

    public int health;

    public LayerMask whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;

        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();

        if (!playerInAttackRange)
            anim.SetBool("isAttack", false);

        if (!playerInSightRange)
        {
            anim.SetBool("isWalk", false);
            nav.SetDestination(transform.position);
        }

        if(health == 0)
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isAttack", false);

            nav.SetDestination(transform.position);
            Destroy(gameObject, 4);

            anim.SetTrigger("doDie");
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void ChasePlayer()
    {
        anim.SetBool("isWalk", true);
        nav.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        nav.SetDestination(transform.position);

        transform.LookAt(player);
        anim.SetBool("isAttack", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            health -= 50;
            anim.SetBool("isGetHit", true);
            Debug.Log("∏ÛΩ∫≈Õ HP: " + health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isGetHit", false);
    }
}
