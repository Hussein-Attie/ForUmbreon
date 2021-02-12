using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour

{
    public Camera MainCamera;

    //Ajustable Variables
    public float firerate;
    public float damage = 7.6f;
    public int MagCapacity;
    int currentAmmo;
    public float reloadTime;
    public float range;
    public float impactforce;
    float nexttimetofire = 0f;
    float relativeDamage;


    public ParticleSystem muzzelflash;
    public GameObject impacteffect;
    public GameObject paintpoint;
    public Transform muzzel;

    Quaternion minRecoil;
    Quaternion maxRecoil;
    float recoil;
    public Transform recoilMod;
    public Transform WeaponGraphic;
    public Transform GunCenter;
    public Transform RayCenter;

    bool isRealoding;
   
   

    public TextMeshProUGUI OriginalDamage;
    public TextMeshProUGUI RelativeDamage;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI HitSide;
    public TextMeshProUGUI EnemyHealth;
    // Start is called before the first frame update
    private void OnEnable()
    {
        isRealoding = false;
    }
    void Start()
    {
        currentAmmo = MagCapacity;
    }

    // Update is called once per frame
    void Update()
    {
       OriginalDamage.text = "OriginalDamage: " + damage.ToString();
        RelativeDamage.text = "RelativeDamage: " + relativeDamage.ToString();
        Ammo.text = "Ammo: " + currentAmmo;
      
        //Rotates the gun toward the Hitpoint
        if (Physics.Raycast(RayCenter.position, RayCenter.forward * 100, out RaycastHit hit))
        { 
            Quaternion OriginalRot = GunCenter.rotation;
            GunCenter.LookAt(hit.point);
            Quaternion NewRot = GunCenter.rotation;
            GunCenter.rotation = OriginalRot;
            GunCenter.rotation = Quaternion.Lerp(GunCenter.rotation, NewRot, 2 * Time.deltaTime);
        }

        //commented function    Recoil();


        if (isRealoding)
        {
            return;
        }

        //Stop line debugging
        #region Debuging
        //Green for GunCenter
        Debug.DrawRay(GunCenter.position, GunCenter.forward * 100, Color.green);
        //Blue for the shooting raycast 
        Debug.DrawRay(RayCenter.transform.position, RayCenter.transform.forward * 100, Color.blue);

        #endregion

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1"))
        {


            if (Time.time >= nexttimetofire)
            {
                nexttimetofire = Time.time + 1f / firerate;
                Shoot();
                // uncomment the following if you want to use the gun recoil
               // recoil += 0.1f;
            }
        }

        // uncomment the following if you want to use the gun recoil
       // else if (!Input.GetButton("Fire1"))
        //{
      //      minRecoil = transform.rotation;
        //}


    }

    void Shoot()
    {
        //Shooting Effects
        muzzelflash.Play();
        //

        //Subbtracts Ammo
        currentAmmo--;
        #region variables
        RaycastHit hit;
        #endregion 

        if (Physics.Raycast(RayCenter.position, RayCenter.forward, out hit, range))
        {

            ShieldSystem shields = hit.transform.GetComponent<ShieldSystem>();

            //Relative Damage
            relativeDamage = (damage) * (Mathf.Sin((Vector3.Angle(hit.normal, GunCenter.transform.forward) * 0.5f) * Mathf.Deg2Rad));


            // Add bullet effects for every object it hits
            GameObject impactobject = Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
            GameObject bullethole = Instantiate(paintpoint, hit.point, Quaternion.LookRotation(hit.normal), hit.transform);

            //

            //Identifies Enemies 
            GameObject side = hit.collider.gameObject;
            Target target = hit.transform.GetComponent<Target>();
            Debug.Log(side.tag + "   " + relativeDamage);

            Health PlayersHealth;
           
            if (hit.rigidbody != null)
            {
                if (hit.rigidbody.GetComponent<Health>() != null)
                {
                    PlayersHealth = hit.rigidbody.GetComponent<Health>();
                  EnemyHealth.text = "Enemy Health :" + PlayersHealth.health.ToString();
                }
                else
                {
                    PlayersHealth = null;
                }
            }
            else
            {
                PlayersHealth = null;
            }

            HitSide.text = "Side :" + side.tag;

            //Destroys The effects
            Destroy(impactobject, 2f);
            Destroy(bullethole, 2f);
            #region Commented


            /////////////////////////////// For ricochet and penetration may work on it later delete if you dont want it 
            // 
            //to the top
            //        if (reflectionsRemaining == 0)
            //      {
            //        return;
            //  }


            // if (Physics.Raycast(muzzel.position, muzzel.forward, out hit, range))
            //{


            //  Gizmos.color = Color.magenta;
            // Gizmos.DrawLine(startingPosition, position);

            //  DrawPredictionReflectionPattern(position, direction, reflectionsRemaining - 1);

            //}

            #endregion

            //To destory Red Cubes when shot 
            //For Testing only
            if (target != null)
            {
                target.TakeDamage(50);
            }
           

            // Checks if the Relative dimentions of the bullet are greater than the dimentions of the shields and if so it applies damage 
            //1
            if (shields != null)
            {
                if (side.CompareTag("SideShields"))
                {
                    if (relativeDamage >= shields.SideArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //2
                if (side.CompareTag("LowerFront"))
                {
                    if (relativeDamage >= shields.LowerFrontArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }
                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //3
                if (side.CompareTag("UpperFront"))
                {
                    if (relativeDamage >= shields.UpperFrontArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //4
                if (side.CompareTag("LowerBack"))
                {
                    if (relativeDamage >= shields.LowerBackArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //5
                if (side.CompareTag("UpperBack"))
                {
                    if (relativeDamage >= shields.UpperBackArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //6
                if (side.CompareTag("CabinFront"))
                {
                    if (relativeDamage >= shields.CabinFrontArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }
                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //7
                if (side.CompareTag("CabinSide"))
                {
                    if (relativeDamage >= shields.CabinSideArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //8
                if (side.CompareTag("EngineCover"))
                {
                    if (relativeDamage >= shields.EngineCoverAarmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //9
                if (side.CompareTag("Turret"))
                {
                    if (relativeDamage >= shields.TurretAarmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }
                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //10
                if (side.CompareTag("TopRoof"))
                {
                    if (relativeDamage >= shields.TopRoofArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }

                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }
                //11
                if (side.CompareTag("Buttom"))
                {
                    if (relativeDamage >= shields.ButtomArmor)
                    {
                         
                        if (PlayersHealth != null)
                        {
                            PlayersHealth.TakeDamage(10);

                        }
                    }
                    else
                    {
                        Debug.Log("Hit but no damage");
                    }
                }


            }
        }


    }
    IEnumerator Reload()
    {
        isRealoding = true;

        // uncomment the following if you want to use the gun recoil
        //recoil = 0;

        // Dampen towards the target rotation
        recoilMod.rotation = Quaternion.Lerp(WeaponGraphic.rotation, minRecoil, Time.deltaTime * 6);
        WeaponGraphic.eulerAngles = new Vector3(recoilMod.eulerAngles.x, WeaponGraphic.eulerAngles.y, WeaponGraphic.eulerAngles.z);

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = MagCapacity;
        isRealoding = false;
    }


   // For recoil uncomment it if you want it or delete it if you dont want it

    /* private void OnDrawGizmos()
    //void Recoil ()
    //{
    //    maxRecoil = Quaternion.Euler(-10, 0, 0);
    //    Mathf.Clamp(recoilMod.rotation.x, maxRecoil.x, minRecoil.x);
    //    if (recoil > 0)
    //    {

    //  // Dampen towards the target rotation
    //     recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, maxRecoil, Time.deltaTime * 0.05f);
    //     WeaponGraphic.eulerAngles = new Vector3(recoilMod.transform.eulerAngles.x, WeaponGraphic.eulerAngles.y, WeaponGraphic.eulerAngles.z);
    //     recoil -= Time.deltaTime;

    //    }
    //  else
    //        {
    // recoil = 0;

    // Dampen towards the target rotation
    // recoilMod.rotation = Quaternion.Lerp(WeaponGraphic.rotation, minRecoil, Time.deltaTime * 6);
    //WeaponGraphic.eulerAngles = new Vector3(recoilMod.eulerAngles.x, WeaponGraphic.eulerAngles.y, WeaponGraphic.eulerAngles.z);
    //      }

    //////////////////////////////




    /////////////////////////////////////// For ricochet and penetration may work on it later delete if you dont want it  
    /* private void OnDrawGizmos()
     {

         DrawPredictionReflectionPattern(muzzel.position + muzzel.forward * 0.75f, muzzel.forward, maxReflectionCount);
     }

     void DrawPredictionReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
     {
         if (reflectionsRemaining == 0)
         {
             return;
         }

         Vector3 startingPosition = position;

         Ray ray = new Ray(position, direction);
         RaycastHit hit;
         if (Physics.Raycast(ray, out hit, maxStepDistance))
         {
             direction = Vector3.Reflect(direction, hit.normal);
             position = hit.point;

         }
         else
         {
             position += direction * maxStepDistance;
         }

         Gizmos.color = Color.yellow;
         Gizmos.DrawLine(startingPosition, position);

         DrawPredictionReflectionPattern(position, direction, reflectionsRemaining - 1);
     }*/
}




