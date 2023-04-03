using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    private bool canJump => (Physics.OverlapSphere(groundCheckPoint.position, 0.2f, whatIsGround).Length > 0);
    private PlayerMotor motor;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float jumpForce = 3000f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Animator anim;
    //[SerializeField] private GameObject projectile;
    [SerializeField] private Gun activeGun;

    public static PlayerController instance;


    private void Awake() {
	instance = this;
    }

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
	float _speed = speed;
	if (Input.GetKey(KeyCode.LeftShift)) _speed = runSpeed;
        Vector3 velocity = (movHorizontal + movVertical).normalized * _speed;

	// apply movement animation
	anim.SetFloat("moveSpeed", movHorizontal.magnitude + movVertical.magnitude);
	anim.SetBool("onGround", canJump);

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
	if (canJump && Input.GetButton("Jump")) {
	    _jumpForce = Vector3.up * jumpForce;
	}

	// Apply the jump force
	motor.ApplyJump(_jumpForce);


	// Handle shooting
	if (Input.GetMouseButtonDown(0)) Shoot(); // left click
	if (Input.GetMouseButton(0)) ShootFullyAutomatic(); // holding down left click
    }


    public Gun GetActiveGun() {
	return activeGun;
    }

    
    private void Shoot() {
	// fire a round
	Camera camera = motor.getCamera();
	activeGun.ShootSingleRound(camera);
    }


    private void ShootFullyAutomatic() {
	// fire a round
	Camera camera = motor.getCamera();
	activeGun.ShootFullyAutomatic(camera);
    }


    public void FreezeMovement() {
	anim.SetBool("isDead", true);
	motor.Freeze();
    }
}

