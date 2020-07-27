using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wayPoints;

    [SerializeField]
    private NewPatrolling[] fellowAI;

    //[SerializeField]
    //private GameObject[] chaseSpots;

    private NavMeshAgent agent;
    private GameObject player;
    private int currentPoint;

    public bool patrolling;
    //public ChaseMe chaseScript;
    public float chaseRadius = 10f;
    public float attackRadius = 1f;
    public float alertRadius = 2f;


    // Start is called before the first frame update
    void Start()
    {
        patrolling = true;
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        currentPoint = 0;
        agent.destination = wayPoints[currentPoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolling)
        {
            //Chase Player
            if (Vector3.Distance(this.transform.position, player.transform.position) <= chaseRadius)
            {
                AttackPlayer();
                agent.speed = 4.5f;
                agent.destination = player.transform.position;
            }

            //Back to Patrol
            if (Vector3.Distance(this.transform.position, player.transform.position) > chaseRadius)
            {
                agent.speed = 3.5f;
                agent.destination = wayPoints[currentPoint].transform.position;
            }

            //Attack
            if (Vector3.Distance(this.transform.position, player.transform.position) < attackRadius)
            {
                Debug.Log("Attack");
            }

            //Back to Patrol
            if (Vector3.Distance(this.transform.position, wayPoints[currentPoint].transform.position) <= alertRadius)
            {
                Iterate();
            }
        }

        //Not Patrolling
        if (!patrolling)
        {
            if (CheckDistance())
            {
                AttackPlayer();
            }
        }
        else
        {
            patrolling = true;
        }
        
    }

    //Cycle through Waypoints
    void Iterate()
    {
        if (currentPoint < wayPoints.Length - 1)
        {
            currentPoint++;
        }
        else
        {
            currentPoint = 0;
        }

        agent.destination = wayPoints[currentPoint].transform.position;
    }


    void AttackPlayer()
    {
        //int k = 0;

        foreach(NewPatrolling n in fellowAI)
        {
            n.patrolling = false;
            n.agent.destination = player.transform.position;

            //n.agent.destination = chaseSpots[k].transform.position;
            //k++;
            //if (k>chaseSpots.Length)
            //{
            //    k = 0;
            //}
        }
    }

    //Checking AI in range
    public bool CheckDistance()
    {
        for (int i = 0; i <fellowAI.Length; i++)
        {
            if (Vector3.Distance(fellowAI[i].transform.position, player.transform.position) <= 10f)
            {
                return true;
            }
        }
        //chaseScript.EmptySpots();
        return false;
    }




    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

    }
}
