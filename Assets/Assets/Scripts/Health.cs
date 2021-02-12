using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Add the following to the player script

public class Health : MonoBehaviour
{
    //Simply Reduces health when damage is taken


    public int health;
    int maxhealth=1000;
    
    // Start is called before the first frame update
    void Start()
    {
      
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(health<=0)
        {
            health = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

    }
}
