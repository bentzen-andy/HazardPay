using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private bool isChasing;
    private bool isFighting;
    private bool isReloading;
    private bool isAlerted;
    private bool isPausingToShoot;
    private Vector3 spawnPos;
    private int numRoundsInMagazine = 6;

    [SerializeField] private float distanceToFight = 5f;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private float distanceToReturnToBase = 15f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxMagazineSize = 6;


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
	else if (!isChasing) Unalert();

	if (isChasing && distToPlayer > distanceToReturnToBase) isChasing = false;
	else if (!isChasing && distToPlayer < distanceToChase) isChasing = true;
	else if (distToPlayer < distanceToFight) isFighting = true;
	else isFighting = false;
    }


    private void ChasePlayer(Vector3 playerPos) {
	Alert();
	if (!isPausingToShoot) agent.destination = playerPos;
	Shoot(playerPos);
    }


    private void FightPlayer(Vector3 playerPos) {
	Alert();
	agent.destination = transform.position;
	Shoot(playerPos);
    }


    private void Alert() {
	if (!isAlerted) StartCoroutine(ReloadNewRound());
	isAlerted = true;
    }


    private void Unalert() {
	isAlerted = false;
	if (!isPausingToShoot) agent.destination = spawnPos;
    }


    private void Shoot(Vector3 playerPos) {
	// TODO: improve this by checking a ray trace before firing.
	if (isReloading) return;
	if (numRoundsInMagazine <= 0) return;

	StartCoroutine(PauseToShoot());
        transform.LookAt(playerPos);
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	numRoundsInMagazine--;

	if (numRoundsInMagazine <= 0) StartCoroutine(ReloadNewMagazine());
	else StartCoroutine(ReloadNewRound());
    }


    private IEnumerator PauseToShoot() {
	isPausingToShoot = true;
	agent.destination = transform.position;
	float rand = Random.Range(0.75f, 1.0f);
	yield return new WaitForSeconds(rand);
	isPausingToShoot = false;
    }


    private IEnumerator ReloadNewRound() {
	isReloading = true;
	float rand = Random.Range(0.15f, 0.2f);
	yield return new WaitForSeconds(rand);
	isReloading = false;
    }


    private IEnumerator ReloadNewMagazine() {
	float rand = Random.Range(3.0f, 5.0f);
	yield return new WaitForSeconds(rand);
	numRoundsInMagazine = maxMagazineSize;
    }

}
