using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 oldPosition, currentPosiotion;
    public GameObject target;
    public float speed = 10;
    public SkinnedMeshRenderer myrenderer;
    public bool onGround;
    public LayerMask whatIsGround;
    public Animator animator;
    public bool h = false;
    public GameObject socketIO;
    public bool localPlayer;
    public NetworkManager networkManager;


    // Use this for initialization
    void Start()
    {

        animator = GetComponentInChildren<Animator>();
        localPlayer = true;

    }

    // Update is called once per frame
    void Update()
    {
        currentPosiotion = transform.position;
        if (!localPlayer)
        {
            return;
        }
        # region animation
        animator.SetBool("isHustler", h);


        if (Input.GetKey(KeyCode.Z))
        {
            animator.SetBool("isCrouching", true);

        }
        else
        {
            animator.SetBool("isCrouching", false);

        }
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            animator.SetBool("isAiming", true);
        }
        else
        {
            animator.SetBool("isAiming", false);
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
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
        #endregion
        #region networking


        if (currentPosiotion != oldPosition)
        {
            //todo position networking
            float xof, yof, zof;
            xof = Mathf.Abs(oldPosition.x - currentPosiotion.x);
            yof = Mathf.Abs(oldPosition.y - currentPosiotion.y);
            zof = Mathf.Abs(oldPosition.z - currentPosiotion.z);


            if (xof>3 || yof > 3 || zof > 3) { 
            networkManager.sendPos(transform.position, networkManager.playerId);
                oldPosition = currentPosiotion;

            }

            #endregion networking
        }
}
