﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class animator : MonoBehaviour {
   public   Animator anim;
    public NetworkManager networkManager;
    public Animation a, b;
    public AnimatorControllerParameter[] currentParams,oldParams;
    
    public int asd;
    

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        currentParams = anim.parameters;
    }

    // Update is called once per frame
    void Update () {
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));


        //currentParams = anim.parameters;



        //for (int i = 0; i < oldParams.Length; i++)
        //{
        //    AnimatorControllerParameter p1, p2;
        //    p1 = oldParams[i];
        //    p2 = currentParams[i];
        //    Debug.Log("P1  "+i+"  " + p1.defaultFloat);
        //    Debug.Log("P2  "+i + "  " + p2.defaultFloat);
        //}


        StartCoroutine(SendAnimation());




    }

    IEnumerator SendAnimation()
    {
        NetworkManager.AnimationJson animation;
        animation = new NetworkManager.AnimationJson(networkManager.playerId,
        anim.GetFloat("Horizontal"),
        anim.GetFloat("Vertical"),
        anim.GetBool("run"),
        anim.GetBool("isHustler"),
        anim.GetBool("isAiming"),
        anim.GetBool("isCrouching")
            );
        string animationString = JsonUtility.ToJson(animation);
        yield  return new WaitForSeconds(1f);
        networkManager.socket.Emit("animate player", new JSONObject(animationString));
    }
}
