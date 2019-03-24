using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;

    public Player[] players;
    public  GameObject[] playersObjects;
    public int playerId;
    public  bool sessionAprroved = false;
    public Animator anim;

    public GameObject playerPrefab;
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
