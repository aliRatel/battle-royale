﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public SessionManager sessionManager;
    public string itemName;
    public int id;
    public int magsize ;
    public int currentMag;
    public int spareAmmo;
    public GameObject bulletPoint;
    public GameObject muzzleFire;
    public AudioClip shoot, reload;
    public AudioSource sound;
    public float fireRate;
    public GameObject canvas;
    public bool canPickUp = false;
    internal string action;
    private void Awake()
    {
        action = "pick";
        canvas = transform.Find("Canvas").gameObject;
        sound = GetComponent<AudioSource>();
        sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();
        currentMag = magsize;
        fireRate = 0.1f;
        Rigidbody itemRb =GetComponent<Rigidbody>();
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;

        itemRb.isKinematic = false;
    }
    // Use this for initialization
    void Start () {
        action = "pick";
        canvas = transform.Find("Canvas").gameObject;
        sound = GetComponent<AudioSource>();
        sound.Stop();
        sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();
        //bulletPoint = transform.Find("bullet Point").gameObject;
        fireRate = 0.1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E)&& canPickUp &&GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>().status!="dead")
        {
            
                
            canPickUp = false;
            action = "pick";
            sessionManager.PickWeapon(gameObject);
            
        }
        if (sessionManager==null) sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();


        if (canvas.activeSelf)
            canvas.transform.LookAt(Camera.main.transform.position);
        
    
}


    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("collid");
        if (other.gameObject.tag == "localPlayer")
        {
            canvas.SetActive(true);
            canPickUp = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "localPlayer")
        {
            canPickUp = false;
            canvas.SetActive(false);
        }
    }

    

}
