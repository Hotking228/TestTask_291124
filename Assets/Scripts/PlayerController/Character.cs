using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    private Rigidbody playerRb;
    [Header("Move forward settings")]
    [SerializeField] private float forwardVelocity;
    [SerializeField] private float maxVelocity;



    [Header("Move side to side settings")]
    private bool isMouseDown = false;
    private float mouseXPos;
    [SerializeField] private float maxMouseOffset;
    [SerializeField] private float maxPlayerOffset = 4;
    [SerializeField] private GameObject playerView;
    private float mouseOffsetNorm;
    [SerializeField] private float maxRotation = 30;
    [SerializeField] private GameObject playerModel;
    private float prevPlayerPos = 0;
    [Header("Rotation settings")]
    [SerializeField] private float rotationSpeed;
    private Vector3 targetRotation;
    private bool isRotating = false;
    [SerializeField] private float moveToPointVelocity;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void ResetSpeed()
    {
        playerRb.velocity = new Vector3 (0, 0, 0);
    }

    public void MoveForward()
    {
        playerRb.AddForce(transform.forward * Time.fixedDeltaTime * forwardVelocity, ForceMode.VelocityChange);

        playerRb.velocity = ClampVelocity(maxVelocity, playerRb.velocity);
        if (isRotating)
            playerRb.velocity = new Vector3(0,0,0);
    }

    public void MoveSideToSide()
    {
        GetPlayerPosNorm();

        playerView.transform.localPosition = new Vector3(maxPlayerOffset * mouseOffsetNorm, playerView.transform.localPosition.y, playerView.transform.localPosition.z);
        
    }


    private void RotateView(float offset)
    {
               
        float yAngle = offset * maxRotation;

        playerModel.transform.localRotation = Quaternion.Euler(new Vector3(0, yAngle, 0));



    }



    private void GetPlayerPosNorm()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            mouseXPos = Input.mousePosition.x;
            isMouseDown = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            prevPlayerPos = mouseOffsetNorm;
        }

        if (isMouseDown)
        {
            float currentPos = Input.mousePosition.x;
            float offset = currentPos - mouseXPos;
            mouseOffsetNorm = offset / maxMouseOffset + prevPlayerPos;
            mouseOffsetNorm = Mathf.Clamp(mouseOffsetNorm, -1, 1);
            RotateView(mouseOffsetNorm - prevPlayerPos);
        }
        else
        {
            RotateView(0);
        }
    }

    private Vector3 ClampVelocity(float magnitude, Vector3 vector)
    {
        
            float x = vector.x;
            float z = vector.z;
            float currentMagnitude = Mathf.Sqrt(x * x + z * z);

            if (currentMagnitude <= magnitude) return vector;

            float koef = currentMagnitude / magnitude;

            return new Vector3(x / koef, vector.y, z / koef);


  
    }

    public void SetRotation(float direction, Vector3 newPos)
    {
        targetRotation = new Vector3(0, transform.localRotation.eulerAngles.y + direction * 90, 0);
        isRotating = true;
        this.newPos = newPos;

    }
    [SerializeField]private Vector3 newPos;
    

    private void SetNewPlayerPos(Vector3 newPos)
    {
        Vector3 oldPos = playerView.transform.localPosition;
        if (transform.localRotation.eulerAngles.y % 180 == 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(newPos.x, transform.position.y, transform.position.z), Time.deltaTime * moveToPointVelocity);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, newPos.z), Time.deltaTime * moveToPointVelocity);
        }
        playerView.transform.localPosition = oldPos;
    }

    private void Rotate()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
        if (transform.localRotation.eulerAngles.y == targetRotation.y)
        {
            isRotating = false;
            
            
        }
    }

    private void Update()
    {
        
        if (isRotating)
            Rotate();
        SetNewPlayerPos(newPos);
    }

    





}

    

