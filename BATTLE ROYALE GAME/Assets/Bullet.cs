﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public int ownerID ;
    public int damage;
    public GameObject bloodSplash;
    
   

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerExit(Collider other)
    {
        EnemyPlayer enemyPlayer;
        GameObject g;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                 health =(int)( damage * 0.7);
                enemyPlayer.dicreaseHealth(health,ownerID);
                Debug.Log(other.gameObject.name); ;
             g  =  Instantiate(bloodSplash, other.transform.position,other.transform.rotation) as GameObject;
                Destroy(this);
                Destroy(g, 1f);

                break;

            case "swat:Head":
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                 health = damage ;
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log("trigger");
                  g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(this);
                Destroy(g, 1f);

                break;
            case "swat:Spine":
                Debug.Log("trigger");

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                 health = (int)(damage * 0.8);
                enemyPlayer.dicreaseHealth(health, ownerID);
                  g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(this);
                Destroy(g, 1f);

                break;

            default:
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                health = (int)(damage * 0.5);
                enemyPlayer.dicreaseHealth(health, ownerID);
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(this);
                Destroy(g, 1f);
                Destroy(this);
                break;
        }
    }
}
