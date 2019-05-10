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
        networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();

    }

    // Update is called once per frame
    void Update () {
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        float camx = Camera.main.transform.eulerAngles.x;
        if (camx >= 0 && camx<61)
        {
            
            anim.SetFloat("AimAngle", camx-10);

        }
        else if(camx>200)
        {   
            anim.SetFloat("AimAngle",camx-360-10 );

        }

        NetworkManager.AnimationJson animation = new NetworkManager.AnimationJson(networkManager.playerId,
       anim.GetFloat("Horizontal"),
       anim.GetFloat("Vertical"),
       anim.GetBool("run"),
       anim.GetBool("isHustler"),
       anim.GetBool("isAiming"),
       anim.GetBool("isCrouching"),
       anim.GetBool("air born")
           );
        networkManager.SendAnimation(animation);




    }

  
}
