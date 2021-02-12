using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //This script is for testing purposes 
    //I gave this script for the red cubes 

    public float heath=100;
  
   
    public void TakeDamage(float ammount)
    {
        heath -= ammount;
      
        if(heath==0)
        {
            Destroy(gameObject);

        }
    }
}
