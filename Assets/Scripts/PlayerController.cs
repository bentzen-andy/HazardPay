using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    private bool canJump => (Physics.OverlapSphere(groundCheckPoint.position, 0.2f, whatIsGround).Length > 0);
    private PlayerMotor motor;
    //private Vector3 gunStartPos;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 12f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float lookSensitivityNormal = 2f;
    [SerializeField] private float lookSensitivitySniper = 0.5f;
    [SerializeField] private float jumpForce = 3000f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Animator anim;

    [SerializeField] private List<Gun> guns = new List<Gun>();
    [SerializeField] private List<Gun> collectedGuns = new List<Gun>();
    [SerializeField] private Gun activeGun;
    [SerializeField] private Transform gunHolder;
    [SerializeField] private Transform gunPointNormal;
    [SerializeField] private Transform gunPointAimDownSights;
    [SerializeField] private float speedAimingDownSights = 2f;
 
    public static PlayerController instance;


    private void Awake() {
	instance = this;
    }

    private void Start() {
        motor = GetComponent<PlayerMotor>();
	InitGun();
	//gunHolder.position = gunPointNormal.position;
    }


    private void Update() {
	// Keep player from moving/shooting if is dead or game is paused
	if (PlayerHealthController.instance.PlayerIsDead()) return;
	if (GameManager.instance.isPaused) return;

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

	// Handle weapon zoom
	ApplyZoom();
    }


    public Gun GetActiveGun() {
	return activeGun;
    }

    
    public List<Gun> GetAllGuns() {
	return guns;
    }

    
    public List<Gun> GetAllCollectedGuns() {
	return collectedGuns;
    }

    
    /* CUT THIS OUT  Sat Apr  8 09:46:57 2023 /andrew_bentzen
    public void SetActiveGun(Gun gun) {
	activeGun = gun;
    }
    * CUT THIS OUT /andrew_bentzen */


    private void InitGun() {
	// initialize all guns
	foreach (Gun gun in guns) {
	    gun.gameObject.SetActive(false);
	}
	SwapGun(0);
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


    private void SwapGun() {
	if (Input.GetKeyDown(KeyCode.Alpha1)) {
	    SwapGun(0);
	} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
	    SwapGun(1);
	} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
	    SwapGun(2);
	} else if (Input.GetKeyDown(KeyCode.Tab)) {
	    int currGunNumber = guns.IndexOf(activeGun);
	    bool res = SwapGun((currGunNumber + 1) % 3);
	    currGunNumber++;
	    while (!res) {
	      res = SwapGun((currGunNumber + 1) % 3);
	      currGunNumber++;
	    }
	}
    }


    public bool SwapGun(int newGunIndex) {
	if (!collectedGuns.Contains(guns[newGunIndex])) return false;
	activeGun.gameObject.SetActive(false);
	activeGun = guns[newGunIndex];
	activeGun.gameObject.SetActive(true);
	return true;
    }


    private void ApplyZoom() {
	if (Input.GetMouseButtonDown(1)) {
	    motor.ZoomIn(activeGun.GetZoom());
	} else if (Input.GetMouseButtonUp(1)) {
	    motor.ZoomOut();
	    lookSensitivity = lookSensitivityNormal;
	}

	if (Input.GetMouseButton(1)) {
	    if (activeGun.GetWeaponType() == WeaponType.LaserSniper) lookSensitivity = lookSensitivitySniper;
	    gunHolder.position = Vector3.MoveTowards(gunHolder.position, gunPointAimDownSights.position, speedAimingDownSights * Time.deltaTime);
	} else {
	    gunHolder.position = Vector3.MoveTowards(gunHolder.position, gunPointNormal.position, speedAimingDownSights * 2 * Time.deltaTime);

	}
    }


    public void FreezeMovement() {
	anim.SetBool("isDead", true);
	motor.Freeze();
    }
}

