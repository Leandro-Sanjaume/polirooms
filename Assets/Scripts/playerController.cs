using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    public float walkingSpeed = 10.0f;
    public float runningSpeed = 15.0f;
    public float earthGravity = 2000f;
    private float lookSpeed = 2.0f;
    private float lookXLimit = 45.0f;
    CharacterController characterController;
    public Camera playerCamera;
    private Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
         // We are grounded, so recalculate move direction based on axes
        Vector3 right = transform.right;
        Vector3 forward = transform.forward;

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveInput = moveInput.normalized;
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        moveInput[0] = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveInput.x : 0;
        moveInput[2] = canMove ? (isRunning ? runningSpeed : walkingSpeed) *moveInput.z : 0;
        
        moveDirection =  (right *  moveInput.x) + (forward * moveInput.z);
        // Move the controller

        if (characterController.isGrounded == false)
        {       
            moveDirection.y -= (earthGravity * Time.deltaTime );
        }else if(characterController.isGrounded){
            moveDirection.y = 0f;
        }

        characterController.Move(moveDirection  * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
