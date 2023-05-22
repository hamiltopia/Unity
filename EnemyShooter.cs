using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    public float alertRadius = 20f, shootRadius = 10f, rotationSpeed = 5f, fireRate = 6f, lastfired;

    public Transform target;

    public GameObject firePointAlpha, firePointBeta, shot;

    public AudioSource gunFireSource;

    private bool m_alternate = false;


    void OnEnable()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < alertRadius)
        {
            FaceTarget();
        }


        if (distanceToPlayer <= shootRadius)
        {

            if (Time.time - lastfired > 1 / fireRate)
            {

                lastfired = Time.time;
                m_alternate = !m_alternate;

                if (m_alternate)
                {
                    Instantiate(shot, firePointAlpha.transform.position, firePointAlpha.transform.rotation);
                    gunFireSource.Play();
                }
                else
                {
                    Instantiate(shot, firePointBeta.transform.position, firePointBeta.transform.rotation);
                    gunFireSource.Play();

                }
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRadius);

    }
}
