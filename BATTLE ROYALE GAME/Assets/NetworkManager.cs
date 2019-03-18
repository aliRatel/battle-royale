using SocketIO;
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
                }
	void Start () {
        socket.On("other player connected", OnOtherPlayerConnected);

    }

    private void OnOtherPlayerConnected(SocketIOEvent socketIOEvent)
    {
       
    }

    public void JoinGame()
    {
        StartCoroutine(ConnectToServer());

    }
    #region commands
    IEnumerator ConnectToServer()
    { PositionJson  positoin= new PositionJson(transform.position);
        String pos = JsonUtility.ToJson(positoin);
    yield return new WaitForSeconds(0.5f);
        socket.Emit("player connection request", new JSONObject(pos));
    }
    #endregion commands
    #region listening

    #endregion listening
    #region JsonClasses
    [Serializable]
    public class PlayerJson
    {

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
#endregion JsonClasses

}
