using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private bool isChasing;
    private bool isFighting;
    private Vector3 spawnPos;
    [SerializeField] private float distanceToFight = 5f;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private float distanceToStopChase = 15f;
    [SerializeField] private NavMeshAgent agent;


    private void Awake() {
	spawnPos = transform.position;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
	Vector3 playerPos = PlayerController.instance.transform.position;
	float distToPlayer = Vector3.Distance(transform.position, playerPos);

	if (isFighting) FightPlayer(playerPos);
	else if (isChasing) ChasePlayer(playerPos);
	else if (!isChasing) agent.destination = spawnPos;

	if       (isChasing && distToPlayer > distanceToStopChase) isChasing = false;
	else if (!isChasing && distToPlayer < distanceToChase) isChasing = true;

	if (distToPlayer < distanceToFight) isFighting = true;
	else isFighting = false;
    }



    private void ChasePlayer(Vector3 playerPos) {
	agent.destination = playerPos;
    }

    private void FightPlayer(Vector3 playerPos) {
	agent.destination = transform.position;
    }
}
