using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{
    public float speedMax = 110f;
    public float speedCurrent;
    public Text speedText;
    public Slider speedSlider;
    public AudioSource speedPickupAudio;
    public float speedBoostTimer = 0f;
    public int speedBoostCount = 3;
    public Text speedBoostCountText;
    public Component[] children;

    private bool speedBoosting = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        children = GetComponentsInChildren<ParticleSystem>();
        speedBoostCountText.text = "" + speedBoostCount;

        foreach (ParticleSystem childParticleSystem in children)
        {
            childParticleSystem.Stop();
        }
    }

    void Update()
    {
        speedCurrent = rb.velocity.magnitude;
        var mph = speedCurrent * 2.237;
        speedText.text = mph.ToString("f0");
        speedSlider.value = SpeedCalculator() * 2.237f;
        speedBoostCountText.text = "" + speedBoostCount;

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


        //Driver Initiated Boost
        if (speedCurrent <= speedMax)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && speedBoostCount > 0)
            {
                SpeedBoost();
                speedBoostCount = speedBoostCount - 1;
                speedBoostCountText.text = "" + speedBoostCount;
            }
        }
    }

    //Pick Up Boost
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            SpeedBoost();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("SpeedCollect"))
        {
            speedBoostCount = speedBoostCount + 1;
            speedBoostCountText.text = "" + speedBoostCount;
            Destroy(other.gameObject);
        }
    }


    //The Boost
    private void SpeedBoost()
    {
        speedBoosting = true;
        rb.velocity = new Vector3(0, 0, rb.velocity.magnitude * 1.8f);
        speedPickupAudio.Play();

        foreach (ParticleSystem childParticleSystem in children)
        {
            childParticleSystem.Play();
        }

    }

    float SpeedCalculator()
    {
        return speedCurrent / speedMax;
    }
}