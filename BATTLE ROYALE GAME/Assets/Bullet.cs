using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
    public int ownerID ;
    public int damage;
    public bool detected;
    public GameObject bloodSplash;
    
   

    // Start is called before the first frame update
    void Start()
    {
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (detected == true) return;
        Collider other = collision.collider;
        EnemyPlayer enemyPlayer;
        GameObject g;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                detected = true;
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);
                health = (int)(damage * 0.7);
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log(other.gameObject.name); ;
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);

                break;

            case "swat:Head":
                detected = true;

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);

                health = damage;
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log("trigger");
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);


                break;
            case "swat:Spine":
                detected = true;

                Debug.Log("trigger");

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;

                health = (int)(damage * 0.8);
                enemyPlayer.dicreaseHealth(health, ownerID);
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;

                Destroy(g, 1f);
                Destroy(this);


                break;

                //default:
                //    enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                //    ownerID = enemyPlayer.id;

                //    health = (int)(damage * 0.5);
                //    enemyPlayer.dicreaseHealth(health, ownerID);
                //    g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                //    Destroy(g, 1f);
                //    break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Collider other = collision.collider;
        EnemyPlayer enemyPlayer;
        GameObject g;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                detected = true;
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);
                health = (int)(damage * 0.7);
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log(other.gameObject.name); ;
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);

                break;

            case "swat:Head":
                detected = true;

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);

                health = damage;
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log("trigger");
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);


                break;
            case "swat:Spine":
                detected = true;

                Debug.Log("trigger");

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;

                health = (int)(damage * 0.8);
                enemyPlayer.dicreaseHealth(health, ownerID);
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;

                Destroy(g, 1f);
                Destroy(this);


                break;

                //default:
                //    enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                //    ownerID = enemyPlayer.id;

                //    health = (int)(damage * 0.5);
                //    enemyPlayer.dicreaseHealth(health, ownerID);
                //    g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                //    Destroy(g, 1f);
                //    break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (detected == true) return;

        EnemyPlayer enemyPlayer;
        GameObject g;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                detected = true;
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);
                health = (int)(damage * 0.7);
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log(other.gameObject.name); ;
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);

                break;

            case "swat:Head":
                detected = true;

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);

                health = damage;
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log("trigger");
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);


                break;
            case "swat:Spine":
                detected = true;

                Debug.Log("trigger");

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;

                health = (int)(damage * 0.8);
                enemyPlayer.dicreaseHealth(health, ownerID);
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;

                Destroy(g, 1f);
                Destroy(this);


                break;

                //default:
                //    enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                //    ownerID = enemyPlayer.id;

                //    health = (int)(damage * 0.5);
                //    enemyPlayer.dicreaseHealth(health, ownerID);
                //    g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                //    Destroy(g, 1f);
                //    break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        EnemyPlayer enemyPlayer;
        GameObject g;
        int health;
        switch (other.gameObject.name)
        {
            case "swat:Hips":
                detected = true;
                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);
                health = (int)(damage * 0.7);
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log(other.gameObject.name); ;
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);

                break;

            case "swat:Head":
                detected = true;

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;
                Debug.Log(ownerID);

                health = damage;
                enemyPlayer.dicreaseHealth(health, ownerID);
                Debug.Log("trigger");
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                Destroy(g, 1f);
                Destroy(this);


                break;
            case "swat:Spine":
                detected = true;

                Debug.Log("trigger");

                enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                ownerID = enemyPlayer.id;

                health = (int)(damage * 0.8);
                enemyPlayer.dicreaseHealth(health, ownerID);
                g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;

                Destroy(g, 1f);
                Destroy(this);


                break;

                //default:
                //    enemyPlayer = other.gameObject.transform.root.GetComponent<EnemyPlayer>();
                //    ownerID = enemyPlayer.id;

                //    health = (int)(damage * 0.5);
                //    enemyPlayer.dicreaseHealth(health, ownerID);
                //    g = Instantiate(bloodSplash, other.transform.position, other.transform.rotation) as GameObject;
                //    Destroy(g, 1f);
                //    break;
        }
    }

}
