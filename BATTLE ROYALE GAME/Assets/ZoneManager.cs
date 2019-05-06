using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 scale;
    public float radius;
    private ParticleSystem.ShapeModule shape;
    public ParticleSystem ps;
      
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("local palyer").gameObject;
        ps = GetComponent<ParticleSystem>();
        shape = ps.shape;
        radius = shape.radius;
        Debug.Log(radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        if(other.CompareTag("localPlayer"))
        {
            Debug.Log("zone collision");
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.CompareTag("localPlayer"))
        {
            Debug.Log("zone collision");
        }
    }


    public void DecreaseSize(float percentage)
    {

        transform.localScale = new Vector3(scale.x - (scale.x * percentage), scale.y, scale.z - (scale.z * percentage));


    }
}
