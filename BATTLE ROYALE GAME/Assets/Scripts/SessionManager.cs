using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    public Player[] players;
    public GameObject[] weapons;
    public  GameObject[] playersObjects;
    public int playerId;
    public  bool sessionAprroved ;
    public Animator anim;
    public GameObject localPlayer;
    public GameObject weaponHolder;
    public PlayerShooter playerShooter;
    public GameObject droppedWeapon;
    public GameObject playerPrefab;
    public GameObject ak_prefab;
    public NetworkManager networkManager;


    //public  void AddWeapon(string name)
    //{
    //    UnityEngine.Object pPrefab = Resources.Load("Assets/prefaps/weapon/"+name);

    //    GameObject weapon = Instantiate(pPrefab, weaponHolder.transform.position,weaponHolder.transform.rotation) as GameObject;
    //    Rigidbody itemRb = weapon.GetComponent<Rigidbody>();
    //    itemRb.isKinematic = true;
    //    weapon.transform.parent = localPlayer.transform;
    //}

    internal void PickWeapon(GameObject temp)
    {
        switch (temp.GetComponent<Item>().itemName.ToLower())
        {
            case "ak_47" :

                GameObject weapon = Instantiate(ak_prefab, weaponHolder.transform.position, weaponHolder.transform.rotation) as GameObject;
                Debug.Log("before " + weapon.GetComponent<Item>().currentMag);
                Item newitem = weapon.GetComponent<Item>();
                Item olditem = temp.GetComponent<Item>();
                
                newitem.id = olditem.id;
                newitem.spareAmmo = olditem.spareAmmo;
                newitem.currentMag = olditem.currentMag;

                Debug.Log("after " + weapon.GetComponent<Item>().currentMag);


                newitem.action = olditem.action;
                weapon.transform.Find("Canvas").gameObject.SetActive(false);
            
            Rigidbody itemRb = weapon.GetComponent<Rigidbody>();
                weapon.GetComponent<BoxCollider>().enabled = false;
                weapon.GetComponent<CapsuleCollider>().enabled = false;

                itemRb.isKinematic = true;
                weapon.transform.parent = weaponHolder.transform;
               playerShooter =  localPlayer.GetComponent<PlayerShooter>();
                weapon.transform.Find("bullet point").transform.Find("muzzle fire").gameObject.SetActive(true);
                playerShooter.addWeapon(weapon);
                weapons[newitem.id]=weapon;
                networkManager.SendWeaponChanged(weapon);
                GameObject.Destroy(temp);
                break;
        }
    
    }

    internal void RemoveWeapon(GameObject temp)
    {
        playerShooter = localPlayer.GetComponent<PlayerShooter>();

        GameObject weapon = null;

        switch (temp.GetComponent<Item>().itemName.ToLower())
        {
            case "ak_47":
                weapon = Instantiate(ak_prefab, weaponHolder.transform.position, weaponHolder.transform.rotation) as GameObject;
                break;
        }
                
                Item newitem = weapon.GetComponent<Item>();
                Item olditem = temp.GetComponent<Item>();

                newitem.id = olditem.id;
                newitem.spareAmmo = olditem.spareAmmo;
                newitem.currentMag = olditem.currentMag;
                newitem.action = "drop";


                weapon.transform.SetParent(null);
                weapon.transform.Find("Canvas").gameObject.SetActive(false);
                weapon.transform.position = localPlayer.transform.position + Vector3.forward*3;
                weapon.GetComponent<BoxCollider>().enabled = true;
                weapon.GetComponent<CapsuleCollider>().enabled = true;
                weapon.GetComponent<Rigidbody>().isKinematic = false;
                weapon.transform.Find("bullet point").transform.Find("muzzle fire").gameObject.SetActive(true);

               
               
                playerShooter.dropWeapon(weapon);

                weapons[newitem.id] = weapon;

                networkManager.SendWeaponChanged(weapon);

                GameObject.Destroy(temp);
          

    }

    public bool isSessionAprroved()
    {
        return sessionAprroved;
    }

    // Use this for initialization
    void Awake()
    {
        sessionAprroved = false;
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
        sessionAprroved = false;

    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log(sessionAprroved + "  sessionapp");
        }
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
            playersObjects[sessionId].GetComponent<PlayerManager>().setId(sessionId);
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

    internal void changeWeapon(NetworkManager.WeaponJson weaponJson)
    {//this is important
        
     //  GameObject droppedWeapon = weapons[weaponJson.id];
        GameObject enemyPlayerObject = playersObjects[weaponJson.sessionId];
        EnemyPlayer enemyPlayer = enemyPlayerObject.GetComponent<EnemyPlayer>();
        GameObject Dweapon = weapons[weaponJson.id];
        GameObject weapon = null;
        Destroy(Dweapon);
        switch (weaponJson.name.ToLower())
        {
            case "ak_47":


                weapon = Instantiate(ak_prefab, enemyPlayer.weaponHolder.transform.position, enemyPlayer.weaponHolder.transform.rotation) as GameObject;
                break;

            default: return; 
        }



                Item item = weapon.GetComponent<Item>();
                item.name = weaponJson.name;
                item.id = weaponJson.id;
                item.currentMag = weaponJson.currentMag;
                item.spareAmmo = weaponJson.spareAmmo;
                item.action = weaponJson.action;
                if (item.action == "pick")
                {
                    weapon.transform.Find("Canvas").gameObject.SetActive(false);
                    Rigidbody itemRb = weapon.GetComponent<Rigidbody>();
                    weapon.GetComponent<BoxCollider>().enabled = false;
                    weapon.GetComponent<CapsuleCollider>().enabled = false;

                    itemRb.isKinematic = true;
                    weapon.transform.Find("bullet point").transform.Find("muzzle fire").gameObject.SetActive(true);
                    weapon.transform.SetParent(enemyPlayer.weaponHolder.transform);
                    enemyPlayer.firstWeapon = item;
           
                }
                else if(weaponJson.action=="drop")
                {
                    weapon.transform.position = enemyPlayerObject.transform.position + Vector3.forward * 3;
                    weapon.transform.Find("Canvas").gameObject.SetActive(true);
                    Rigidbody itemRb = weapon.GetComponent<Rigidbody>();
                    weapon.GetComponent<BoxCollider>().enabled = true;
                    weapon.GetComponent<CapsuleCollider>().enabled = true;

                    itemRb.isKinematic = false;
                    weapon.transform.Find("bullet point").transform.Find("muzzle fire").gameObject.SetActive(false);
                    weapon.transform.SetParent(null);
                    enemyPlayer.firstWeapon = null;

                }

                
                weapons[weaponJson.id] = weapon;
              
              
        
    }

    internal void setSessionApproved()
    {
        sessionAprroved = true;    }

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

   
}
