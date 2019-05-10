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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dicreaseHealth(int health ,int id )
    {
        networkManager.HitPlayer(health, id);
    }
}
