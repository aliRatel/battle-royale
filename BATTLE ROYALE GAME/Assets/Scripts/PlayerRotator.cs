using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    public Quaternion camOldRotation, camNewRotation;
    public float mouseSensitivityX, mouseSensitivityY;
    public Vector3 oldMousePosition;
    public Animator animator;
    float mouseX, mouseY;
    public Transform target;
    public GameObject camera;
    public float rotationSpeed = 1;
    public NetworkManager networkManager;
    public Quaternion oldRotation, currentRotation;


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        camOldRotation = camera.transform.rotation;
        target = GameObject.FindGameObjectWithTag("camP").transform;
        networkManager = GameObject.FindGameObjectWithTag("network manager").GetComponent<NetworkManager>();



    }
    private void Update()
    {
        currentRotation = transform.rotation;


        #region networking
        if (currentRotation != oldRotation)
        {
            //todo position networking
            networkManager.sendRot(transform.rotation, networkManager.playerId);
            oldRotation = currentRotation;


        }

        #endregion networking
    }
    // Update is called once per frame
    void LateUpdate()
    {


        RotateCam();


        //if (Input.mousePosition != oldMousePosition)
        //{
        //    Debug.Log("rotate");
        //    float xOffset =  Input.mousePosition.x;
        //    float yOffset = Input.mousePosition.y ;

        //    newRotation = Quaternion.Euler(0,xOffset,0);           
        //    transform.rotation = newRotation;

        //   Vector3  camNewRotation = new Vector3(1, 0, 0);

        //}

        //oldMousePosition = Input.mousePosition;
    }



    private void RotateCam()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, target.transform.rotation, Time.deltaTime * 10000);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
        }
        else
        {
            target.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, mouseX, 0), Time.deltaTime * 500);

        }

    }


}
