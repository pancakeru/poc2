using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Basic")]
    public Rigidbody2D rb;
    public Aim aim;
    [Header("Movement")]
    public Vector2 movement;
    public WheelJoint2D Fwheel;
    public WheelJoint2D Bwheel;
    public float speedMultiplier;
    public float breakRate;
    public Wheel WheelF;
    public Wheel WheelB;
    [Header("Turn")]
    public bool isTurning;
    public Rigidbody2D FWheelRb;
    public Rigidbody2D BwheelRb;
    [Header("filp")]
    public float torqueForce;
    public float maxAngularVelocity;
    public float rotationMount;
    private float previousRotation;
    public int clockSpinAmount;
    public int counterClockSpinAmount;

    [Header("ability")]
    public List<string> abilityList = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        if (speedMultiplier < 0)
        {
            PhysicsMaterial2D FWmaterial = new PhysicsMaterial2D();
            FWmaterial.friction = 0.8f;
            FWmaterial.bounciness = 0;
            BwheelRb.sharedMaterial = FWmaterial;

            PhysicsMaterial2D BWmaterial = new PhysicsMaterial2D();
            BWmaterial.friction = 0.4f;
            BWmaterial.bounciness = 0;
            FWheelRb.sharedMaterial = BWmaterial;
        }
        else if (speedMultiplier > 0)
        {
            PhysicsMaterial2D FWmaterial = new PhysicsMaterial2D();
            FWmaterial.friction = 0.8f;
            FWmaterial.bounciness = 0;
            FWheelRb.sharedMaterial = FWmaterial;

            PhysicsMaterial2D BWmaterial = new PhysicsMaterial2D();
            BWmaterial.friction = 0.4f;
            BWmaterial.bounciness = 0;
            BwheelRb.sharedMaterial = BWmaterial;
        }
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        ///////////////////////////Move//////////////////////////
        if (movement.y != 0 && isTurning == false)
        {
            JointMotor2D Bmotor = Bwheel.motor;
            Bmotor.motorSpeed = movement.y * speedMultiplier;
            Bwheel.motor = Bmotor;

            JointMotor2D Fmotor = Fwheel.motor;
            Fmotor.motorSpeed = movement.y * speedMultiplier;
            Fwheel.motor = Fmotor;
        }
        else if (movement.y == 0 && isTurning == false)
        {
            JointMotor2D Bmotor = Bwheel.motor;
            Bmotor.motorSpeed = Mathf.Lerp(Bmotor.motorSpeed, 0, Time.fixedDeltaTime * breakRate);
            Bwheel.motor = Bmotor;

            JointMotor2D Fmotor = Fwheel.motor;
            Fmotor.motorSpeed = Mathf.Lerp(Fmotor.motorSpeed, 0, Time.fixedDeltaTime * breakRate);
            Fwheel.motor = Fmotor;
        }
        ///////////////////filp////////////////////////////////
        if (movement.x != 0 && isTurning == false)
        {
            rb.AddTorque(-movement.x * torqueForce * Time.deltaTime);
            if (rb.angularVelocity > maxAngularVelocity)
            {
                rb.angularVelocity = maxAngularVelocity;
               
            }
            else if (rb.angularVelocity < -maxAngularVelocity)
            {
                rb.angularVelocity = -maxAngularVelocity;
                

            }

            
        }
        //////////////onland//////////////////////
        if (WheelF.onAir == false && WheelB.onAir == false)
        {
            if (clockSpinAmount > 0 || counterClockSpinAmount > 0)
            {
                givePlayerAbilityWhenLand();
            }
            
        }

        /////////////////////
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationDelta = rb.rotation - previousRotation;

        
        if (rotationDelta > 180)
        {
            rotationDelta -= 360;
        }
        else if (rotationDelta < -180)
        {
            rotationDelta += 360;
        }


        rotationMount += rotationDelta;

        previousRotation = rb.rotation;
        //////////////////////Turning///////////////////////////
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTurning == false)
            {
                StartCoroutine(Turning());
            }
        }
        ///////////////////reload////////////////////
        if (rotationMount > 300)
        {
            //Debug.Log("360!");
            rotationMount = 0f;
           
            counterClockSpinAmount += 1;
            //shotGunAmount += 1;

        }
        else if (rotationMount < -300)
        {
            //Debug.Log("-360!");
            clockSpinAmount += 1;
            rotationMount = 0f;
        }
        //////////////shoot/////////////////////
        if (Input.GetMouseButtonDown(0))
        {
            if (abilityList.Count > 0)
            {
                if (abilityList[0] == "ShotGun")
                {
                    aim.ShootShotGun();
                }

                abilityList.RemoveAt(0);
            }
        }
    }

    private IEnumerator Turning()
    {
        isTurning = true;
        JointMotor2D Bmotor = Bwheel.motor;
        Bmotor.motorSpeed = 0;
        Bwheel.motor = Bmotor;

        JointMotor2D Fmotor = Fwheel.motor;
        Fmotor.motorSpeed = 0;
        Fwheel.motor = Fmotor;
        yield return new WaitForSeconds(2f);
        isTurning = false;
        speedMultiplier = speedMultiplier * - 1;
        if (speedMultiplier < 0)
        {
            PhysicsMaterial2D FWmaterial = new PhysicsMaterial2D();
            FWmaterial.friction = 0.8f; 
            FWmaterial.bounciness = 0;
            BwheelRb.sharedMaterial = FWmaterial;

            PhysicsMaterial2D BWmaterial = new PhysicsMaterial2D();
            BWmaterial.friction = 0.4f;
            BWmaterial.bounciness = 0;
            FWheelRb.sharedMaterial = BWmaterial;
        }
        else if (speedMultiplier > 0)
        {
            PhysicsMaterial2D FWmaterial = new PhysicsMaterial2D();
            FWmaterial.friction = 0.8f;
            FWmaterial.bounciness = 0;
            FWheelRb.sharedMaterial = FWmaterial;

            PhysicsMaterial2D BWmaterial = new PhysicsMaterial2D();
            BWmaterial.friction = 0.4f;
            BWmaterial.bounciness = 0;
            BwheelRb.sharedMaterial = BWmaterial;
        }
    }

    public void givePlayerAbilityWhenLand()
    {
        //////put ability on here////
        if (clockSpinAmount == 1 && counterClockSpinAmount == 0)
        {
            abilityList.Add("ShotGun");
        }

        clockSpinAmount = 0;
        counterClockSpinAmount = 0;
    }
}
