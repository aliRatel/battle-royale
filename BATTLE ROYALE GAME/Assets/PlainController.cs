using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainController : MonoBehaviour
{
   public  NetworkManager networkManager;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > 1800f)
        {
            if (networkManager == null)
            {
                networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();
            }
            networkManager.KickPlayers();
        }
        if (transform.position.x > 3200) Destroy(gameObject);
    }
}
