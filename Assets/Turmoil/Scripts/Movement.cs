using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{
    CharacterController cc;
    Camera cam;
    AnimationManager anim;
    Combat com;
    AnimateUV aUV;

    /*--- PLAYER MOVEMENT ---*/
    private float fSpeed, sSpeed;                                              //Store movement speeds before calculating them
    private float moveSpeed = 8.0f;                                                 //Character speed
    private float verticalVelocity = 0.0f;

    /*--- CAMERA ROTATION ---*/
    private float sensitivity = 5.0f;                                               //Mouse sensitivity
    private float rotateHorizontal = 0.0f;                                          //Horizontal view rotation
    private float rotateVertical = 0.0f;                                            //Vertical view rotation
    private float verticalRange = 60.0f;                                            //Restrict camera

    /*--- CUSTOM CLASSES ---*/
    private CooldownTimer cdLightning;


    //VARIABLES
    private bool cdReady;


    void Start ()
    {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        anim = GetComponentInChildren<AnimationManager>();
        com = GetComponent<Combat>();
        aUV = GetComponentInChildren<AnimateUV>();
        cdLightning = new CooldownTimer(0.1f);

        if (!isLocalPlayer)
        {
            cam.enabled = false;
        }
    }
	
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Rotate();
        Move();






        if (Input.GetKey(KeyCode.Mouse0))
        {
            anim.Attack(1);
            aUV.SetUVSpeed(0.0001f);

            //If cooldown is reached, attack once
            if (cdLightning.ActionReady())
            {
                cdLightning.UpdateTimer();
                cdReady = true;
            }
            else
            {
                cdReady = false;
            }
            com.LightningAttack(cdReady);

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.StopAttack(1);
            com.StopLightningAttack();
            aUV.SetUVSpeed(0.1f);
        }
	}

    void Rotate()
    {
        /*--- CAMERA ROTATION ---*/
        rotateHorizontal = rotateHorizontal + (Input.GetAxis("Mouse X") * sensitivity);
        rotateVertical = rotateVertical - (Input.GetAxis("Mouse Y") * sensitivity);

        rotateVertical = Mathf.Clamp(rotateVertical, -verticalRange, verticalRange);
        transform.localRotation = Quaternion.Euler(0, rotateHorizontal, 0);
        cam.transform.localRotation = Quaternion.Euler(rotateVertical, 0, 0);
    }

    void Move()
    {
                /*--- PLAYER MOVEMENT ---*/
        verticalVelocity += (Physics.gravity.y * Time.deltaTime);
        if (cc.isGrounded)
        {
            verticalVelocity = 0.0f;
        }

        fSpeed = Input.GetAxisRaw("Vertical") * moveSpeed;                          //Set forward speed to input of axis * move speed
        sSpeed = Input.GetAxisRaw("Horizontal") * moveSpeed;                        //Set side speed to input of axis * move speed
        Vector3 movement = new Vector3(sSpeed, verticalVelocity, fSpeed);           //The final movement stored in a Vector3
        movement = transform.rotation * movement;  //transform.rotation decides back = rotation?
        cc.Move(movement * Time.deltaTime);
    }
}
