using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField] private Camera camera;
    [SerializeField] private float cameraRotationLimit = 85f;
    [SerializeField] private float zoomSpeed = 4f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 jumpForce = Vector3.zero;

    private float startFOV;
    private float targetFOV;

    private bool isFrozen;

    private Rigidbody rb;


    public Camera getCamera() {
	return camera;
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
	startFOV = camera.fieldOfView;
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


    // Get a force vector for our thrusters
    public void ApplyJump (Vector3 jumpForce) {
	this.jumpForce = jumpForce;
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
        PerformRotation();
        PerformZoom();
    }


    // Perform movement based on velocity variable 
    private void PerformMovement() {
        if (velocity != Vector3.zero) {
            // MovePosition will stop the rb from moving if something is in the way. 
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

	if (jumpForce != Vector3.zero) {
	    rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
	}
    }


    // Perform rotation
    private void PerformRotation() {
        rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
        if (camera != null) {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of our camera
            camera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }


    private void PerformZoom() {
	camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
    }


    public void Freeze() {
	isFrozen = true;
    }

}
