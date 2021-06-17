using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform player;

    Rigidbody rigid;
    public NavMeshAgent nav;
    Animator anim;

    public int health;

    public LayerMask whatIsPlayer, whatIsGround;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

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

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();

        if (!playerInAttackRange)
            anim.SetBool("isAttack", false);

        if (!playerInSightRange)
        {
            anim.SetBool("isRun", false);
            nav.SetDestination(transform.position);
        }

        if (health <= 0)
        {
            nav.SetDestination(transform.position);
            Destroy(gameObject, 5);

            anim.SetTrigger("doDie");
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            nav.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,
            transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    void ChasePlayer()
    {
        anim.SetBool("isRun", true);
        if (health > 0)
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
        if (other.CompareTag("Bullet"))
        {
            health -= 50;
            anim.SetBool("isGetHit", true);
            Debug.Log("몬스터 HP: " + health);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isGetHit", false);
    }
}
