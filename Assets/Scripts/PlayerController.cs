using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float jumpForce = 3000f;

    private bool canJump;
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private LayerMask whatIsGround;


    private PlayerMotor motor;
    

    private void Start() {
        motor = GetComponent<PlayerMotor>();
    }


    private void Update() {
        // calculate movement velocity as a 3D vector 
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        
        // final movement vector 
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        // apply movement 
        motor.Move(velocity);
        // calculate rotation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        // apply rotation
        motor.Rotate(rotation);

        // calculate camera rotation as a 3D vector 
        float xRot = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRot * lookSensitivity;
        // Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        // apply camera rotation
        motor.RotateCamera(cameraRotationX);
	
	// calculate the jump force based on player input
	Vector3 _jumpForce = Vector3.zero;
	canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;
	if (canJump && Input.GetButton("Jump")) {
	    _jumpForce = Vector3.up * jumpForce;
	} else {
	    _jumpForce = Vector3.zero;
	}
	
	// Apply the jump force
	motor.ApplyJump(_jumpForce);
	
    }

}

