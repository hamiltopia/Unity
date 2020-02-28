using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    private float fireRate = .2f;

    [SerializeField]
    [Range(1, 10)]
    private int damage = 1;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private AudioSource gunFireSource;

    private float timer;

    [SerializeField]
    private Transform firePoint;


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1")) {
                timer = 0f;
                FireGun();
            }
        }


    }

    // FireGun
    private void FireGun()
    {

        // Play these things on Fire
        muzzleParticle.Play();
        gunFireSource.Play();

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(firePoint.position, firePoint.forward * 30, Color.blue, 1f);


        // Gun Hit
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            //Destroy(hitInfo.collider.gameObject);
            var health = hitInfo.collider.GetComponent<Health>();

            if (health != null) {
                health.TakeDamage(damage);
            }
        }
    }
}
