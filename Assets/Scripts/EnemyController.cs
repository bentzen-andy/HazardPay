using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private bool isPausingBetweenShots;
    private bool isPausingToReload;
    private bool isStandingStill;
    private bool isCloseToPlayer;
    private bool isInCombat;
    private Vector3 spawnPos;
    private int numRoundsInMagazine = 0;

    [SerializeField] private float distanceToFight = 5f;
    [SerializeField] private float distanceToChase = 10f;
    [SerializeField] private float distanceToReturnToBase = 15f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxMagazineSize = 2;
    [SerializeField] private float pauseTimeToShoot = 0.3f;
    [SerializeField] private float pauseTimeToReload = 6.0f;


    private void Awake() {
	spawnPos = transform.position;
    }


    // Update is called once per frame
    void Update() {
	Vector3 playerPos = PlayerController.instance.transform.position;
	float distToPlayer = Vector3.Distance(transform.position, playerPos);

	Move(playerPos);
	bool enemyDidShoot = Shoot(playerPos);
	Reload();
	SetEnemyState(distToPlayer, enemyDidShoot);
    }


    private void Move(Vector3 playerPos) {
	if (isStandingStill || isCloseToPlayer) agent.destination = transform.position;
	else if (isInCombat) agent.destination = playerPos;
	else agent.destination = spawnPos;
    }


    private bool Shoot(Vector3 playerPos) {
	if (isPausingBetweenShots) return false;
	if (isPausingToReload) return false;
	if (numRoundsInMagazine <= 0) return false;

        transform.LookAt(playerPos);
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	numRoundsInMagazine--;
	return true;
    }


    private void Reload() {
	if (isPausingToReload) return;
	if (numRoundsInMagazine <= 0) StartCoroutine(PauseToReload());
    }


    private void SetEnemyState(float distToPlayer, bool enemyDidShoot) {
	// Set state based on distance from the player
	if (isInCombat && distToPlayer > distanceToReturnToBase) isInCombat = false;
	else if (!isInCombat && distToPlayer < distanceToChase) isInCombat = true;
	else if (distToPlayer < distanceToFight) isCloseToPlayer = true;
	else if (distToPlayer > distanceToFight) isCloseToPlayer = false;

	// Set state based on whether enemy did shoot in this frame
	if (enemyDidShoot) StartCoroutine(StandStillToShoot());
	if (enemyDidShoot) StartCoroutine(PauseBetweenShots());
    }


    private IEnumerator StandStillToShoot() {
	isStandingStill = true;
	float rand = Random.Range(0.75f, 1.0f);
	yield return new WaitForSeconds(rand);
	isStandingStill = false;
    }


    private IEnumerator PauseBetweenShots() {
	isPausingBetweenShots = true;
	float rand = Random.Range(pauseTimeToShoot - pauseTimeToShoot/4,
				  pauseTimeToShoot + pauseTimeToShoot/4);
	yield return new WaitForSeconds(rand);
	isPausingBetweenShots = false;
    }


    private IEnumerator PauseToReload() {
	isPausingToReload = true;
	float rand = Random.Range(pauseTimeToReload - pauseTimeToReload/4,
				  pauseTimeToReload + pauseTimeToReload/4);
	yield return new WaitForSeconds(rand);
	isPausingToReload = false;
	numRoundsInMagazine = maxMagazineSize;
    }
}
