using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour {
    public Quaternion oldRotation, newRotation;
    public float mouseSensitivityX, mouseSensitivityY;
    public Vector3 oldMousePosition;
    public Animator animator;
	// Use this for initialization
	void Start () {
        oldRotation = transform.rotation;
        oldMousePosition = Input.mousePosition;
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update () {
        if (Input.mousePosition != oldMousePosition)
        {
            Debug.Log("rotate");
            float xOffset =  Input.mousePosition.x;
            float yOffset = Input.mousePosition.y ;

            newRotation = Quaternion.Euler(0,xOffset,0);
            transform.rotation = newRotation;
            animator.SetBool("sidewalk", true);

        }
        else
        {
            animator.SetBool("sidewalk", false);

        }
        oldMousePosition = Input.mousePosition;
	}
}
