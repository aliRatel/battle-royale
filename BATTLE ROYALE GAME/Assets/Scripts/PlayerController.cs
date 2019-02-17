using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject target;
    public float speed = 10;
    public SkinnedMeshRenderer myrenderer;
    public bool onGround;
    public LayerMask whatIsGround;
    public Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject groundCheck = GameObject.FindGameObjectWithTag("GroundCheck");
       onGround= Physics2D.OverlapCircle(groundCheck.transform.position,10f,whatIsGround);
#region movement
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 15f;
                animator.SetBool("running", true);
                animator.SetBool("walking", false);
            }
            else{
                speed = 10f;
                animator.SetBool("running", false);
                animator.SetBool("walking", true);
            }

            transform.position += transform.forward * speed * Time.deltaTime;

        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("running", false);


        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed * Time.deltaTime;
           // animator.SetBool("walkRight", true);
        }
        if (Input.GetKey(KeyCode.Space) )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
#endregion 
    }
}
