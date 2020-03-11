using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Bullet_Emitter;

    public GameObject Bullet;

    public float Bullet_Forward_Force = 1200f;

    private float timer;

    [SerializeField]
    private float fireRate = .2f;

    //[SerializeField]
    //[Range(1, 10)]
    //private int damage = 1;

    [SerializeField]
    private AudioSource gunFireSource;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0f;
                FireGun();
            }
        }
    }


    private void FireGun()
    {
        gunFireSource.Play();

        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

        Destroy(Temporary_Bullet_Handler, 5.0f);
    }

}
