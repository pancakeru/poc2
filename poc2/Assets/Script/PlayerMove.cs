using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Basic")]
    public Rigidbody2D rb;
    public Aim aim;
    public FollowCamera followCam;
    [Header("Movement")]
    public bool playerIsMoving;
    public Vector2 movement;
    public WheelJoint2D Fwheel;
    public WheelJoint2D Bwheel;
    public float speedMultiplier;
    public float breakRate;
    public int maxSpeed;
    public Wheel WheelF;
    public Wheel WheelB;
    public bool wantFlip;
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

    [Header("defendFilp")]
    public bool inDefendFilp;
    public GameObject Shield;
    public MMF_Player filpDefendFeedBack;
    [Header("ability")]
    //public List<string> abilityList = new List<string>();
    public int shotGun;
    public int defendFilp;
    [Header("slowMotion")]
    public int slowMotionTime;
    public int slowMotionTimer;
    public bool inSLowMotion;
    public float slowMotionRate;
    public MMProgressBar slowMotionBar;
    public bool isBurnOut;
    public MMF_Player onBurn;
    public MMF_Player offBurn;
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
        if (movement.y != 0 && playerIsMoving == true)
        {
            //JointMotor2D Bmotor = Bwheel.motor;
            //Bmotor.motorSpeed = movement.y * speedMultiplier;
            //Bwheel.motor = Bmotor;

            //JointMotor2D Fmotor = Fwheel.motor;
            //Fmotor.motorSpeed = movement.y * speedMultiplier;
            //Fwheel.motor = Fmotor;
            if (WheelF.onAir == false || WheelB.onAir == false)
            {
                Vector2 force = transform.right * movement.y * speedMultiplier;
                rb.AddForce(force);
                rb.drag = 0;
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
                }
            }


        }
        else if (movement.y == 0 && playerIsMoving == true)
        {
            if (WheelF.onAir == false || WheelB.onAir == false)
            {
                Vector2 force = transform.right * new Vector2(1,0)* speedMultiplier * 0.5f;
                rb.AddForce(force);
                rb.drag = 0;
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed / 2;
                }
            }
            //if (WheelF.onAir == false && WheelB.onAir == false)
            //{
            //    Vector2 force = transform.right * movement.y * speedMultiplier;
            //    rb.AddForce(force);
            //    rb.drag = 0;
            //    if (rb.velocity.magnitude > maxSpeed)
            //    {
            //        rb.velocity = rb.velocity.normalized * maxSpeed;
            //    }
            //    rb.drag = breakRate;
            //}
            //else
            //{
            //    Vector2 force = transform.right * movement.y * speedMultiplier;
            //    rb.AddForce(force);
            //    rb.drag = 0;
            //    if (rb.velocity.magnitude > maxSpeed)
            //    {
            //        rb.velocity = rb.velocity.normalized * maxSpeed;
            //    }

            //}

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
            if (shotGun > 0 || defendFilp > 0)
            {
                givePlayerAbilityWhenLand();
            }
            
        }

        /////////////////////

        if (inSLowMotion == true)
        {
            if (slowMotionTimer > 0)
            {
                Time.timeScale = slowMotionRate;
                slowMotionTimer--;
                slowMotionBar.UpdateBar(slowMotionTimer, 0, slowMotionTime);
            }
            else if (slowMotionTimer == 0)
            {
                isBurnOut = true;
                StartCoroutine(onBurnOut());
                slowMotionBar.UpdateBar(slowMotionTimer, 0, slowMotionTime);
                onBurn.PlayFeedbacks();
            }

        }
        else if (inSLowMotion == false)
        {
            if (slowMotionTimer < slowMotionTime)
            {
                Time.timeScale = 1;
                slowMotionTimer += 1;
                slowMotionBar.UpdateBar(slowMotionTimer, 0, slowMotionTime);
                if (isBurnOut == true && slowMotionTime == slowMotionTimer)
                { 
                    isBurnOut = false;
                    offBurn.PlayFeedbacks();
                }

            }
        }
        
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
            if (defendFilp > 0)
            {
                defendFilp--;
                filpDefendFeedBack.PlayFeedbacks();
                StartCoroutine(defendFilpG());
            }
        }
        ///////////////////reload////////////////////
        if (rotationMount > 360)
        {
            ////Debug.Log("360!");
            //rotationMount = 0f;
            shotGun += 1;
            rotationMount = 0f;
            //counterClockSpinAmount += 1;
            ////shotGunAmount += 1;

        }
        else if (rotationMount < -360)
        {
            defendFilp += 1;
            rotationMount = 0f;
        }
        //////////////shoot/////////////////////
        if (Input.GetMouseButtonDown(0))
        {
            if (shotGun > 0)
            {
                aim.ShootShotGun();
                shotGun -= 1;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isBurnOut == false && slowMotionTimer > 0)
            {
                inSLowMotion = true;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            inSLowMotion = false;
            
        }

    }



    private IEnumerator Turning()
    {
        isTurning = true;
        //JointMotor2D Bmotor = Bwheel.motor;
        //Bmotor.motorSpeed = 0;
        //Bwheel.motor = Bmotor;

        //JointMotor2D Fmotor = Fwheel.motor;
        //Fmotor.motorSpeed = 0;
        //Fwheel.motor = Fmotor;
        if (WheelF.onAir == false && WheelB.onAir == false)
        {
            rb.drag = breakRate;
        }
        else
        {
            rb.drag = 0;
        }
        speedMultiplier = speedMultiplier * -1;
        if (wantFlip)
        {
            if (followCam != null)
                followCam.flip();
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        
        yield return new WaitForSeconds(0.2f);
        isTurning = false;
        rb.drag = 0;
        //if (speedMultiplier < 0)
        //{
        //    //PhysicsMaterial2D FWmaterial = new PhysicsMaterial2D();
        //    //FWmaterial.friction = 0.8f; 
        //    //FWmaterial.bounciness = 0;
        //    //BwheelRb.sharedMaterial = FWmaterial;

        //    //PhysicsMaterial2D BWmaterial = new PhysicsMaterial2D();
        //    //BWmaterial.friction = 0.4f;
        //    //BWmaterial.bounciness = 0;
        //    //FWheelRb.sharedMaterial = BWmaterial;
        //}
        //else if (speedMultiplier > 0)
        //{
        //    //PhysicsMaterial2D FWmaterial = new PhysicsMaterial2D();
        //    //FWmaterial.friction = 0.8f;
        //    //FWmaterial.bounciness = 0;
        //    //FWheelRb.sharedMaterial = FWmaterial;

        //    //PhysicsMaterial2D BWmaterial = new PhysicsMaterial2D();
        //    //BWmaterial.friction = 0.4f;
        //    //BWmaterial.bounciness = 0;
        //    //BwheelRb.sharedMaterial = BWmaterial;
        //}
    }

    IEnumerator defendFilpG()
    {

        rb.gravityScale = 0.1f;
        yield return new WaitForSeconds(3f);
        rb.gravityScale = 1f;
    }

    public void givePlayerAbilityWhenLand()
    {
        //////put ability on here////
        //if (clockSpinAmount == 1 && counterClockSpinAmount == 0)
        //{
        //    abilityList.Add("ShotGun");
        //}

        //clockSpinAmount = 0;
        //counterClockSpinAmount = 0;
        shotGun = 0;
        defendFilp = 0;
    }

    public IEnumerator onBurnOut()
    {
        
        Time.timeScale = 1;
        yield return new WaitForSeconds(2f);
        inSLowMotion = false;
    }
}
