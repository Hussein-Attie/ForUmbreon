using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    /*
    public Transform recoilMod;
    float recoil;
    Quaternion minRecoil;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vehiclerecoiling();
        if(Input.GetKey(KeyCode.R))
        {
            recoil+=0.1f;
        }

    }

    void Vehiclerecoiling()

    {
    
    }
    */
    public Vector3 force = new Vector3(0, 0, -20);
    Rigidbody _rigidbody;
    WheelCollider wc;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

       
        


        if (Input.GetMouseButton(0))
        {
            _rigidbody.AddForce(force.normalized * _rigidbody.mass, ForceMode.Impulse);
        }
    }

}