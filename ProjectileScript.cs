using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileLifeSpan = 2f;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= projectileLifeSpan)
        { 
            timer = 0f;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
