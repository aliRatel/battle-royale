using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health;
    public int armor;
    public int sessionId;
    // Start is called before the first frame update
    private void Awake()
    {
        health = 100;
        armor = 0;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    internal void dicreaseHealth(int health)
    {
       this.health -= health;
        //todo networking;
    }

    internal void setId(int id)
    {
        sessionId = id;
    }
}
