using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public SocketIOComponent socket;
    public GameObject player;
    public SessionManager sessionManager;
    public  int playerId;

    public bool loggedIn = false;
    public static bool isId = false;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
        Connect();

    }

    void Start()
    {
        
        socket.On("join session approved", OnApproved);
        socket.On("other player connected", OnOtherPlayerConnected);
        socket.On("player moved", OnOtherPlayerMoved);
        socket.On("setId", SetSessionId);
        socket.On("player rotated", OnOtherPlayerRotated);
        socket.On("player animated", OnPlayerAnimated);
        socket.On("weapon changed", OnWeaponChanged);
        socket.On("logged in successed", OnLogIn);
        CheckNulls();

    }

    private void OnLogIn(SocketIOEvent obj)
    {
        if (loggedIn == true)
            return;
        Debug.Log("logged in successful");
        loggedIn = true;
       GameObject account =  GameObject.FindGameObjectWithTag("account") ;
        account.GetComponent<Text>().text = "Connected";
        account.GetComponent<Text>().color = Color.green;

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))

        if (Input.GetKeyDown(KeyCode.J))
        {

            JoinSession();


        }




    }

    private void CheckNulls()
    {
        if (sessionManager == null)
        {
           
            sessionManager = GameObject.FindGameObjectWithTag("session manager").GetComponent<SessionManager>();
        }
        if (player == null)
        {
            Debug.Log("null");
            player = GameObject.FindGameObjectWithTag("localPlayer");
        }
        }
    public void Play()
    {
        socket.Emit("play");
    }
    public void JoinSession()
    {
        CheckNulls();
        posrotJson posrotJson = new posrotJson(player.transform.position, player.transform.rotation,playerId);
        String posrot = JsonUtility.ToJson(posrotJson);
        socket.Emit("join session", new JSONObject(posrot));

    }

    internal void SendAnimation(AnimationJson animation)
    {
        return;
        string animationString = JsonUtility.ToJson(animation);
        if(sessionManager.isSessionAprroved())
        socket.Emit("player animated", new JSONObject(animationString));
    }

    private void LogIn()
    {
        logInInfo info = new logInInfo("ali", "emad");
        String infoJson = JsonUtility.ToJson(info);

        socket.Emit("log in", new JSONObject(infoJson));

    }
    

  

    private void SetSessionId(SocketIOEvent obj)
    {
        Debug.Log("sadfalsdkjhgsa");
        string player = obj.data.ToString();
        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(player);
        Debug.Log("data" + player + "    id is :   " + playerJson.sessionId);

        playerId = playerJson.sessionId;
        isId = true;

        socket.Emit("id is set");
    }


    public void Connect()
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
        //socket.Emit("connection", new JSONObject(pos));
       // socket.Emit("connection");

    }
    public void sendRot(Quaternion rotation, int sessionId)
    {
        RotationJson rotJ = new RotationJson(rotation, playerId);
        String newRot = JsonUtility.ToJson(rotJ);
        if (sessionManager.isSessionAprroved())
            socket.Emit("player rotated", new JSONObject(newRot));
    }
    public void sendPos(Vector3 pos, int sessionId)
    {
        PositionJson posJ = new PositionJson(pos, playerId);
        String newPos = JsonUtility.ToJson(posJ);
        if (sessionManager.isSessionAprroved())
            socket.Emit("player moved", new JSONObject(newPos));
    }
    public void  SendWeaponChanged(GameObject weapon)
    {
        WeaponJson weaponJson = new WeaponJson(weapon, this.playerId);
        String newWeapon = JsonUtility.ToJson(weaponJson);
        socket.Emit("weapon changed", new JSONObject(newWeapon));
    }
    public void SignUP(GameObject field)
    {
        String userName=null, password=null;
       Text[] fields =  field.GetComponentsInChildren<Text>();
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].name == "user name field")
            {
                
                userName = fields[i].text;
            }
            else if (fields[i].name == "password field")
            {
                password = fields[i].text;
            }
        }
        if (userName.Length < 4 || password.Length < 4)
            return;
        UserJson userJson = new UserJson(userName,password);

        String user = JsonUtility.ToJson(userJson);
        socket.Emit("sign up", new JSONObject(user));
    }
    public void LogIn(GameObject field)
    {
        Debug.Log("log");
        String userName = null, password = null;
        Text[] fields = field.GetComponentsInChildren<Text>();
        for (int i = 0; i < fields.Length; i++)
        {
            if (fields[i].name == "user name field")
            {

                userName = fields[i].text;
            }
            else if (fields[i].name == "password field")
            {
                password = fields[i].text;
            }
        }
        if (userName.Length <1 || password.Length < 1)
            return;


        UserJson userJson = new UserJson(userName,password);

        String user = JsonUtility.ToJson(userJson);
        socket.Emit("log in", new JSONObject(user));
    }
    #endregion commands
    #region listening
    private void OnWeaponChanged(SocketIOEvent obj)
    {
        string w = obj.data.ToString();
        WeaponJson weaponJson = JsonUtility.FromJson<WeaponJson>(w);
        sessionManager.changeWeapon(weaponJson);
    }

    private void OnPlayerAnimated(SocketIOEvent obj)
    {
        string animation = obj.data.ToString();
        AnimationJson animationJson = JsonUtility.FromJson<AnimationJson>(animation);
        sessionManager.AnimatePlayer(animationJson);
    }
    private void OnOtherPlayerConnected(SocketIOEvent obj)
    {
        string player = obj.data.ToString();

        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(player);
        Debug.Log(playerJson.sessionId);
        sessionManager.AddNewPlayer(playerJson);
    }

    private void OnApproved(SocketIOEvent obj)
    {
        Debug.Log("on Approved");
        sessionManager.setSessionApproved(); 
        string players = obj.data.ToString();

        PlayerJson[] playersJson = JsonUtility.FromJson<PlayerJson[]>(players);

        foreach (PlayerJson player in playersJson)
        {
            if(player.sessionId!=playerId)
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
    public class UserJson

    {
        public String userName;
        public String password;
        public UserJson(String userName,String password)
        {
            this.userName = userName;
            this.password = password;
        }

    }

        [Serializable]
    public class WeaponJson
    {
        public string name;
        public int id;
        public int currentMag;
        public int spareAmmo;
        public int sessionId;
        public float[] position;
        public string action;

        public WeaponJson(GameObject weapon,int sessionID)
        {
            Item item = weapon.GetComponent<Item>();
            this.name = item.itemName;
            this.id = item.id;
            this.currentMag = item.currentMag;
            this.spareAmmo = item.spareAmmo;
            this.sessionId = sessionID;
            this.action = item.action;
            this.position = new float[] { weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z };

        }
    }
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
        public int sessionId;

        public posrotJson(Vector3 position, Quaternion rotation,int sessionId)
        {
            this.position = new float[] { position.x, position.y, position.z };
            this.rotation = new float[] { rotation.x, rotation.y, rotation.z };
            this.sessionId = sessionId;
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
