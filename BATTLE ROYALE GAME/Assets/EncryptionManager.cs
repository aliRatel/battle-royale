using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EncryptionManager : MonoBehaviour
{
    public static EncryptionManager instance;
    public RSA rsa;
    public AES aes;
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
        
        
        rsa.d =  System.Numerics.BigInteger.Parse(rsaJson.d);
        rsa.N =  System.Numerics.BigInteger.Parse(rsaJson.N);
        Debug.Log(rsa.decrypt(rsaJson.cipher));
        aes = new AES(rsa.decrypt(rsaJson.cipher));
 


    }
}
