﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 scale;
    public float radius;
    private ParticleSystem.ShapeModule shape;
    public ParticleSystem ps;
      
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("localPlayer").gameObject;
        ps = GetComponent<ParticleSystem>();
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerExit(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("networkManager").GetComponent<NetworkManager>().status != "on ground") return;

        if (other.CompareTag("localPlayer"))
        {
            player.GetComponent<PlayerManager>().inZone = false;

            Debug.Log("exit");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("networkManager").GetComponent<NetworkManager>().status != "on ground") return;
        if (other.CompareTag("localPlayer") )
        {
            Debug.Log("enter");
            player.GetComponent<PlayerManager>().inZone = true;
        }
    }


    public void DecreaseSize(float percentage)
    {

        transform.localScale = new Vector3(scale.x - (scale.x * percentage), scale.y, scale.z - (scale.z * percentage));


    }
}
