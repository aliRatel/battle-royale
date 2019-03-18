﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SessionManager : MonoBehaviour {
    public static Player[] players;
    public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
        players = new Player[100];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal static void AddNewPlayer(NetworkManager.PlayerJson playerJson)
    {
        Vector3 position = new Vector3(playerJson.position[0], playerJson.position[1], playerJson.position[2]);
        Quaternion rotation = new Quaternion(playerJson.rotation[0], playerJson.rotation[1], playerJson.rotation[2],0.0f);
        int sessionId = playerJson.sessionId;

        Player player = new Player(position, rotation, sessionId);
        players[sessionId] = player;
        GameObject.Instantiate(Resources.Load("player"), position, rotation);

    }
}