using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Transform player; 
    public Transform cursor; 
    public float radius = 2.0f; 
    public float rotationDuration = 2.0f; 
    public float moveDuration = 1.0f; 

    private float elapsedTime = 0f; 
    private bool isRotating = true; 
    private bool isMovingToCursor = false; 
    private Vector3 startPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cursor = GameObject.FindGameObjectWithTag("Cursor").transform;
    }
    void Update()
    {
        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / rotationDuration);
            t = Mathf.SmoothStep(0f, 1f, t);
            float angle = Mathf.Lerp(0f, 180f, t);
            float radians = Mathf.Deg2Rad * angle;
            Vector3 offset = new Vector3(
                Mathf.Sin(radians) * radius,
                -Mathf.Cos(radians) * radius, 
                0 
            );

            transform.position = player.position + offset;
            if (t >= 1f)
            {
                isRotating = false;
                isMovingToCursor = true;
                elapsedTime = 0f; 
                startPosition = transform.position;
            }
        }
        else if (isMovingToCursor)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.position = Vector3.Lerp(startPosition, cursor.position, t);
            if (t >= 1f)
            {
                isMovingToCursor = false;
            }
        }
    }
}
