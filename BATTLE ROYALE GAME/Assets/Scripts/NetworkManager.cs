using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public SocketIOComponent socket;
    public GameObject player;
    public SessionManager sessionManager;
    public int playerId;
    public GameObject plain;
    public GameObject parachute;
    public bool loggedIn = false;
    public static bool isId = false;
    public String status = "in plain";
    public EncryptionManager encryptionManager;
    public ZoneManager zoneManager;
    public  bool canJoin = false;

 

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
        canJoin = false;
        socket.On("rsa", OnRsa);
        socket.On("logged in successed", OnLogIn);
        socket.On("setId", SetSessionId);
        socket.On("approved", OnApproved);
        socket.On("other player connected", OnOtherPlayerConnected);
        socket.On("weapons", OnWeapons);
        socket.On("start game", OnStartGame);
        socket.On("player moved", OnOtherPlayerMoved);
        socket.On("player rotated", OnOtherPlayerRotated);
        socket.On("player animated", OnPlayerAnimated);
        socket.On("weapon changed", OnWeaponChanged);
        socket.On("move plain", MovePlain);
        socket.On("player air born", OnOtherPlayerAirBorn);
        socket.On("kill player", OnPlayerKilled);
        socket.On("hit player", OnPlayerHit);
        socket.On("player air born", OnPlayerAirBorn);
        socket.On("flash muzzle", OnMuzzleFlash);
        socket.On("jump", jump);
        socket.On("player landed", OnOtherPlayerLanded);
        socket.On("decrease zone", OnDecreaseZone);
        socket.On("disconnect", OnDisconnect);
        socket.On("try again",OnTryAgain);
        socket.On("can join", OnCanJoin);
        socket.On("you won",OnYouWon);
        CheckNulls();

    }

    public void OnYouWon(SocketIOEvent obj)
    {
        status = "dead";
        sessionManager.hudManager.btn.onClick.AddListener(DestroyScene);
        Debug.Log("you won");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        sessionManager.hudManager.panel.SetActive(true);
       // DestroyScene();
    }

    private void OnCanJoin(SocketIOEvent obj)
    {
        Debug.Log("can join");
        canJoin = true;

    }

    private void OnTryAgain(SocketIOEvent obj)
    {
        Debug.Log("try again");
        canJoin = false;

    }

    private void OnMuzzleFlash(SocketIOEvent obj)
    {
        string s = obj.data.ToString();
        WeaponJson wJson = JsonUtility.FromJson<WeaponJson>(s);
        sessionManager.flashMuzzle(wJson.id);
    }

    private void OnPlayerAirBorn(SocketIOEvent obj)
    {
        String s = obj.data.ToString();
        PositionJson pJson = JsonUtility.FromJson<PositionJson>(s);
        int id = pJson.sessionId;
        sessionManager.ParachutePlayer(id);
    }

    private void OnPlayerHit(SocketIOEvent obj)
    {
        Debug.Log("from player manager " + obj);
        String s = obj.data.ToString();
        HealthJson healthJson = JsonUtility.FromJson<HealthJson>(s);
        if(healthJson.id == playerId)
        {
            Debug.Log("velllllo");
            sessionManager.decreaseMyHealth(healthJson.newHealth);
        }
           else
                {
                    sessionManager.DecreasePlayerHealth(healthJson);
                }
    }

    internal void FlashMuzzle(Item currentWeapon)
    {
        WeaponJson wJson = new WeaponJson(currentWeapon.gameObject, playerId);
        String s = JsonUtility.ToJson(wJson);
        socket.Emit("flash muzzle", new JSONObject(s));
    }

    private void OnPlayerKilled(SocketIOEvent obj)
    {
        Debug.Log("killed");

        String s = obj.data.ToString();
        ShotJson shotJson = JsonUtility.FromJson<ShotJson>(s);
        if (shotJson.playerId == playerId)
        {
            status = "dead";
           sessionManager.KillMe();
          //  DestroyScene();
        }
        else
        {
            sessionManager.killPlayer(shotJson.playerId);
        }
    }

    public void DestroyScene()
    {

        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject go in gameObjects)
        {
            if (go.tag != this.tag || go.name != "SocketIO") Destroy(go);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        player = null;
      sessionManager=null;
      playerId = 0 ;
      plain = null;
        parachute = null ;
       isId = false;
      status = "in plain";
      encryptionManager = null;
        zoneManager = null;
        canJoin = false;
        SceneManager.LoadScene(0);
}

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            player.GetComponent<PlayerController>().animator.SetBool("air born", true);

            GameObject[] weaponSpawnPoints = GameObject.FindGameObjectsWithTag("weapon spawn point");

            WeaponsJson weaponsPoints = new WeaponsJson(weaponSpawnPoints);
            String s = JsonUtility.ToJson(weaponsPoints);
            Debug.Log("looooaded");
            //    socket.Emit("weapons points", new JSONObject(s));

            //player.GetComponent<PlayerController>().animator.enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = true;

            socket.Emit("in scene");
            sessionManager.sessionAprroved = false;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))

            if (Input.GetKeyDown(KeyCode.J))
            {



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
        if (encryptionManager == null)
            encryptionManager = GameObject.FindGameObjectWithTag("encryption manager").GetComponent<EncryptionManager>();

    }








    public void Connect()
    {
        StartCoroutine(ConnectToServer());

    }



    ///////////////////////////////////////////////////////////////////////////////

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
    public void HitPlayer(int health, int pid)
    {
        ShotJson shotJson = new ShotJson(health, pid, this.playerId);
        String s = JsonUtility.ToJson(shotJson);
        Debug.Log(s);
        socket.Emit("hit player", new JSONObject(s));
    }

    public void Play()
    {

        socket.Emit("play");
    }
    public void JoinSession()
    {
        CheckNulls();
        posrotJson posrotJson = new posrotJson(player.transform.position, player.transform.rotation, playerId);
        String posrot = JsonUtility.ToJson(posrotJson);
        socket.Emit("join session", new JSONObject(posrot));

    }

    internal void SendAnimation(AnimationJson animation)
    {

        string animationString = JsonUtility.ToJson(animation);
        if (sessionManager.isSessionAprroved())
            socket.Emit("player animated", new JSONObject(animationString));
    }

    internal void land()
    {
        //player.GetComponent<PlayerController>().animator.enabled = true;
        player.GetComponent<PlayerController>().animator.SetBool("air born", false);
        parachute.SetActive(false);
        PositionJson p = new PositionJson(player.transform.position,playerId);
        String s = JsonUtility.ToJson(p);
        socket.Emit("land", new JSONObject(s));
        parachute.SetActive(false);
        player.GetComponent<Rigidbody>().isKinematic = false;

    }
    internal void KickPlayers()
    {
        socket.Emit("kick players");
    }

    //private void LogIn()
    //{
    //    logInInfo info = new logInInfo("ali", "emad");
    //    String infoJson = JsonUtility.ToJson(info);
    //    //infoJson = AES.encrypting(infoJson);
    //    socket.Emit("log in", new JSONObject(infoJson));

    //}
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
    public void SendWeaponChanged(GameObject weapon)
    {
        WeaponJson weaponJson = new WeaponJson(weapon, this.playerId);
        String newWeapon = JsonUtility.ToJson(weaponJson);
        socket.Emit("weapon changed", new JSONObject(newWeapon));
    }
    public void SignUP(GameObject field)
    {
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
        if (userName.Length < 4 || password.Length < 4)
            return;
        UserJson userJson = new UserJson(userName, password);

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
        if (userName.Length < 1 || password.Length < 1)
            return;


        UserJson userJson = new UserJson(userName, password);

        String user = JsonUtility.ToJson(userJson);
        Debug.Log(user.Length + "   length");
        user = AES.encrypting(user);
        Debug.Log(user);
        Debug.Log(AES.decrypting(user));
        socket.Emit("log in", new JSONObject(JsonUtility.ToJson(new EncString(user))));
    }
    [Serializable]
    public class EncString
    {
        public String s;
        public EncString(String s)
        {
            this.s = s;
        }
    }
    public void Parachute()
    {
        if (plain == null) plain = GameObject.FindGameObjectWithTag("plain");
        if (parachute == null) parachute = player.transform.Find("parachute point").gameObject;
        parachute.SetActive(true);
        plain.transform.Find("plain cam").gameObject.SetActive(false);
        player.transform.Find("Main Camera").gameObject.SetActive(true);
        player.transform.position = plain.transform.position + Vector3.down * 20;
        player.GetComponent<Rigidbody>().isKinematic = false;
        sessionManager.sessionAprroved = true;
        PositionJson p = new PositionJson(player.transform.position, playerId);
        String s = JsonUtility.ToJson(p);
        socket.Emit("parachute", new JSONObject(s));
        status = "airborn";

    }
    #endregion commands



    //////////////////////////////////////////////////////////////////



    #region listening
    private void OnWeapons(SocketIOEvent obj)
    {

   
    }

    private void SetSessionId(SocketIOEvent obj)
    {

        string player = obj.data.ToString();
        //Debug.Log(player);
        //EncString s = JsonUtility.FromJson<EncString>(player);
        //player  = AES.decrypting(s.s);

        Debug.Log(player);
        PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(player);

        playerId = playerJson.sessionId;
        isId = true;
        playerJson.sessionId = playerId;
        String p = JsonUtility.ToJson(playerJson);

        socket.Emit("id is set", new JSONObject(p));
    }
    private void OnOtherPlayerAirBorn(SocketIOEvent obj)
    {
        String s = obj.data.ToString();
    }

    private void OnOtherPlayerLanded(SocketIOEvent obj)
    {
        String s = obj.data.ToString();
        PositionJson pJson = JsonUtility.FromJson<PositionJson>(s);
        int id = pJson.sessionId;
        sessionManager.LandPlayer(id);
    }

    private void MovePlain(SocketIOEvent obj)
    {
        Debug.Log("move plain");
        String weapons = obj.data.ToString();
        WeaponsJson2 weaponsJson = JsonUtility.FromJson<WeaponsJson2>(weapons);

        sessionManager.distribute(weaponsJson.weapons);
        if (plain == null)
            plain = GameObject.FindGameObjectWithTag("plain");
        Debug.Log(plain);
        plain.GetComponent<Rigidbody>().velocity = plain.transform.forward * 50f;
        plain.GetComponent<Rigidbody>().velocity = plain.transform.forward * 50f;

        parachute = player.transform.Find("parachute point").transform.gameObject;


    }

    private void OnDisconnect(SocketIOEvent obj)
    {
        Debug.Log("disconnected");
    }

    private void OnStartGame(SocketIOEvent obj)
    {

        SceneManager.LoadScene(2);
    }

    private void OnDecreaseZone(SocketIOEvent obj)
    {
        String s = obj.data.ToString();

        ZoneJson zoneJson = JsonUtility.FromJson<ZoneJson>(s);
        float size = zoneJson.size;
        if (zoneManager == null)
            zoneManager = GameObject.FindGameObjectWithTag("zone").GetComponent<ZoneManager>();
        zoneManager.DecreaseSize(size);
    }

    private void OnLogIn(SocketIOEvent obj)
    {
        Debug.Log(socket.pingInterval);
        if (loggedIn == true)
            return;
        Debug.Log("logged in successful");
        loggedIn = true;
        GameObject account = GameObject.FindGameObjectWithTag("account");
        account.GetComponent<Text>().text = "Connected";
        account.GetComponent<Text>().color = Color.green;

    }
    private void OnWeaponChanged(SocketIOEvent obj)
    {
        Debug.Log("on weapon changed");
        string w = obj.data.ToString();
        Debug.Log(w);
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
        Debug.Log(players + "sss");
        PlayersJson playersJson = JsonUtility.FromJson<PlayersJson>(players);
        Debug.Log("r");

        PlayerJson[] p = playersJson.players;
        Debug.Log("u");

        Debug.Log("0   " + p[0].sessionId + " " + p[0].health);
        Debug.Log("1   " + p[1].sessionId + " " + p[1].health);

        // Debug.Log("2   " + p[2].sessionId + " " + p[2].health);
        Debug.Log(p.Length + "    length");

        for (int i = 0; i < p.Length; i++)
        {
            Debug.Log(p.Length + "    length");


            PlayerJson player = p[i];
            Debug.Log("array " + player.sessionId);
            if (player.sessionId != playerId)
            {
                Debug.Log("dsfsagfhjdyhsgv" + player.sessionId);
                sessionManager.AddNewPlayer(player);
                Debug.Log("sadf" + player.sessionId);
            }

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

    private void OnRsa(SocketIOEvent obj)
    {
        Debug.Log("on rsa");


        if (encryptionManager == null) encryptionManager =
                GameObject.FindGameObjectWithTag("encryption manager").GetComponent<EncryptionManager>();
        string rsa = obj.data.ToString();
        Debug.Log(rsa);

        RsaJson rsaJson = JsonUtility.FromJson<RsaJson>(rsa);
        Debug.Log(rsaJson.d + "   " + rsaJson.N + "           " + rsaJson.cipher);
        encryptionManager.SetRsa(rsaJson);
    }

    private void jump(SocketIOEvent obj)
    {
        Debug.Log("jump");
        Parachute();
    }
    #endregion listening


    ////////////////////////////////////////////////////////////////////////////

    #region JsonClasses
        [Serializable]
        public class HealthJson
    {
        public int newHealth;
        public int id;
        public HealthJson(int newHealth,int id)
        {
            this.newHealth = newHealth;
            this.id = id;
        }
    }
    [Serializable]
    public class UserJson

    {
        public String userName;
        public String password;
        public UserJson(String userName, String password)
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

        public WeaponJson(GameObject weapon, int sessionID)
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
        public bool isAirBorn;

        public AnimationJson(int sessionId, float horizontal, float vertical, bool run, bool isHustler, bool isAiming, bool isCrouching,bool isAirBorn)
        {
            this.sessionId = sessionId;
            this.horizontal = horizontal;
            this.vertical = vertical;
            this.run = run;
            this.isHustler = isHustler;
            this.isAiming = isAiming;
            this.isCrouching = isCrouching;
            this.isAirBorn = isAirBorn;
        }
    }
    [Serializable]
    public class PlayersJson
    {
        public PlayerJson[] players;
        public PlayersJson(PlayerJson[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                this.players[i] = players[i];
            }
        }
    }
    [Serializable]
    public class PlayerJson
    {
        public int health = 0;
        public int sessionId;
        public float[] position;
        public float[] rotation;
        public String socketId;
        public String status;

        public PlayerJson(int health, int sessionId, Vector3 position, Quaternion rotation, String socketId, String status)
        {
            this.health = health;
            this.sessionId = sessionId;
            this.position = new float[] { position.x, position.y, position.z };
            this.rotation = new float[] { rotation.x, rotation.y, rotation.z };
            this.socketId = socketId;
            this.status = status;
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

        public posrotJson(Vector3 position, Quaternion rotation, int sessionId)
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

    }
    [Serializable]
    public class WeaponsJson
    {
        public PositionJson[] p;
        public WeaponsJson(GameObject[] temp)
        {
            p = new PositionJson[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                p[i] = new PositionJson(temp[i].transform.position, 0);
            }
        }

    }
    [Serializable]
    public class RsaJson
    {
        public string d;
        public string N;
        public string cipher;
        public RsaJson(string d, string N, string cipher)
        {
            this.d = d;
            this.N = N;
            this.cipher = cipher;
        }
    }
    [Serializable]
    public class WeaponsJson2
    {
        public WeaponJson2[] weapons;
        public WeaponsJson2(WeaponJson2[] weapons)
        {
            this.weapons = weapons;
        }
    }
    [Serializable]
    public class WeaponJson2
    {
        public string name;
        public int id;
        public WeaponJson2(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
    [Serializable]
    public class ZoneJson
    {
        public float size;
        public ZoneJson(float size)
        {
            this.size = size;
        }
    }
    [Serializable]
    public class ShotJson
    {
        public int health;
        public int playerId;
        public int shooterId;
        public ShotJson(int health, int playerId, int shooterId)
        {
            this.health = health;
            this.playerId = playerId;
            this.shooterId = shooterId;
        }

    }
    #endregion JsonClasses


}
