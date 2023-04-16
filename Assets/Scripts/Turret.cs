using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private float rangeToTargetPlayer = 30f;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;

    private float shotCounter;
    private bool canShootBarrel2;


    // Update is called once per frame
    void Update() {
	//Debug.Log(PlayerIsInLineOfSight());

	if (PlayerIsWithinRange()) {
	    Shoot();
	} else {
	    gun.rotation = Quaternion.Lerp(gun.rotation,
					     Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 10f, 0f),
					     rotateSpeed * Time.deltaTime);
	}
    }
    

    private bool PlayerIsWithinRange() {
	return Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer;
    }


    private bool PlayerIsInLineOfSight() {
	// Check if the player is in the enemy's line of sight
	bool playerIsInLineOfSight = false;
	RaycastHit hit;
	if (Physics.Raycast(firePoint1.position, firePoint1.forward, out hit, rangeToTargetPlayer) &&
	    hit.collider.gameObject == PlayerController.instance.gameObject) {
	    transform.LookAt(hit.point);
	    playerIsInLineOfSight = true;
	}

	if (Physics.Raycast(firePoint2.position, firePoint2.forward, out hit, rangeToTargetPlayer) &&
	    hit.collider.gameObject == PlayerController.instance.gameObject) {
	    transform.LookAt(hit.point);
	    playerIsInLineOfSight = true;
	}

	return playerIsInLineOfSight;
    }


    private void Shoot() {
	if (GameManager.instance.levelIsEnding) return;

	gun.LookAt(PlayerController.instance.transform.position);
	if (!PlayerIsInLineOfSight()) return;
	shotCounter -= Time.deltaTime;
	ShootBarrel1();
	ShootBarrel2();
    }


    private void ShootBarrel1() {
	if (shotCounter > 0) return;
	Instantiate(projectile, firePoint1.position, firePoint1.rotation);
	canShootBarrel2 = true;
	shotCounter = timeBetweenShots;
    }


    private void ShootBarrel2() {
	if (shotCounter > timeBetweenShots/2) return;
	if (!canShootBarrel2) return;
	Instantiate(projectile, firePoint2.position, firePoint2.rotation);
	canShootBarrel2 = false;
    }
}
