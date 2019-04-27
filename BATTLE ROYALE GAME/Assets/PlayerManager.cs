using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int health;
    public int armor;
    public int sessionId;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        health = 100;
        armor = 0;
    }
    void Start()
    {
        GameObject [] spawnPoints = GameObject.FindGameObjectsWithTag("spawn point");
        int a = UnityEngine.Random.Range(0, spawnPoints.Length);
        GameObject spawnPoint = spawnPoints[a];

        transform.position = spawnPoint.transform.position;
        GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>().JoinSession();
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
