using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public float health = 100.0f, maxHealth = 100.0f;
    public Slider healthBar;

    //Grab all rb's on character and set kinematic value
    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }

    // Use this for initialization
    void Start () {
        //Sets up initial UI values
        healthBar.value = calcHealth();

        //Give full controll of rb's to animator
        SetKinematic(true);
    }
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            //isDead();
            Debug.Log("YOU DIED!");
        }
	}

    //Updates the health bar
    float calcHealth()
    {
        return health / maxHealth;
    }

    //Called from enemy scripts (damages player)
    public void dealDamage(float damageVal)
    {
        //take out damage amount
        health -= damageVal;
        //Bar updates new value
        healthBar.value = calcHealth();
    }

    //Called from pickup script (heals player)
    public void addHealth(float newHealth)
    {
        //Only take in new health if damaged
        if (health < maxHealth)
        {
            health += newHealth;

            //Defaults health to 100 if exceeded limit
            if (health > maxHealth)
            {
                health = 100;
            }

            healthBar.value = calcHealth();
        }
    }

    public void isDead() {
        //Surrender full controll to physics
        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        GetComponent<AimIK>().enabled = false;
        GetComponent<LimbIK>().enabled = false;
        GetComponent<Movement>().enabled = false;
        GetComponentInChildren<RaycastGun>().enabled = false;
        //switch to spectator camera
        //bring respawn up dialogue
        //award other points
    }
}
