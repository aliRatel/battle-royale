using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{
    public Item firstWeapon;
    public Item SecondWeapon;
    public Item currentWeapon;
    public GameObject weaponHolder;
    public NetworkManager networkManager;
    public Animator anim;
    public int health;
    public int armor;
    public int id;
    public bool isLerpingPosition;
    public Vector3 realPosition;
    public Vector3 lastRealPosion;
    public float timestartedLerping;
    public float timeToLerp;
    
    private void Awake()
    {
        health = 100;
        DontDestroyOnLoad(this);
        networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isLerpingPosition = false;
        realPosition = transform.position;
    }

    // Update is called once per frame
    
    private void FixedUpdate()
    {
        NetworkLerp();
    }

    private void NetworkLerp()
    {
        if (isLerpingPosition)
        {
            float lerpPercentage = (Time.time - timestartedLerping) / timeToLerp;
            transform.position = Vector3.Lerp(lastRealPosion, realPosition, lerpPercentage); 
        }
    }

    public void dicreaseHealth(int health ,int id )
    {
        networkManager.HitPlayer(health, id);
    }
}
