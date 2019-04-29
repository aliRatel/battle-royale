using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{
    public Item firstWeapon;
    public Item SecondWeapon;
    public Item currentWeapon;
    public GameObject weaponHolder;
    public int health;
    public int armor;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
