using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Water")
        {

            //You can either use a new Vector 3 
            this.transform.position = new Vector3(0, 2f, 0);

            //or have a GameObject and get its position
            //this.transform.position = mySpawnGameObject.transform.position;
            //Where mySpawnGameObject is a public GameObject variable that you assign from the inspector


        }
    }
}
