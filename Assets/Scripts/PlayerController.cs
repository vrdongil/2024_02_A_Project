using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float rotation = 10.0f;

    [Header("Camera Settings")]
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float mouseSenesitivity = 200.0f;

    public float cameraDistance = 5.0f;
    public float minDistance = 1.0f;
    public float maxDistance = 10.0f;

    public float currentX = 0.0f;
    public float currentY = 45.0f;

    public const float Y_ANGLE_MIN = 0.0f;
    public const float Y_ANGLE_MAX = 50.0f;
    
    public float radius = 5.0f;
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;


    private float theta = 0.0f;
    private float phi = 0.0f;
    private float targetVericalRotaion = 0f;
    private float verticalRoatationSpeed = 240f;

    public bool isFirstPerson = true;
    //private bool isGrounded;
    private Rigidbody rb;
    private float rotationSpeed;


    public float fallingThreshold = -0.1f;

    [Header("Ground Check Setting")]

    public float groundCheckDistance = 0.3f;
    public float slopedLimit = 45f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        SetupCameras();
        SetActiveCamera();
    }

    // Update is called once per frame
    void Update()
    {

        HandleRotation();
        
        HandleCameraToggle();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

    }

    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.6f, 0.0f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity * Time.deltaTime;

        if (isFirstPerson)
        {

            transform.rotation = Quaternion.Euler(0.0f, currentX, 0.0f);
            firstPersonCamera.transform.localRotation = Quaternion.Euler(currentY, 0.0f, 0.0f);
        }
        else 
        {
            currentX += mouseX;
            currentY -= mouseY;

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            Vector3 dir = new Vector3(0, 0, -cameraDistance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            thirdPersonCamera.transform.position = transform.position + rotation * dir;
            thirdPersonCamera.transform.LookAt(transform.position);

            cameraDistance = Mathf.Clamp(cameraDistance - Input.GetAxis("Mouse ScrollWheel") * 5, minDistance, maxDistance);   
        }
    }

    void HandleCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            isFirstPerson = !isFirstPerson;
            SetActiveCamera();
        }


    }
    public void HandleJump()
    {

        if (isGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    public void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement;
        if (!isFirstPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 cameraRight = thirdPersonCamera.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            movement = cameraRight * moveHorizontal + cameraForward * moveVertical;


        }
        else
        {
            movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        }

        if (movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 2.0f);
    }

    public bool isFalling()
    {
        return rb.velocity.y < fallingThreshold && !isGrounded();
    }

    public float GetVerticalVelocity()
    { 
         return rb.velocity.y;
    }

}




