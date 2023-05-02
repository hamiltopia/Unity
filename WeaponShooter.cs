using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
	public GameObject[] shotSpawns;

	public GameObject shot;

	public float fireRate = 10;  // The number of bullets fired per second
	public float lastfired;      // The value of Time.time at the last firing moment


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (Time.time - lastfired > 1 / fireRate)
			{
				lastfired = Time.time;
				foreach (GameObject ss in shotSpawns)
				{
					Instantiate(shot, ss.transform.position, ss.transform.rotation);
				}

			}
		}
	}
}
