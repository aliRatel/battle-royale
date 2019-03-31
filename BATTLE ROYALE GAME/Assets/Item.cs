using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public SessionManager sessionManager;
    public string name;
    public int magsize = 30;
    public int currentMag;
    public int spareAmmo = 90;
    public GameObject bulletPoint;
    public AudioSource sound;
    public float fireRate;
    public GameObject canvas;
    public bool canPickUp = false;

    // Use this for initialization
    void Start () {

        canvas = transform.Find("Canvas").gameObject;
        
        sound = GetComponent<AudioSource>();
        sound.Stop();
        name = gameObject.name;
        sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();
        //bulletPoint = transform.Find("bullet Point").gameObject;
        currentMag = magsize;
        fireRate = 0.1f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E)&& canPickUp)
        {
            Debug.Log("player");
            sessionManager.AddWeapon(gameObject);
            canPickUp = false;
            
        }
        if (sessionManager==null) sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();


        if (canvas.activeSelf)
            canvas.transform.LookAt(Camera.main.transform.position);
        
    
}


    private void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);


        if (other.gameObject.tag == "Player")
        {
            canvas.SetActive(true);
            canPickUp = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }

}
