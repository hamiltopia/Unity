using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleNitroCustom : MonoBehaviour
{
	public enum Mode { Acceleration, Impulse };
	public Mode mode = Mode.Acceleration;
	public float value = 10.0f;
	public float maxVelocity = 50.0f;
	public KeyCode key = KeyCode.N;

	public float speedCurrent;
	public Text speedText;
	public Slider speedSlider;
	public float speedBoostTimer = 0f;
	public int speedBoostCount = 3;
	public Component[] children;
	public Text speedBoostCountText;


	Rigidbody m_rigidbody;
	private bool speedBoosting = false;


	void OnEnable()
	{
		m_rigidbody = GetComponent<Rigidbody>();
		children = GetComponentsInChildren<ParticleSystem>();
		speedBoostCountText.text = "" + speedBoostCount;

		foreach (ParticleSystem childParticleSystem in children)
		{
			childParticleSystem.Stop();
		}
	}


	void Update()
	{

		//speedCurrent = rb.velocity.magnitude;
		//var mph = speedCurrent * 2.237;
		//speedText.text = mph.ToString("f0");
		//speedSlider.value = SpeedCalculator() * 2.237f;
		//speedBoostCountText.text = "" + speedBoostCount;


		if (mode == Mode.Impulse)
		{
			speedBoostTimer += Time.deltaTime;

			if (Input.GetKeyDown(key) && m_rigidbody.velocity.magnitude < maxVelocity && speedBoostCount > 0)
			{
				m_rigidbody.AddRelativeForce(Vector3.forward * value, ForceMode.VelocityChange);
				speedBoostCount = speedBoostCount - 1;
				speedBoostCountText.text = "" + speedBoostCount;
				speedBoosting = true;

				foreach (ParticleSystem childParticleSystem in children)
				{
					childParticleSystem.Play();
				}
			}
		}

		//Speed Boosting
		if (speedBoosting)
		{
			speedBoostTimer += Time.deltaTime;

			if (speedBoostTimer >= 3)
			{
				speedBoostTimer = 0f;
				speedBoosting = false;

				foreach (ParticleSystem childParticleSystem in children)
				{
					childParticleSystem.Stop();
				}
			}
		}
	}



	void FixedUpdate ()
		{
		if (mode == Mode.Acceleration)
		{

			speedBoostTimer += Time.deltaTime;

			if (Input.GetKey(key) && m_rigidbody.velocity.magnitude < maxVelocity && speedBoostCount > 0)
			{
				m_rigidbody.AddRelativeForce(Vector3.forward * value, ForceMode.Acceleration);
				speedBoostCount = speedBoostCount - 1;
				speedBoostCountText.text = "" + speedBoostCount;
				speedBoosting = true;

				foreach (ParticleSystem childParticleSystem in children)
				{
					childParticleSystem.Play();
				}
			}

		}
	}


	//Pick Up Boost
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("SpeedBoost"))
		{
			//do nothing for now
			Destroy(other.gameObject);
		}

		if (other.gameObject.CompareTag("SpeedCollect"))
		{
			speedBoostCount = speedBoostCount + 1;
			speedBoostCountText.text = "" + speedBoostCount;
			Destroy(other.gameObject);
		}
	}


	float SpeedCalculator()
	{
		return speedCurrent / speedMax;
	}

}
