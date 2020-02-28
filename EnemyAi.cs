using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAi : MonoBehaviour
{

    public float alertRadius;
    public float hitRadius;

    public float speed;

    Transform target;
    NavMeshAgent agent;

    private float timer;

    // Wander 
    public float wanderRadius = 10f;
    public float wanderTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        speed = agent.speed;
        alertRadius = agent.stoppingDistance * 5f;
        hitRadius = agent.stoppingDistance;

        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // Look and See
        if (distance <= alertRadius)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
        else if (distance <= hitRadius)
        {
            FaceTarget();
            Debug.Log("Attack!!!");
        }
        //Wandering
        else if (distance > alertRadius)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);

    }

}
