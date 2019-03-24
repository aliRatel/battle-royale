using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public SocketIOComponent socket;
    public GameObject player;
    public SessionManager sessionManager;
    public  int playerId;

    public bool loggedIn = false;

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

    void Start()
    {
        socket.On("join session approved", OnApproved);
        socket.On("other player connected", OnOtherPlayerConnected);
        socket.On("player moved", OnOtherPlayerMoved);
        socket.On("seId", SetSessionId);
        socket.On("player rotated", OnOtherPlayerRotated);
        socket.On("player animated", OnPlayerAnimated);

    }

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            LogIn();
        if (Input.GetKeyDown(KeyCode.J))
        {
            JoinSession();


        }




    }

  

    private void JoinSession()
    {
        posrotJson posrotJson = new posrotJson(player.transform.position, player.transform.rotation);
        String posrot = JsonUtility.ToJson(posrotJson);
        socket.Emit("join session", new JSONObject(posrot));

    }

    private void LogIn()
    {
        logInInfo info = new logInInfo("ali", "emad");
        String infoJson = JsonUtility.ToJson(info);

        socket.Emit("log in", new JSONObject(infoJson));

    }
    private void OnPlayerAnimated(SocketIOEvent obj)
    {
        string animation = obj.data.ToString();
        AnimationJson animationJson = JsonUtility.FromJson<AnimationJson>(animation);
        sessionManager.AnimatePlayer(animationJson);
    }

    private void SetSessionId(SocketIOEvent obj)
    {
        string player = obj.data.ToString();
        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(player);
        Debug.Log("data" + player + "    id is :   " + playerJson.sessionId);

        playerId = playerJson.sessionId;
    }

    private void OnOtherPlayerConnected(SocketIOEvent obj)
    {
        string player = obj.data.ToString();
        
        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(player);
        Debug.Log(playerJson.sessionId);
        sessionManager.AddNewPlayer(playerJson);
    }

    public void JoinGame()
    {
        StartCoroutine(ConnectToServer());

    }
    #region commands
    IEnumerator ConnectToServer()
    {
        PositionJson positoin = new PositionJson(transform.position);
        String pos = JsonUtility.ToJson(positoin);
        yield return new WaitForSeconds(0.1f);
        socket.Connect();
        socket.Emit("connection", new JSONObject(pos));

    }
    public void sendRot(Quaternion rotation, int sessionId)
    {
        RotationJson rotJ = new RotationJson(rotation, playerId);
        String newRot = JsonUtility.ToJson(rotJ);
        if (sessionManager.sessionAprroved)
            socket.Emit("player rotated", new JSONObject(newRot));
    }
    public void sendPos(Vector3 pos, int sessionId)
    {
        PositionJson posJ = new PositionJson(pos, playerId);
        String newPos = JsonUtility.ToJson(posJ);
        if (sessionManager.sessionAprroved)
            socket.Emit("player moved", new JSONObject(newPos));
    }
    #endregion commands
    #region listening

    private void OnApproved(SocketIOEvent obj)
    {
        sessionManager.setTrue();
        loggedIn = true;
        string players = obj.data.ToString();

        PlayerJson[] playersJson = JsonUtility.FromJson<PlayerJson[]>(players);

        foreach (PlayerJson player in playersJson)
        {
            sessionManager.AddNewPlayer(player);
        }
    }

    private void OnOtherPlayerRotated(SocketIOEvent obj)
    {
        Quaternion rot;
        int sessionId;
        String s = obj.data.ToString();
        RotationJson rotJ = JsonUtility.FromJson<RotationJson>(s);
        rot = new Quaternion(rotJ.rotation[0], rotJ.rotation[1], rotJ.rotation[2], 0);
        sessionId = rotJ.sessionId;
        sessionManager.RotatePlayer(rot, sessionId);
    }
    private void OnOtherPlayerMoved(SocketIOEvent obj)
    {
        Vector3 pos;
        int sessionId;
        String s = obj.data.ToString();
        PositionJson posJ = JsonUtility.FromJson<PositionJson>(s);
        pos = new Vector3(posJ.position[0], posJ.position[1], posJ.position[2]);
        sessionId = posJ.sessionId;
        sessionManager.movePlayer(pos, sessionId);
    }
    #endregion listening
    #region JsonClasses
    [Serializable]
    public class AnimationJson
    {
        public int sessionId;
        public float horizontal, vertical;
        public bool run, isHustler, isAiming, isCrouching;

        public AnimationJson(int sessionId,float horizontal, float vertical, bool run, bool isHustler, bool isAiming, bool isCrouching)
        {
            this.sessionId = sessionId;
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.run = run;
            this.isHustler = isHustler;
            this.isAiming = isAiming;
            this.isCrouching = isCrouching;
        }
    }
    [Serializable]
    public class PlayerJson
    {
        public int sessionId;
        public float[] position;
        public float[] rotation;
        public PlayerJson(int sessionId, Vector3 position, Quaternion rotation)
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
        public int sessionId;
        public PositionJson(Vector3 position, int sessionId)
        {
            this.position = new float[]
            { position.x, position.y, position.z };
            this.sessionId = sessionId;
        }
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
        public int sessionId;

        public RotationJson(Quaternion rotation)
        {
            this.rotation = new float[] { rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z };
        }
        public RotationJson(Quaternion rotation, int sessionId)
        {
            this.rotation = new float[] { rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z };
            this.sessionId = sessionId;

        }

    }
    [Serializable]
    public class posrotJson
    {
        public float[] position;
        public float[] rotation;

        public posrotJson(Vector3 position, Quaternion rotation)
        {
            this.position = new float[] { position.x, position.y, position.z };
            this.rotation = new float[] { rotation.x, rotation.y, rotation.z };
        }
    }
    [Serializable]
    public class logInInfo
    {
        public string userName;
        public string passWord;
        public logInInfo(string userName, string passWord)
        {
            this.userName = userName;
            this.passWord = passWord;
        }
        #endregion JsonClasses

    }
}
