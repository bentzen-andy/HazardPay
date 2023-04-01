using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private bool isPausingBetweenShots;
    private bool isPausingToReload;
    private bool isStandingStill;
    private bool isInCombat;
    private bool isTurning;
    private bool playerIsInLineOfSight;
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
    [SerializeField] private Animator anim;


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
	SetEnemyState(playerPos, distToPlayer, enemyDidShoot);
	SetAnimState(enemyDidShoot);
    }


    private void Move(Vector3 playerPos) {
	if (isStandingStill) agent.destination = transform.position;
	else if (isInCombat) agent.destination = playerPos;
	else agent.destination = spawnPos;
	//if (!isTurning) TurnDirection(transform.forward, playerPos);
    }


    private bool Shoot(Vector3 playerPos) {
	if (isPausingBetweenShots) return false;
	if (isPausingToReload) return false;
	if (numRoundsInMagazine <= 0) return false;

	// FIXME: change this so that the enemey gradually turns
        transform.LookAt(playerPos);
	if (!playerIsInLineOfSight) return false;

        //firePoint.transform.LookAt(playerPos);
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	numRoundsInMagazine--;
	return true;
    }


    private void Reload() {
	if (isPausingToReload) return;
	if (numRoundsInMagazine <= 0) StartCoroutine(PauseToReload());
    }


    private void SetEnemyState(Vector3 playerPos, float distToPlayer, bool enemyDidShoot) {
	// Set state based on distance from the player
	if (isInCombat && distToPlayer > distanceToReturnToBase) isInCombat = false;
	else if (!isInCombat && distToPlayer < distanceToChase) isInCombat = true;
	else if (distToPlayer < distanceToFight) isStandingStill = true;
	else if (distToPlayer > distanceToFight) isStandingStill = false;

	// Check if the player is in the enemy's line of sight
	RaycastHit hit;
	if (Physics.Raycast(transform.position, transform.forward, out hit, 50.0f) &&
	    hit.collider.gameObject == PlayerController.instance.gameObject) {
	    firePoint.transform.LookAt(hit.point);
	    playerIsInLineOfSight = true;
	} else {
	    playerIsInLineOfSight = false;
	}

	// Set state based on whether enemy did shoot in this frame
	if (enemyDidShoot) StartCoroutine(StandStillToShoot());
	if (enemyDidShoot) StartCoroutine(PauseBetweenShots());
    }


    private void SetAnimState(bool enemyDidShoot) {
	if (enemyDidShoot) anim.SetTrigger("fireShot");
	
	if (agent.isStopped) anim.SetBool("isMoving", false);
	else anim.SetBool("isMoving", true);

	if (agent.remainingDistance < 0.25) anim.SetBool("isMoving", false);
	else anim.SetBool("isMoving", true);

	Debug.Log(agent.remainingDistance);
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


    /* CUT THIS OUT  Sat Apr  1 13:00:22 2023 /andrew_bentzen
    private IEnumerator TurnDirection(Vector3 from, Vector3 to) {
	isTurning = true;
	float elapsed = 0f;
	float duration = 0.25f;
	while (elapsed < duration) {
	    float t = elapsed - duration;
	    // this is a linear interpolation
	    Vector3 newPosToLook = Vector3.Lerp(from, to, t);
	    transform.LookAt(newPosToLook);
	    elapsed += Time.deltaTime;
	    yield return null;
	}
	isTurning = false;
    }
    * CUT THIS OUT /andrew_bentzen */

}
