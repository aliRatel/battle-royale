using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

public class RSA
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

        test += System.Text.Encoding.UTF8.GetString(encrypted);

        return test;
    }
    //public string encrypt(string message)
    //{

    //    return (new BigInteger(message)).modPow(e, N).toByteArray();
    //}

    public string decrypt(string message)
    {

        byte[] bytes = Encoding.UTF8.GetBytes(message);
        BigInteger enc = new BigInteger(bytes);
        BigInteger temp = BigInteger.ModPow(enc, d, N);
        bytes = temp.ToByteArray();
        return BytesToString(bytes);

    }




}
