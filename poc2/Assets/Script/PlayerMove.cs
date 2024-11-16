using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Basic")]
    public Rigidbody2D rb;
    [Header("Movement")]
    public Vector2 movement;
    public WheelJoint2D Fwheel;
    public WheelJoint2D Bwheel;
    public float speedMultiplier;
    public float breakRate;
    [Header("Turn")]
    public bool isTurning;
    public Rigidbody2D FWheelRb;
    public Rigidbody2D BwheelRb;
    [Header("filp")]
    public float torqueForce;
    public float maxAngularVelocity;
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

    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////Turning///////////////////////////
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTurning == false)
            {
                StartCoroutine(Turning());
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
}
