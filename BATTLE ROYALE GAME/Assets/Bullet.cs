using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public int ownerID ;
    public int damage;
   

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager enemyPlayerManager;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                enemyPlayerManager = other.gameObject.transform.root.GetComponent<PlayerManager>();
                 health =(int)( damage * 0.7);
                enemyPlayerManager.dicreaseHealth(health);
                break;

            case "swat:Head":
                enemyPlayerManager = other.gameObject.transform.root.GetComponent<PlayerManager>();
                 health = damage ;
                enemyPlayerManager.dicreaseHealth(health);
                break;
            case "swat:Spine":
                enemyPlayerManager = other.gameObject.transform.root.GetComponent<PlayerManager>();
                 health = (int)(damage * 0.8);
                enemyPlayerManager.dicreaseHealth(health);
                break;

            default:

                break;
        }
    }
}
