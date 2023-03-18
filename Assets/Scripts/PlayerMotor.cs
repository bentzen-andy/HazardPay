using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
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

    // Runs every physics iteration 
    private void FixedUpdate() {

        Debug.Log("cam.transform.rotation.eulerAngles: " + cam.transform.rotation.eulerAngles);

        PerformMovement();
        PerformRotation();
    }

    // Perform movement based on velocity variable 
    private void PerformMovement() {
        if (velocity != Vector3.zero) {
            // MovePosition will stop the rb from moving if something is in the way. 
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Perform rotation
    void PerformRotation () {
        rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
        if (cam != null) {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
}
