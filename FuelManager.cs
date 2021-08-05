using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    public float fuelMax = 100F;
    public float fuelCurrent;
    public float fuelBurnRate = .25f;
    public AudioSource fuelPickupAudio;
    public AudioSource fuelEmptyAudio;
    

    public Text fuelText;
    public Slider fuelSlider;

    private Rigidbody rb;
    private bool canRunAudio = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fuelCurrent = fuelMax / 3;
    }

    void Update()
    {
        //if moving
        if (rb.velocity.magnitude > .05f)
        {
            fuelCurrent -= fuelBurnRate * Time.deltaTime;
            fuelText.text = fuelCurrent.ToString("F0");
            fuelSlider.value = FuelCalculator();
        }

        //fuel stays below max
        if (fuelCurrent > fuelMax)
        {
            fuelCurrent = fuelMax;
        }

        //out of gas
        if (fuelCurrent <= 0f)
        {
            Empty();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FuelRefill"))
        {
            fuelCurrent += 10;
            fuelPickupAudio.Play();
            Destroy(other.gameObject);
        }
    }

    float FuelCalculator()
    {
        return fuelCurrent / fuelMax;
    }

    public void Empty()
    {
        rb.drag = 6;
        fuelCurrent = 0f;
        fuelText.text = "-|-";
        //fuelEmptyAudio.Play();
        Debug.Log("EMPTY!");

        if (canRunAudio)
        {
            fuelEmptyAudio.Play();
            canRunAudio = false;
        }
    }
}
