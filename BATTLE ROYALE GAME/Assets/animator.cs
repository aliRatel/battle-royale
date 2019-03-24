using System.Collections;
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



        NetworkManager.AnimationJson animation = new NetworkManager.AnimationJson(networkManager.playerId,
       anim.GetFloat("Horizontal"),
       anim.GetFloat("Vertical"),
       anim.GetBool("run"),
       anim.GetBool("isHustler"),
       anim.GetBool("isAiming"),
       anim.GetBool("isCrouching")
           );
        networkManager.SendAnimation(animation);




    }

  
}
