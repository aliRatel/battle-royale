﻿using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
    public static NetworkManager instance;
    public SocketIOComponent socket;
    public GameObject player;
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        JoinGame();

    }

    void Start () {
        socket.On("join session approved", OnApproved);
        socket.On("other player connected", OnOtherPlayerConnected);

    }
  

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
            LogIn();
        if (Input.GetKeyDown(KeyCode.J)) {
            JoinSession();
           

        }
       



    }

    private void OnApproved(SocketIOEvent obj)
    {
        string players = obj.data.ToString();
        
        PlayerJson[] playersJson= JsonUtility.FromJson<PlayerJson[]>(players);
        
    }

    private void JoinSession()
    {
        logInInfo info = new logInInfo("ali", "emad");
        String infoJson = JsonUtility.ToJson(info);
        socket.Emit("join session",new JSONObject(infoJson) );
    }

    private void LogIn()
    {
        logInInfo info = new logInInfo("ali", "emad");
        String infoJson = JsonUtility.ToJson(info);

        socket.Emit("log in", new JSONObject(infoJson));

    }

    private void OnOtherPlayerConnected(SocketIOEvent obj)
    {
        string player = obj.data.ToString();
        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(player);
        SessionManager.AddNewPlayer(playerJson) ;
    }

    public void JoinGame()
    {
        StartCoroutine(ConnectToServer());

    }
    #region commands
    IEnumerator ConnectToServer()
    { PositionJson  positoin= new PositionJson(transform.position);
        String pos = JsonUtility.ToJson(positoin);
    yield return new WaitForSeconds(0.1f);
        socket.Connect();
        socket.Emit("connection",new JSONObject(pos));
       
    }
    #endregion commands
    #region listening
    
    #endregion listening
    #region JsonClasses
    [Serializable]
    public class PlayerJson
    {
        public int sessionId;
        public float[] position;
        public float[] rotation;
        public PlayerJson(int sessionId,Vector3 position,Quaternion rotation)
        {
            this.sessionId = sessionId;
            this.position = new float[] { position.x, position.y, position.z };
            this.rotation = new float[] { rotation.x, rotation.y, rotation.z };
        }
    }
    [Serializable]
    public class PositionJson
    {
        public float[] position;
        public PositionJson(Vector3 position)
        {
            this.position = new float[]
            { position.x, position.y, position.z };
        }

    }
    [Serializable]
    public class RotationJson
    {
        public float[] rotation;
        public RotationJson(Quaternion rotation)
        {
            this.rotation = new float[] { rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z };
        }
         
    }
    [Serializable]
    public class logInInfo
    {
        public string userName;
        public string passWord;
        public logInInfo(string userName,string passWord){
            this.userName = userName;
            this.passWord = passWord;
    }
#endregion JsonClasses

}
}
