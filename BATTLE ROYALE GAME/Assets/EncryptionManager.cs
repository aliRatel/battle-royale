using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EncryptionManager : MonoBehaviour
{
    public static EncryptionManager instance;
    public RSA rsa;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRsa(NetworkManager.RsaJson rsaJson)
    {
        Debug.Log("setRsa");
        rsa = new RSA();
        
        byte[] d = Encoding.UTF8.GetBytes(rsaJson.d);
        byte[] n = Encoding.UTF8.GetBytes(rsaJson.N);
        
        rsa.d = new System.Numerics.BigInteger(d);
        rsa.N = new System.Numerics.BigInteger(n);
        Debug.Log(rsa.d);
        Debug.Log(rsa.N);

    }
}
