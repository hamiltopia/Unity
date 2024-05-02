using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public float fireRate = .2f;
    //private float timer;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Pickup")
        {

            //You can either use a new Vector 3 
            trigger.gameObject.SetActive(false);

            //or have a GameObject and get its position
            //this.transform.position = mySpawnGameObject.transform.position;
            //Where mySpawnGameObject is a public GameObject variable that you assign from the inspector


        }
    }
}
