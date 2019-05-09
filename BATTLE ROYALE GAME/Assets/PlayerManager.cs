using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int health;
    public int armor;
    public int sessionId;
    public bool inZone;
    public float zoneHitRate;
    public float zoneNextHit;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("awake player");
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        health = 100;
        armor = 0;
    }
    void Start()
    {
        zoneHitRate = 1f;
        zoneNextHit = 0f;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("spawn point");
            int a = UnityEngine.Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[a];

            transform.position = spawnPoint.transform.position;
            GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>().JoinSession();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inZone) {
            zoneNextHit += 0.05f;
        if (zoneNextHit >= zoneHitRate)
        {
                zoneNextHit = 0f;
                dicreaseHealth(1);
        }
        }
        else
        {
            zoneNextHit = 0f;
        }
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
