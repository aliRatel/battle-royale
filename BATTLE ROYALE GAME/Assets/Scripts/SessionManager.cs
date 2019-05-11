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
    public GameObject[] weaponsPoints;
    public  GameObject[] playersObjects;
    public int playerId;
    public  Boolean sessionAprroved =false;
    public Animator anim;
    public GameObject localPlayer;
    public GameObject weaponHolder;
    public PlayerShooter playerShooter;
    public GameObject droppedWeapon;
    public GameObject playerPrefab;
    public GameObject ak_prefab,m4_prefab;
    public HUDManager hudManager;
    public NetworkManager networkManager;

    
    internal void setCurrentWeapon(GameObject gameObject, int v)
    {

    }


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
        if (localPlayer.GetComponent<PlayerShooter>().firstWeapon != null &&
            localPlayer.GetComponent<PlayerShooter>().SecondWeapon != null) return;
        GameObject weapon= null;
        Debug.Log(temp.GetComponent<Item>().itemName.ToLower());
        switch (temp.GetComponent<Item>().itemName.ToLower())
        { 
            case "ak_47" :

                 weapon = Instantiate(ak_prefab, weaponHolder.transform.position, weaponHolder.transform.rotation) as GameObject;
                break;
            case "m4":
                 weapon = Instantiate(m4_prefab, weaponHolder.transform.position, weaponHolder.transform.rotation) as GameObject;
                break;
                
        }
        Item newitem = weapon.GetComponent<Item>();
                Item olditem = temp.GetComponent<Item>();
        Destroy(temp);

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
                playerShooter.AddWeapon(weapon);
                weapons[newitem.id]=weapon;

        networkManager.SendWeaponChanged(weapon);
                
        
    
    }

    internal void flashMuzzle(int id)
    {
        if(!weapons[id].GetComponent<Item>().sound.isPlaying)
        weapons[id].GetComponent<Item>().sound.Play();
        weapons[id].GetComponent<Item>().muzzleFire.GetComponent<ParticleSystem>().Play();

    }

    internal void ParachutePlayer(int id)

    {

        GameObject parachute = playersObjects[id].transform.Find("parachute point").gameObject;
        parachute.SetActive(true);
    }
    internal void LandPlayer(int id)

    {
        GameObject parachute = playersObjects[id].transform.Find("parachute point").gameObject;
        parachute.SetActive(false);
    }


    internal void KillMe()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        hudManager.panel.SetActive(true);
        hudManager.state.text = "you died";
        playerShooter = localPlayer.GetComponent<PlayerShooter>();
        if (playerShooter.firstWeapon != null) RemoveWeapon(playerShooter.firstWeapon.gameObject);
        if (playerShooter.SecondWeapon != null) RemoveWeapon(playerShooter.SecondWeapon.gameObject);
        localPlayer.GetComponentInChildren<Animator>().SetBool("died", true);


    }

    internal void killPlayer(int playerId)
    {
        GameObject killedPlayer = playersObjects[playerId];
        killedPlayer.GetComponentInChildren<Animator>().SetBool("died", true);
       

        Collider[] c = killedPlayer.GetComponentsInChildren<Collider>();
            

        foreach(Collider collider in c)
        {
            collider.enabled = false;
        }
        hudManager = GameObject.FindGameObjectWithTag("hud").GetComponent<HUDManager>();
        hudManager.killSomeOne();
        Debug.Log("ad");

    }

    internal void decreaseMyHealth(int health)
    {
        Debug.Log("from session manager " + health);
        if (hudManager == null)
            hudManager = GameObject.FindGameObjectWithTag("hud").GetComponent<HUDManager>();

        hudManager.decreasehealth(health);
        Debug.Log("ali");
        localPlayer.GetComponent<PlayerManager>().health = health;
        Debug.Log("after ali ");
        hudManager.decreasehealth(health);
    }

    internal void DecreasePlayerHealth(NetworkManager.HealthJson healthJson)
    {
        playersObjects[healthJson.id].GetComponent<EnemyPlayer>().health = healthJson.newHealth;
    }

    internal void distribute(NetworkManager.WeaponJson2[] weapons)
    {

        weaponsPoints= GameObject.FindGameObjectsWithTag("weapon spawn point");

        for (int i = 0 ; i < weapons.Length;i++ )
        {

            GameObject w = null;
            NetworkManager.WeaponJson2 weapon = weapons[i];
            string name = weapon.name;
            int id = weapon.id;
            switch (name.ToLower())
            {
                case "ak-47":
                   w  = Instantiate(ak_prefab, weaponsPoints[i].transform.position,weaponsPoints[i].transform.rotation) as GameObject;

                    break;
                case "m4a1":
                    w = Instantiate(m4_prefab, weaponsPoints[i].transform.position, weaponsPoints[i].transform.rotation) as GameObject;


                    break;

            }

            w.GetComponent<Item>().id = i;
            this.weapons[id] = w;



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
            case "m4":
                weapon = Instantiate(m4_prefab, weaponHolder.transform.position, weaponHolder.transform.rotation) as GameObject;
                break;
        }
                
                Item newitem = weapon.GetComponent<Item>();
                Item olditem = temp.GetComponent<Item>();
        Destroy(temp);

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

               
               
                //playerShooter.dropWeapon(weapon);

                weapons[newitem.id] = weapon;

        networkManager.SendWeaponChanged(weapon);

          

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
        weapons = new GameObject[500];
        localPlayer = GameObject.FindGameObjectWithTag("localPlayer");
        weaponsPoints = new GameObject[500];
        players = new Player[100];
        playersObjects = new GameObject[100];
        weaponHolder = GameObject.FindGameObjectWithTag("weapon holder");

    }
  
    // Update is called once per frame
    void Update()
    {
        CheckNulls();
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log(sessionAprroved + "  sessionapp");
        }
    }

    private void CheckNulls()
    {
        if (networkManager == null)
            networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();
    }

    public void  AddNewPlayer(NetworkManager.PlayerJson playerJson )
    {
        Vector3 position = new Vector3(playerJson.position[0], playerJson.position[1], playerJson.position[2]);

        Quaternion rotation = new Quaternion(playerJson.rotation[0], playerJson.rotation[1], playerJson.rotation[2], 0.0f);

        int sessionId = playerJson.sessionId;

        if (players[sessionId] == null && playersObjects[sessionId] == null)
        {


            Player player = new Player(position, rotation, sessionId);

            players[sessionId] = player;

            GameObject pl = Instantiate(playerPrefab, position, rotation) as GameObject;
            pl.GetComponent<EnemyPlayer>().id = sessionId;
            playersObjects[sessionId] = pl;

            //playersObjects[sessionId].GetComponent<PlayerManager>().setId(sessionId);
            //Debug.Log("m5");

        }


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
            case "m4":
                weapon = Instantiate(m4_prefab, enemyPlayer.weaponHolder.transform.position, enemyPlayer.weaponHolder.transform.rotation) as GameObject;
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
        anim.SetBool("air born", animationJson.isAirBorn);
    }

   
}
