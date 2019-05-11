using System.Collections;
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
        if (rb.velocity.magnitude > 1)
        {
            isJumping = false;
        }
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

        if(Input.GetKey(KeyCode.F)&&networkManager.status=="in plain" &&SceneManager.GetActiveScene().buildIndex==2)
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (isJumping) return;
            animator.SetBool("jump", true);
            isJumping = true;
         //   rb.velocity = transform.position +(transform.up *0.5f );
            
            transform.position = new Vector3(transform.position.x, transform.position.y + ( 15* Time.deltaTime), transform.position.z);
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


            if (xof > 5 || yof < 5 || zof <5)
            {
                networkManager.sendPos(transform.position, networkManager.playerId);
                oldPosition = currentPosiotion;

            }

            #endregion networking
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (networkManager.status == "airborn" && collision.collider.gameObject.transform.root.tag != "eplayer")
        {
            Debug.Log(" asfsadf" +collision.collider.name);
            networkManager.status = "on ground";
            networkManager.land();
        }
    }
}
