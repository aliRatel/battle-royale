﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    public Item firstWeapon;
    public Item SecondWeapon;
    public int firstWeaponAmmo;
   public int secondWeaponAmmo;
    public Item currentWeapon;
    public Animator animator;
    public GameObject bulletPrefab;
    public SessionManager sessionManager;
    public NetworkManager networkManager;
public    float z;

    private float nextFire;
    public bool isReloading;
    Coroutine startSomeCoroutine;
    public HUDManager hUDManager;
    public GameObject bloodSplash;

    // Use this for initialization
    void Start () {
        isReloading = false;
        sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();
        networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (networkManager.status == "dead") return;

        if (hUDManager == null) { hUDManager = GameObject.FindGameObjectWithTag("hud").GetComponent<HUDManager>(); }

        if (currentWeapon != null)
        {
            animator.SetLayerWeight(animator.GetLayerIndex("aimangle"), 1);
            animator.SetBool("isHustler", true);
        }
        else
        {
            animator.SetBool("isHustler", false);
            animator.SetLayerWeight(animator.GetLayerIndex("aimangle"), 0);
        }



            if (Input.GetKey(KeyCode.Alpha1) && firstWeapon != null)
        {
            hustlerFirstWeapon();
            



        }
        else if (Input.GetKey(KeyCode.Alpha2) && SecondWeapon != null)
        {
            hustlerSecondtWeapon();
           
        }

       





        if (Input.GetButton("Fire1"))
        {

            if (currentWeapon == null) return;
            if (currentWeapon.currentMag > 0 && !isReloading)
            {

                currentWeapon.bulletPoint.transform.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, z)));
                if (Time.time > nextFire) {
                    currentWeapon.muzzleFire.GetComponent<ParticleSystem>().Play();

                    nextFire = Time.time + currentWeapon.fireRate;
                    if (!currentWeapon.sound.isPlaying)
                        currentWeapon.sound.Play();
                    //todo fire
                    networkManager.FlashMuzzle(currentWeapon);
                    checkBulletHit(trackBullets());

                    //GameObject bullet = Instantiate(bulletPrefab, currentWeapon.bulletPoint.transform.position, currentWeapon.bulletPoint.transform.localRotation) as GameObject;
                   // bullet.GetComponent<Rigidbody>().velocity = currentWeapon.bulletPoint.transform.forward * 7800f*Time.deltaTime;
                  //  bullet.GetComponent<Bullet>().ownerID = networkManager.playerId;
                  //  Debug.Log(bullet.GetComponent<Bullet>().ownerID);
                    // todo start coroutine to destroy bullet
                    //todo manage bullets
                    currentWeapon.currentMag--;

                    //todo networking shooting
                }
                if (currentWeapon == firstWeapon)
                    hUDManager.SetAmmo1(currentWeapon);
                else if (currentWeapon == SecondWeapon)
                    hUDManager.SetAmmo2(currentWeapon);
            }
            else if (currentWeapon.spareAmmo > 0)
            {
                if (!isReloading)
                    StartCoroutine(reload());


            }
            else return;
        } else if (Input.GetKey(KeyCode.R) && currentWeapon.spareAmmo > 0 && !isReloading && currentWeapon.currentMag < currentWeapon.magsize)
        {

            startSomeCoroutine = StartCoroutine(reload());


        } else if (Input.GetKey(KeyCode.G) && currentWeapon != null)
        {
            isReloading = false;


            currentWeapon.action = "drop";
            if (firstWeapon == currentWeapon) hUDManager.drop1();
            if (SecondWeapon == currentWeapon) hUDManager.drop2();

            sessionManager.RemoveWeapon(currentWeapon.gameObject);

            currentWeapon = null;
            


        }
    }

    private void checkBulletHit(Collider other)
    {
        int damage = 10;   
        EnemyPlayer enemyPlayer;
        GameObject g;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                int ownerID = enemyPlayer.id;
                Debug.Log(ownerID);
                health = (int)(damage * 0.7);
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log(other.gameObject.name); ;
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);

                break;

            case "swat:Head":

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);

                health = damage;
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log("trigger");
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);


                break;
            case "swat:Spine":
               

                Debug.Log("trigger");

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;

                health = (int)(damage * 0.8);
                enemyPlayer.dicreaseHealth(health, ownerID);
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;

                Destroy(g, 1f);


                break;

                //default:
                //    enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                //    ownerID = enemyPlayer.id;

                //    health = (int)(damage * 0.5);
                //    enemyPlayer.dicreaseHealth(health, ownerID);
                //    g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                //    Destroy(g, 1f);
                //    break;
        }
    }

    private void hustlerSecondtWeapon()
    {
        if (SecondWeapon == null) return;

        if (currentWeapon == firstWeapon)
        {
            if (firstWeapon != null) firstWeapon.gameObject.SetActive(false);

            SecondWeapon.gameObject.SetActive(true);
        }

        currentWeapon = SecondWeapon;
        hUDManager.SetBg2();
    }

    private void hustlerFirstWeapon()
    {
        if (firstWeapon == null) return;
        if (currentWeapon == SecondWeapon)
        {
            if (SecondWeapon != null) SecondWeapon.gameObject.SetActive(false);
            firstWeapon.gameObject.SetActive(true);
        }

        currentWeapon = firstWeapon;
        hUDManager.SetBg1();

    }

    private Collider trackBullets()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        ray.origin = Camera.main.transform.position;
        ray.origin += Camera.main.transform.forward * 15;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            z = hit.distance;
            if (hit.collider.isTrigger)
            {
                z += 400;
            }
        }
        else
        {
            z = 1000;
        }
        

        Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f, 0.922f, 0.016f, 1f));
        return hit.collider;
    }

    IEnumerator reload()
    {
        isReloading = true;
        Debug.Log("before");
        currentWeapon.sound.clip = currentWeapon.reload;
        currentWeapon.sound.Play();
        yield return new WaitForSeconds(3f);
        Debug.Log("after");


        currentWeapon.sound.Play();

            int bullets = currentWeapon.magsize - currentWeapon.currentMag;
        if (currentWeapon.spareAmmo > currentWeapon.magsize)
        {
            currentWeapon.currentMag += bullets;
            currentWeapon.spareAmmo -= bullets;

        }

        else
        {
            currentWeapon.currentMag += currentWeapon.spareAmmo;
            currentWeapon.spareAmmo = 0;
        }
        if(currentWeapon == firstWeapon)
        hUDManager.SetAmmo1(currentWeapon);
        else if (currentWeapon == SecondWeapon) hUDManager.SetAmmo2(currentWeapon);


        isReloading = false;
        currentWeapon.sound.clip = currentWeapon.shoot;

    }

    internal void AddWeapon(GameObject weapon)
    {
        if(firstWeapon == null)
        {
            firstWeapon = weapon.GetComponent<Item>();
            currentWeapon = firstWeapon;
            hustlerFirstWeapon();
            hUDManager.SetAmmo1(firstWeapon);
            hUDManager.SetImg1(weapon.GetComponent<Item>());


        }
        else if(SecondWeapon == null)
        {
            
            SecondWeapon = weapon.GetComponent < Item > ();
            currentWeapon = SecondWeapon;
            hustlerSecondtWeapon();
            hUDManager.SetAmmo2(SecondWeapon);
            hUDManager.SetImg2(weapon.GetComponent<Item>());



        }
    }


   
}
