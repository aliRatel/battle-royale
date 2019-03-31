using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    public GameObject w1Ammo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAmmo(Item item)
    {
        Text t = w1Ammo.GetComponent<Text>();
        t.text = item.currentMag + "/" + item.spareAmmo;
    }
}
