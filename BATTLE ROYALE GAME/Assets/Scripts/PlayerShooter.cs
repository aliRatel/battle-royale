using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    public Item firstWeapon;
    public Item SecondWeapon;
    public int firstWeaponAmmo;
   public int secondWeaponAmmo;
    public Item currentWeapon;
    public GameObject bulletPrefab;
    private float nextFire;
    public bool isReloading;
    Coroutine startSomeCoroutine;
    public HUDManager hUDManager;

    // Use this for initialization
    void Start () {
        isReloading = false;
	}
	
	// Update is called once per frame
	void Update () {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        float z;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            z = hit.distance;
            if (hit.collider.isTrigger )
            {
                z += 400;
            }
        }
        else
        {
            z = 1000;
        }
        
            Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f, 0.922f, 0.016f, 1f));





        if (Input.GetButton("Fire1"))
        {
            if (firstWeapon.currentMag > 0 && !isReloading)
            {

                firstWeapon.bulletPoint.transform.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, z)));
                if (Time.time > nextFire) {
                    firstWeapon.muzzleFire.GetComponent<ParticleSystem>().Play();

                    nextFire = Time.time + firstWeapon.fireRate;
                    if (!firstWeapon.sound.isPlaying)
                        firstWeapon.sound.Play();
                    //todo fire
                    GameObject bullet = Instantiate(bulletPrefab, firstWeapon.bulletPoint.transform.position, firstWeapon.bulletPoint.transform.localRotation) as GameObject;
                    bullet.GetComponent<Rigidbody>().velocity = firstWeapon.bulletPoint.transform.forward * 25;
                    // todo start coroutine to destroy bullet
                    //todo manage bullets
                    firstWeapon.currentMag--;

                    //todo networking shooting
                }
                hUDManager.SetAmmo(firstWeapon);
            }
            else if (firstWeapon.spareAmmo > 0)
            {
                if (!isReloading)
                    StartCoroutine(reload());


            }
            else return;
        } else if (Input.GetKey(KeyCode.R) && firstWeapon.spareAmmo > 0 && !isReloading && firstWeapon.currentMag < firstWeapon.magsize)
        {

            startSomeCoroutine = StartCoroutine(reload());


        } else if (Input.GetKey(KeyCode.G) && firstWeapon != null)
        {
            isReloading = false;
            GameObject weapon = firstWeapon.gameObject;
            weapon.transform.SetParent(null);
            firstWeapon = null;
            weapon.transform.position = gameObject.transform.position + Vector3.forward*3;
            weapon.GetComponent<BoxCollider>().enabled = true;
            weapon.GetComponent<CapsuleCollider>().enabled = true;
            weapon.GetComponent<Rigidbody>().isKinematic = false;

        }
    }

    IEnumerator reload()
    {
        isReloading = true;
        Debug.Log("before");
        firstWeapon.sound.clip = firstWeapon.reload;
        firstWeapon.sound.Play();
        yield return new WaitForSeconds(3f);
        Debug.Log("after");

        
            firstWeapon.sound.Play();

            int bullets = firstWeapon.magsize - firstWeapon.currentMag;
        if (firstWeapon.spareAmmo > firstWeapon.magsize)
        {
            firstWeapon.currentMag += bullets;
            firstWeapon.spareAmmo -= bullets;

        }

        else
        {
            firstWeapon.currentMag += firstWeapon.spareAmmo;
            firstWeapon.spareAmmo = 0;
        }

        hUDManager.SetAmmo(firstWeapon);

        isReloading = false;
        firstWeapon.sound.clip = firstWeapon.shoot;

    }

    internal void addWeapon(GameObject weapon)
    {
        if(firstWeapon == null)
        {
            firstWeapon = weapon.GetComponent<Item>();
            hUDManager.SetAmmo(firstWeapon);

        }
        else if(SecondWeapon == null)
        {
            SecondWeapon = weapon.GetComponent < Item > ();
        }
    }
}
