using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool canAutoFire;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int currentAmmo;

    private float timeUntilReadyToFire;
    private bool canFire => (timeUntilReadyToFire <= 0f && currentAmmo > 0);


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        timeUntilReadyToFire -= Time.deltaTime;
    }


    public void ShootSingleRound(Camera playerCamera) {
	if (!canFire) return;
	ShootRound(playerCamera);
    }


    public void ShootFullyAutomatic(Camera playerCamera) {
	if (!canFire) return;
	if (!canAutoFire) return;
	ShootRound(playerCamera);
    }


    private void ShootRound(Camera playerCamera) {
	RaycastHit hit;
	if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 50.0f)) {
	    if (Vector3.Distance(playerCamera.transform.position, hit.point) > 0.1f) {
		firePoint.LookAt(hit.point);
	    }
	} else {
	    firePoint.LookAt(playerCamera.transform.position + (playerCamera.transform.forward*30.0f));
	}

	// Fire a round
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	currentAmmo -= 1;
	timeUntilReadyToFire = timeBetweenShots;
    }
}
