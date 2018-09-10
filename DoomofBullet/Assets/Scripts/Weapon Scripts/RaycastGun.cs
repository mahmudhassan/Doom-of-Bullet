using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RaycastGun : MonoBehaviour {

    GunRecoil recoilComponent;
    public Transform gunTip;
    public FixedButton fireButton;
    public ParticleSystem flash;   
    public AudioSource weaponSound;
    public float recoilParam = 0.2f, maxRecoil_xParam = 10f, recoilSpeedParam = 10f;
    public bool isAutomatic = false;
    bool readyToFire = true;
    public float rateOfFire = 5.0f;
    public float damageAmount = 2;
    public float range = 100f;
    public int clipSize = 30;
    public int clips = 5; //ammo refills use up one clip 
    int ammo = 0;
    float lastFired;

    private float nextTimeToFire = 0f;
    public float impactForce = 30f;
    public GameObject impactEffect;
    public Text text;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    public bool isReloading = false;

    // Use this for initialization
    void Start () {
        //Fill to initial clip size
        ammo = clipSize;

        recoilComponent = gameObject.GetComponent<GunRecoil>();

        //fireButton = ??? need to get ref at runtime?

        currentAmmo = maxAmmo;
    }
	
	// Update is called once per frame
	void Update () {
        text.text = "Ammo: " + currentAmmo + "/30";

        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        else if(currentAmmo < 30 && Input.GetKeyDown("r"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / rateOfFire);
            shoot();
            currentAmmo -= 1;
        }

        /*
        if (isAutomatic)
        { //Full-Automatic Path for ballistic & hitscan
            if (fireButton.Pressed && ammo > 0)
            {
                //Mechanism for full auto/rate of fire
                if (Time.time - lastFired > 1 / rateOfFire)
                {
                    lastFired = Time.time;
                    shoot();
                }
            }
        }
        else
        { //Semi-Automatic Path for ballistic & hitscan
            if (fireButton.Pressed && ammo > 0)
            {
                //Fire Rate Controlled Boolean
                if (readyToFire && !fireButton.hasBeenShot)
                {
                    StartCoroutine(fireLock());
                    shoot();

                    //Flag that shot has happened once
                    fireButton.hasBeenShot = true;
                    //set back to false only onButtonUp
                }
            }
        } //Closes main structure   
        */
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    public void shoot() {
        //The Ray Itself
        Ray ray = new Ray(gunTip.position, gunTip.forward);
        RaycastHit hit;
        
        recoilComponent.StartRecoil(recoilParam, maxRecoil_xParam, recoilSpeedParam);

        Debug.DrawRay(gunTip.position, gunTip.forward * 500, Color.yellow);
        if(weaponSound != null)
            weaponSound.Play();
        if(flash != null)
            flash.Play();

        //What Happens When Ray Hits Player 
        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("The ray hit a sossy: " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damageAmount);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    IEnumerator fireLock()
    {
        readyToFire = false;
        //Use rate of fire as rate for semi autos
        yield return new WaitForSeconds(rateOfFire);
        readyToFire = true;
    }
}
