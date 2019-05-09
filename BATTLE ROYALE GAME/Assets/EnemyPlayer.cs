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
    public int health;
    public int armor;
    public int id;
    private void Awake()
    {
        networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dicreaseHealth(int health ,int playerId )
    {
        this.health -=health;
        networkManager.HitPlayer(health, playerId);
    }
}
