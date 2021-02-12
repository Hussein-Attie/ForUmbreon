using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    ////Just a test script for the Free Gun flaoting in the scene  

    public float firerate;
    public float damage= 9;
    float RealDamage;
    public float range;
    public float impactforce;

    public Transform muzzel;

    public ParticleSystem muzzelflash;
    public GameObject impacteffect;
    public GameObject paintpoint;
    int hitpoints;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Debug.DrawRay(muzzel.position, muzzel.forward * 100, Color.red);
        Shoot();
       
        
    }
    void Shoot()
    {
        muzzelflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(muzzel.position, muzzel.forward, out hit, range))
        {
            RealDamage = (damage) * (Mathf.Sin((Vector3.Angle(hit.normal, muzzel.forward)/2)* Mathf.Deg2Rad));

          

            Health PlayersHealth = hit.transform.GetComponent<Health>();
            ShieldSystem shields = hit.transform.GetComponent<ShieldSystem>();

            GameObject side = hit.collider.transform.gameObject;
//            Debug.Log(side.name);

            //1
            if (side.CompareTag("SideShields"))
            {
                if (damage >= shields.SideArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
                   // Debug.Log(  hitpoints + i); i++;
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //2
            if (side.CompareTag("LowerFront"))
            {
                if (damage >= shields.LowerFrontArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //3
            if (side.CompareTag("UpperFront"))
            {
                if (damage >= shields.UpperFrontArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //4
            if (side.CompareTag("LowerBack"))
            {
                if (damage >= shields.LowerBackArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //5
            if (side.CompareTag("UpperBack"))
            {
                if (damage >= shields.UpperBackArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //6
            if (side.CompareTag("CabinFront"))
            {
                if (damage >= shields.CabinFrontArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
                    Debug.Log(  hitpoints + i); i++;
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //7
            if (side.CompareTag("CabinSide"))
            {
                if (damage >= shields.CabinSideArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //8
            if (side.CompareTag("EngineCover"))
            {
                if (damage >= shields.EngineCoverAarmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //9
            if (side.CompareTag("Turret"))
            {
                if (damage >= shields.TurretAarmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //10
            if (side.CompareTag("TopRoof"))
            {
                if (damage >= shields.TopRoofArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }
            //11
            if (side.CompareTag("Buttom"))
            {
                if (damage >= shields.ButtomArmor)
                {
                    hitpoints = 10;
                    PlayersHealth.TakeDamage(10);
 
                }
                else
                {
                    Debug.Log("Hit but no damage");
                }
            }


           // GameObject impactobject = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
         //  Destroy(impactobject, 2f);
        }
    }
}
