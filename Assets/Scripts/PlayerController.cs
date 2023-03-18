using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public CharacterController charCon;
    private Vector3 moveInput;
    public Transform camTrans;
    public float mouseSensitivity = 1.0f;
    public bool invertX;
    public bool invertY;

    void Update() {
	//moveInput.x = Input.GetAxis("Horizontal")*moveSpeed*Time.deltaTime;
	//moveInput.z = Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;

	Vector3 vertMove = transform.forward*Input.GetAxis("Vertical");
	Vector3 horiMove = transform.right*Input.GetAxis("Horizontal");

	moveInput = horiMove + vertMove;
	//moveInput.Normalize(); // commenting this out becaues it's causing lag when up-pressing a movement key (WASD).
	moveInput = moveInput*moveSpeed;

	charCon.Move(moveInput*Time.deltaTime);

	// control camera rotation
	Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))*mouseSensitivity;
	if (invertX) mouseInput.x = -mouseInput.x;
	if (invertY) mouseInput.y = -mouseInput.y;

	transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
					      transform.rotation.eulerAngles.y + mouseInput.x,
					      transform.rotation.eulerAngles.z);
	camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
    
}

