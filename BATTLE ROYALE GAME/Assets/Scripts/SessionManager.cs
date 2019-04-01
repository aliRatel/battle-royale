using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    public Player[] players;
    public  GameObject[] playersObjects;
    public int playerId;
    public  bool sessionAprroved = false;
    public Animator anim;
    public GameObject localPlayer;
    public GameObject weaponHolder;
    public PlayerShooter playerShooter;
    public GameObject playerPrefab;
    public GameObject ak_prefab;

    //public  void AddWeapon(string name)
    //{
    //    UnityEngine.Object pPrefab = Resources.Load("Assets/prefaps/weapon/"+name);

    //    GameObject weapon = Instantiate(pPrefab, weaponHolder.transform.position,weaponHolder.transform.rotation) as GameObject;
    //    Rigidbody itemRb = weapon.GetComponent<Rigidbody>();
    //    itemRb.isKinematic = true;
    //    weapon.transform.parent = localPlayer.transform;
    //}

    internal void AddWeapon(GameObject temp)
    {
        switch (temp.name.ToLower())
        {
            case "ak_47" :
                GameObject weapon = Instantiate(ak_prefab, weaponHolder.transform.position, weaponHolder.transform.rotation) as GameObject;
                weapon.transform.Find("Canvas").gameObject.SetActive(false);
            GameObject.Destroy(temp);
            Rigidbody itemRb = weapon.GetComponent<Rigidbody>();
                weapon.GetComponent<BoxCollider>().enabled = false;
                weapon.GetComponent<CapsuleCollider>().enabled = false;

                itemRb.isKinematic = true;
            weapon.transform.parent = weaponHolder.transform;
               playerShooter =  localPlayer.GetComponent<PlayerShooter>();
                weapon.transform.Find("bullet point").transform.Find("muzzle fire").gameObject.SetActive(true);
                playerShooter.addWeapon(weapon);
                //todo networking
                break;
        }
    
    }

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

  

    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("Player");
        
        players = new Player[100];
        playersObjects = new GameObject[100];

    }
  
    // Update is called once per frame
    void Update()
    {

    }

    internal void AddNewPlayer(NetworkManager.PlayerJson playerJson)
    {

        Vector3 position = new Vector3(playerJson.position[0], playerJson.position[1], playerJson.position[2]);
        Quaternion rotation = new Quaternion(playerJson.rotation[0], playerJson.rotation[1], playerJson.rotation[2], 0.0f);
        int sessionId = playerJson.sessionId;



        //players[sessionId] = player;
        if (players[sessionId] == null && playersObjects[sessionId] == null)
        {
            Player player = new Player(position, rotation, sessionId);
            players[sessionId] = player;
            GameObject pl = Instantiate(playerPrefab, position, rotation) as GameObject;
            playersObjects[sessionId] = pl;
        }

        //Debug.Log("posi" + pl.transform.position);

    }

    internal void movePlayer(Vector3 pos, int sessionId)
    {

        
        playersObjects[sessionId].transform.position = Vector3.Lerp(playersObjects[sessionId].transform.position,pos,10f*Time.deltaTime);

    }

    internal void RotatePlayer(Quaternion rot, int sessionId)
    {
        
            playersObjects[sessionId].transform.rotation = Quaternion.Euler(rot.x,rot.y,rot.z);
    }

    internal void AnimatePlayer(NetworkManager.AnimationJson animationJson)
    {
        anim = playersObjects[animationJson.sessionId].GetComponentInChildren<Animator>();
        anim.SetFloat("Horizontal", animationJson.horizontal);
        anim.SetFloat("Vertical", animationJson.vertical);
        anim.SetBool("run", animationJson.run);
        anim.SetBool("isHustler", animationJson.isHustler);
        anim.SetBool("isAiming", animationJson.isAiming);
        anim.SetBool("isCrouching", animationJson.isCrouching);
    }

    public void setTrue()
    {
        sessionAprroved = true;
    }
}
