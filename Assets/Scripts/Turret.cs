using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private float rangeToTargetPlayer = 0.5f;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform firePoint;

    private float shotCounter;


    // Start is called before the first frame update
    void Start() {
	shotCounter = timeBetweenShots;
    }
    
    // Update is called once per frame
    void Update() {
	if (PlayerIsWithinRange()) {
	    gun.LookAt(PlayerController.instance.transform.position);
	    shotCounter -= Time.deltaTime;
	    if (shotCounter <= 0) Shoot();
	} else {
	    shotCounter = timeBetweenShots;
	    gun.rotation = Quaternion.Lerp(gun.rotation,
					     Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 10f, 0f),
					     rotateSpeed * Time.deltaTime);
	}
    }
    

    private bool PlayerIsWithinRange() {
	return Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToTargetPlayer;
    }


    private void Shoot() {
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	shotCounter = timeBetweenShots;
    }
}
