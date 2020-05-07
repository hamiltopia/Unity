using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    //Display the Health
    public GameObject healthBarUI;  //Canvas
    public Slider slider;           //Slider

    [SerializeField]
    private int maxHealth = 10;
    private int currentHealth;
    private Animator anim;

    // OnEnable
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        slider.value = CalculateHealth();
    }

    //Taking Damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBarUI.SetActive(true); //Show Healthbar upon injury

        if (anim != null) //Checks to see if there is an Animator
        {
            anim.SetTrigger("damage");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Calculate the Health for the Slider
    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }


    // You Die
    private void Die()
    {

        if (anim != null) //Checks to see if there is an Animator
        {
            anim.SetTrigger("death");
        }

        healthBarUI.SetActive(false);//Hide Healthbar
        Destroy(gameObject, 5f);
    }
    
    
    //Health Pickup
    //if (collision.transform.tag == "HealthPickup")
    //{

    //    if (currentHealth < maxHealth)
    //    {
    //        currentHealth += 2;
    //        healthText.text = "" + currentHealth;
    //        healthSlider.value = CalculateHealth();
    //        Debug.Log("Yummmmmm!");
    //    }
    //    else if (currentHealth >= maxHealth)
    //    {
    //        currentHealth = maxHealth;
    //        healthText.text = "" + currentHealth;
    //        healthSlider.value = CalculateHealth();
    //    }

    //    Destroy(collision.gameObject);
    //}


}
