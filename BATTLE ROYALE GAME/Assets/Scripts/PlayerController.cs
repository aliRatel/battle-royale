﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector3 oldPosition, currentPosiotion;
    public GameObject target;
    public float speed = 40;
    public SkinnedMeshRenderer myrenderer;
    public bool onGround;
    public LayerMask whatIsGround;
    public Animator animator;
    public bool h = false;
    public GameObject socketIO;
    public bool localPlayer;
    public NetworkManager networkManager;
    public GameObject plain;
    public Rigidbody rb;
    public bool isJumping;
    public bool canSend;
    public float networkSendRate = 5;
    public float timeBetweenMovementStart;
    public float timeBetwennMovementEnd;


    // Use this for initialization
    void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        localPlayer = true;
        networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("jump", false);

        if (networkManager.status == "dead") return;

        currentPosiotion = transform.position;
        if (!localPlayer)
        {
            return;
        }
        #region animation


        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);

        }
        else
        {
            animator.SetBool("isCrouching", false);

        }
        if ( Input.GetMouseButton(0))
        {
            animator.SetBool("isAiming", true);
        }
        else
        {
            animator.SetBool("isAiming", false);
        }
        
        if (Input.GetMouseButton(1) && animator.GetBool("isHustler"))
        {
            animator.SetBool("isAiming", true);
            Camera.main.fieldOfView = 30;

        }
        else
        {
            animator.SetBool("isAiming", false);
            Camera.main.fieldOfView = 60;

        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15f;
            animator.SetBool("run", true);

        }
        else
        {
            animator.SetBool("run", false);

            speed = 10f;

        }
        #endregion animation
        #region movement

        if (Input.GetKey(KeyCode.F) && networkManager.status == "in plain" && SceneManager.GetActiveScene().buildIndex == 2)
        {

            networkManager.Parachute();

        }
        if (Input.GetKey(KeyCode.W))
        {


            transform.position += transform.forward * speed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jump", true);

        }
        #endregion
        #region networking
        if(!canSend){
            canSend = true;
            StartCoroutine(StartNetworkSendCooldown());
        }

       
        //if (currentPosiotion != oldPosition)
        //{
        //    //todo position networking
        //    float xof, yof, zof;
        //    xof = Mathf.Abs(oldPosition.x - currentPosiotion.x);
        //    yof = Mathf.Abs(oldPosition.y - currentPosiotion.y);
        //    zof = Mathf.Abs(oldPosition.z - currentPosiotion.z);


        //    if (xof > 5 || yof < 5 || zof < 5)
        //    {
        //        networkManager.sendPos(transform.position, networkManager.playerId);
        //        oldPosition = currentPosiotion;

        //    }

          
        //}
      #endregion networking
    }
    private IEnumerator StartNetworkSendCooldown()
    {
        timeBetweenMovementStart = Time.time;
        yield return new WaitForSeconds((1 / networkSendRate));
        SendNetworkMovement();
    }
    private void SendNetworkMovement()
    {
        timeBetwennMovementEnd = Time.time;
        networkManager.sendPos(transform.position, networkManager.playerId,(timeBetwennMovementEnd - timeBetweenMovementStart));
        canSend = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (networkManager.status == "airborn" && collision.collider.gameObject.transform.root.tag != "eplayer")
        {
            Debug.Log(" asfsadf" + collision.collider.name);
            networkManager.status = "on ground";
            networkManager.land();
        }
    }
}
