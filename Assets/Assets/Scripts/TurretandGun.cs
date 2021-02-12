using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class TurretandGun : MonoBehaviour
{
    //Rotates the Gun , the Gun Camera and the Turret with the mouse

    public GameObject TurretBase;
    public GameObject Gun;
    public GameObject GunCamera;
    public GameObject vehicle;
   
    //Adjust Turret Speed and Gun speed to addjust sensitivity 
    public float TurretSpeed;
    public float GunSpeed;

    float TurretAngle;
    float GunAngle;

    //Ajust GunBorderDown and GunBorderUp to adjust the limit of the gun tilt
    public float GunBorderDown;
    public float GunBorderUp;
   
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    int xPos = Screen.width/2, yPos = Screen.height/2;
    public static Vector3 originalposfrommouse;
    
  
    private void Start()
    {
        Cursor.visible = false;
       SetCursorPos(xPos, yPos);
        TurretAngle = Input.GetAxis("Mouse X");
        GunAngle = Input.GetAxis("Mouse Y");
        RotateTurrent();
        RotateGun();
    }
    void Update()
    {
       
        TurretAngle += Input.GetAxis("Mouse X") * TurretSpeed * Time.deltaTime;
        GunAngle +=  Input.GetAxis("Mouse Y") * -GunSpeed * Time.deltaTime;

    }
    void FixedUpdate()
    {
        RotateTurrent();
        RotateGun();
    }


    void RotateTurrent()
    {
        TurretBase.transform.localRotation = Quaternion.AngleAxis(TurretAngle-90, Vector3.up);
    }
    void RotateGun()
    {
        GunAngle = Mathf.Clamp(GunAngle, GunBorderDown, GunBorderUp);
        Gun.transform.localRotation = Quaternion.AngleAxis(GunAngle, Vector3.right);
        GunCamera.transform.localRotation = Quaternion.AngleAxis(GunAngle, Vector3.right);

    }



}



