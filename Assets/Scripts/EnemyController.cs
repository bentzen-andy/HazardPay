using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private bool isChasing;
    private bool isFighting;
    private bool isReloading;
    private Vector3 spawnPos;

    [SerializeField] private float distanceToFight = 5f;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private float distanceToReturnToBase = 15f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;


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

	if (isChasing && distToPlayer > distanceToReturnToBase) isChasing = false;
	else if (!isChasing && distToPlayer < distanceToChase) isChasing = true;

	if (distToPlayer < distanceToFight) isFighting = true;
	else isFighting = false;
    }


    private void ChasePlayer(Vector3 playerPos) {
	agent.destination = playerPos;
	if (!isReloading) Shoot();
    }


    private void FightPlayer(Vector3 playerPos) {
	agent.destination = transform.position;
        transform.LookAt(playerPos);
	if (!isReloading) Shoot();
    }


    private void Shoot() {
	// TODO: improve this by checking a ray trace before firing.
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	isReloading = true;
	StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
	yield return new WaitForSeconds(1f);
	isReloading = false;
    }

}
