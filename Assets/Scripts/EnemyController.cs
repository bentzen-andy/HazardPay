using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(PlayerController.instance.gameObject.transform.position);
	rb.velocity = transform.forward * moveSpeed;
    }
}
