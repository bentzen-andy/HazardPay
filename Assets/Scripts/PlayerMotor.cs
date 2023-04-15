using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField] private Camera cam;
    [SerializeField] private float cameraRotationLimit = 85f;
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float fallSpeed = 5f;
    [SerializeField] private float maxSpeed = 10f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 jumpForce = Vector3.zero;
    private Vector3 bounceForce = Vector3.zero;
    private bool isFalling => (rb.velocity.y < -0.1f);

    private float startFOV;
    private float targetFOV;

    private bool isFrozen;

    private Rigidbody rb;


    public Camera getCamera() {
	return cam;
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
	startFOV = cam.fieldOfView;
	targetFOV = startFOV;
    }


    // Gets a movement vector
    public void Move(Vector3 velocity) {
        this.velocity = velocity;
    }


    // Gets a rotation vector
    public void Rotate(Vector3 rotation) {
        this.rotation = rotation;
    }


    // Gets a rotation vector for the camera
    public void RotateCamera(float rotationX) {
        this.cameraRotationX = rotationX;
    }


    // Get a force vector
    public void ApplyJump (Vector3 jumpForce) {
	this.jumpForce = jumpForce;
    }


    // Get a force vector
    public void ApplyBounce (Vector3 bounceForce) {
	this.bounceForce = bounceForce;
    }


    public void ZoomIn(float newZoom) {
	targetFOV = newZoom;
    }


    public void ZoomOut() {
	targetFOV = startFOV;
    }


    // Runs every physics iteration
    private void FixedUpdate() {
	if (isFrozen) return;
        PerformMovement();
        SpeedControl();
        PerformRotation();
        PerformZoom();
    }


    // Perform movement based on velocity variable 
    private void PerformMovement() {
        if (velocity != Vector3.zero) {
            // MovePosition will stop the rb from moving if something is in the way. 
            //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	    rb.AddForce(velocity * Time.fixedDeltaTime, ForceMode.Force);
        }

	if (jumpForce != Vector3.zero) {
	    rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
	}

	if (bounceForce != Vector3.zero) {
	    rb.AddForce(bounceForce * Time.fixedDeltaTime, ForceMode.Acceleration);
	}
    }

    
    private void SpeedControl() {
	if (velocity != Vector3.zero) {
	    //Debug.Log("rb.velocity.magnitude" +  rb.velocity.magnitude);
	    //Debug.Log("velocity.magnitude  * Time.fixedDeltaTime" + velocity.magnitude  * Time.fixedDeltaTime);
	    Debug.Log("rb.velocity.y" + rb.velocity.y);
	    
	    rb.drag = groundDrag;
	    
	    // limit velocity in needed
	    Vector3 currFlatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
	    //float maxVelocity = velocity.magnitude;
	    if (currFlatVelocity.magnitude > maxSpeed) {
		Vector3 limitedVelocity = currFlatVelocity.normalized * maxSpeed;
		rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
	    }
	    
	}

	// correction force to make player fall faster
	if (isFalling) {
	    Debug.Log(isFalling);
	    Vector3 fallForce = new Vector3(rb.velocity.x, fallSpeed, rb.velocity.z);
	    rb.AddForce(fallForce * Time.fixedDeltaTime, ForceMode.Acceleration);
	}
	
    }


    // Perform rotation
    private void PerformRotation() {
        rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
        if (cam != null) {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }


    private void PerformZoom() {
	cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }


    public void Freeze() {
	isFrozen = true;
    }

}
