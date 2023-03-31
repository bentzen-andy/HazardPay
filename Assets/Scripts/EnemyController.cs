using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private bool isChasing;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private float distanceToStopChase = 15f;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
	Vector3 playerPos = PlayerController.instance.transform.position;
	float distToPlayer = Vector3.Distance(transform.position, playerPos);
	
	if (isChasing) ChasePlayer(playerPos);
	if (!isChasing) rb.velocity = Vector2.zero;

	if       (isChasing && distToPlayer > distanceToStopChase) isChasing = false;
	else if (!isChasing && distToPlayer < distanceToChase) isChasing = true;
    }



    private void ChasePlayer(Vector3 playerPos) {
	transform.LookAt(playerPos);
	Vector3 velocity = transform.forward * moveSpeed;
	// only move horizontally 
	velocity.y = 0f;
	rb.velocity = velocity;
    }
}
