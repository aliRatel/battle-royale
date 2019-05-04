using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using UnityEngine;

public class RSA : MonoBehaviour
{
    public BigInteger p;
    public BigInteger q;
    public BigInteger N;
    public BigInteger phi;
    public BigInteger e;
    public BigInteger d;
    int bitlength = 1024;


    public RSA()
    {

    }

    public RSA(BigInteger e1, BigInteger d1, BigInteger N1)
    {
        e = e1;
        d = d1;
        N = N1;
    }




    private string BytesToString(byte[] encrypted)
    {
        string test = "";

        test += Encoding.ASCII.GetString(encrypted);

        return test;
    }
    //public string encrypt(string message)
    //{

    //    return (new BigInteger(message)).modPow(e, N).toByteArray();
    //}

    public string decrypt(string message)
    {
        
        byte[] bytes = Encoding.UTF8.GetBytes(message);
        Debug.Log("message   "+ message);
        
        BigInteger enc = BigInteger.Parse(message);
        Debug.Log("enc    " + enc);
        BigInteger temp = BigInteger.ModPow(enc, d, N);
        Debug.Log("temp  "+temp);



        return decode(temp.ToString()) ;

    }

    private byte[] getBytes(string v)
    {
        return System.Text.Encoding.UTF8.GetBytes(v);
    }
    private string decode(string s)
    {
        string stringified = s;
        string s1 = "";

        for (int i = 0; i < stringified.Length; i += 2)
        {
            byte num = byte.Parse(stringified.Substring(i, 2));

            if (num <= 30)
            {
                s1 += (char)(byte.Parse(stringified.Substring(i, 3)));
                i++;
            }
            else
            {
                s1 += (char)(num);
            }
        }

        return s1;
    }
}
