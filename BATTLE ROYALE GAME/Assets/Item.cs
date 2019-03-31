using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public SessionManager sessionManager;
    public string name = "ak47";
	// Use this for initialization
	void Start () {
        name = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player");
            sessionManager.AddWeapon(this.gameObject);
        }

    }
}
