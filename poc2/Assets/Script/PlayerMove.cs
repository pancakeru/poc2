using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("BGM")]
    public AudioSource BaseBGM;
    public AudioSource StartBGM;
    public bool isPlaying = false;
    [Header("Basic")]
    public Rigidbody2D rb;
    public Aim aim;
    public FollowCamera followCam;
    public CursorMove cursor;
    public CursorMove Rcursor;
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
    public Transform startLocation;
    //public List<string> abilityList = new List<string>();
    public int shotGun;
    public int defendFilp;
    public GameObject shotGunBall;
    public List<GameObject> ShotGunabilityList;
    public GameObject defendBall;
    public List<GameObject> defendBallabilityList;
    public MMF_Player onAimCrossAppear;
    public MMF_Player onAimCircleDisappear;
    public AudioSource onGunLoad;
    public AudioSource onGasAdd;
    public MMF_Player onLandFeedBack;
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
        if (isPlaying == false && playerIsMoving == true)
        {

            BaseBGM.Stop();
            StartBGM.Play();
            isPlaying = true;
        }

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
                defendBallabilityList[defendBallabilityList.Count - 1].GetComponent<BallFeel>().playOnTrigger();
                defendBallabilityList.RemoveAt(defendBallabilityList.Count - 1);
            }
        }
        ///////////////////reload////////////////////
        if (rotationMount > 290)
        {
            ////Debug.Log("360!");
            //rotationMount = 0f;
            if(shotGun < 5)
            {
                onGunLoad.Play();   
                if (shotGun == 0)
                {
                    onAimCrossAppear.PlayFeedbacks();
                }
                shotGun += 1;
                rotationMount = 0f;
                GameObject PShotGun;
                PShotGun = Instantiate(shotGunBall, startLocation.transform);
                ShotGunabilityList.Add(PShotGun);
                
                //counterClockSpinAmount += 1;
                ////shotGunAmount += 1;
            }
        }
        else if (rotationMount < -290)
        {
            rotationMount = 0f;
            if (defendFilp < 5)
            {
                onGasAdd.Play();
                defendFilp += 1;
                rotationMount = 0f;
                GameObject PDefend;
                PDefend = Instantiate(defendBall, startLocation.transform);
                defendBallabilityList.Add(PDefend);
                //counterClockSpinAmount += 1;
                ////shotGunAmount += 1;
            }
        }
        //////////////shoot/////////////////////
        if (Input.GetMouseButtonDown(0))
        {
            if (shotGun > 0)
            {
                aim.ShootShotGun();
                shotGun -= 1;
                ShotGunabilityList[ShotGunabilityList.Count - 1].GetComponent<BallFeel>().playOnTrigger();
                ShotGunabilityList.RemoveAt(ShotGunabilityList.Count - 1);
                if (shotGun == 0)
                { 
                    onAimCircleDisappear.PlayFeedbacks();
                }
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
        inDefendFilp = true;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.gravityScale = 0.01f;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        yield return new WaitForSeconds(3f);
        rb.gravityScale = 1f;
        inDefendFilp = false;
    }

    public void givePlayerAbilityWhenLand()
    {
        //////put ability on here////
        //if (clockSpinAmount == 1 && counterClockSpinAmount == 0)
        //{
        //    abilityList.Add("ShotGun");
        //}
        if (shotGun != 0)
        {
            onAimCircleDisappear.PlayFeedbacks();
        }
        //clockSpinAmount = 0;
        //counterClockSpinAmount = 0;
        shotGun = 0;
        //defendFilp = 0;
        for (int i = 0; i < ShotGunabilityList.Count; i++)
        {
            ShotGunabilityList[i].GetComponent<BallFeel>().playOnDestory();
            onLandFeedBack.PlayFeedbacks();
        }

        //for (int i = 0; i < defendBallabilityList.Count; i++)
        //{
        //    defendBallabilityList[i].GetComponent<BallFeel>().playOnDestory();
        //}
        ShotGunabilityList.Clear();
        
        //defendBallabilityList.Clear();
    }

    public IEnumerator onBurnOut()
    {
        
        Time.timeScale = 1;
        yield return new WaitForSeconds(2f);
        inSLowMotion = false;
    }
}
