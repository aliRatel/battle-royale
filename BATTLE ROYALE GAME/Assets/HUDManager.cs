using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour {
    public GameObject w1Ammo;
    public GameObject w2Ammo;
    public GameObject w1Img;
    public GameObject w2Img;
    public GameObject w1Bg;
    public GameObject w2IBg;
    public Image healthBar;
    public Text killed;
    public Text alive;
    Color yellow = Color.yellow;
    Color white = Color.white;
    public Button btn;
    public Text  state;
    public GameObject panel;

    // Use this for initialization
    void Start () {
        white.a = 100f;
        yellow.a = 255f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetAmmo1(Item item)
    {
        Text t = w1Ammo.GetComponent<Text>();
        t.text = item.currentMag + "/" + item.spareAmmo;
    }
    public void SetAmmo2(Item item)
    {
        Text t = w2Ammo.GetComponent<Text>();
        t.text = item.currentMag + "/" + item.spareAmmo;
    }
    public void SetImg1(Item item)
    {
        String path="";
        switch (item.itemName.ToLower())
        {
            case "ak_47":
                path = "images/ak";
                break;
            case "m4":
                path = "images/m4";
                break;
        }
        w1Img.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);

    }
    public void SetImg2(Item item)
    {
        String path = "";
        switch (item.itemName.ToLower())
        {
            case "ak_47":
                path = "images/ak";
                break;
            case "m4":
                path = "images/m4";
                break;
        }
        w2Img.GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
    }

    public void SetBg1()
    {
        Image img1 = w1Bg.GetComponent<Image>();
        Image img2 = w2IBg.GetComponent<Image>();
      
       
        img1.color = yellow;
        img2.color = white;
    }
    public void SetBg2()
    {
        Image img1 = w1Bg.GetComponent<Image>();
        Image img2 = w2IBg.GetComponent<Image>();


        img1.color = white;
        img2.color = yellow;
    }
    public void decreasehealth(int health)
    {
        healthBar.fillAmount = (health / 100f);
        Debug.Log(health / 100f);
    }

    public void killSomeOne()
    {


        int a = int.Parse( killed.text.ToString());
        int b = int.Parse(alive.text.ToString());
        a++;
        b--;
        killed.text = a.ToString();
        alive.text = b.ToString();
    }
}
