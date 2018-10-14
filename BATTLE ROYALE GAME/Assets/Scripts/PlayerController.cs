using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.x + 0.0011f); ;
        }
	}
}
