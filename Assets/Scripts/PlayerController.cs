using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<Gun> guns = new List<Gun>();
    [SerializeField] private Gun activeGun;
    //[SerializeField] private GameObject projectile;
 
    public static PlayerController instance;


    private void Awake() {
	instance = this;
    }

    private void Start() {
        motor = GetComponent<PlayerMotor>();
	//activeGun = guns[0];
	InitGun();
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


	// Handle weapon swaping
	SwapGun();
    }


    public Gun GetActiveGun() {
	return activeGun;
    }

    
    /* CUT THIS OUT  Sat Apr  8 09:46:57 2023 /andrew_bentzen
    public void SetActiveGun(Gun gun) {
	activeGun = gun;
    }
    * CUT THIS OUT /andrew_bentzen */


    private void InitGun() {
	activeGun.gameObject.SetActive(false);
	activeGun = guns[0];
	activeGun.gameObject.SetActive(true);
    }


    private void SwapGun() {
	if (Input.GetKeyDown(KeyCode.Alpha1)) {
	    SwapGun(0);
	    //activeGun.gameObject.SetActive(false);
	    //activeGun = guns[0];
	    //activeGun.gameObject.SetActive(true);
	} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
	    SwapGun(1);
	    //activeGun.gameObject.SetActive(false);
	    //activeGun = guns[1];
	    //activeGun.gameObject.SetActive(true);
	} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
	    SwapGun(2);
	    //activeGun.gameObject.SetActive(false);
	    //activeGun = guns[2];
	    //activeGun.gameObject.SetActive(true);
	} else if (Input.GetKeyDown(KeyCode.Tab)) {
	    int currGunNumber = guns.IndexOf(activeGun);
	    SwapGun((currGunNumber + 1) % 3);
	    //activeGun.gameObject.SetActive(false);
	    //activeGun = guns[(currGunNumber + 1) % 3];
	    //activeGun.gameObject.SetActive(true);
	}
    }


    private void SwapGun(int newGunIndex) {
	activeGun.gameObject.SetActive(false);
	activeGun = guns[newGunIndex];
	activeGun.gameObject.SetActive(true);
    }

    
    private void Shoot() {
	//Debug.Log("Shooting a round----1");
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

