using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {LaserPistol, LaserRepeater, LaserSniper, RocketLauncher};

public class Gun : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private bool canAutoFire;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private int maxAmmo;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private float zoomAmount;
    [SerializeField] private int startingAmmo;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private float muzzleFlashTime = 0.025f;
			 
    private bool muzzleFlashIsActive;
    private int currentAmmo;
    private float timeUntilReadyToFire;
    private bool canFire => (timeUntilReadyToFire <= 0f && currentAmmo > 0);

    public static Gun instance;


    // Update is called once per frame
    void Update() {
        timeUntilReadyToFire -= Time.deltaTime;

	// Debuging
	if (Input.GetMouseButtonDown(1)) {
	    //Debug.Log("canFire " + canFire);
	    //Debug.Log("timeUntilReadyToFire " + timeUntilReadyToFire);
	    //Debug.Log("currentAmmo " + currentAmmo);
	    //Debug.Log("weaponType " + weaponType);
	    //Debug.Log("timeBetweenShots " + timeBetweenShots);
	}
    }


    // Start is called before the first frame update
    void Start() {
	instance = this;
	//InitWeapon();
        InitAmmo();
	UpdateAmmoBarText();
	//Debug.Log("init ammo count " + currentAmmo);
    }


    void OnEnable() {
	UpdateAmmoBarText();
    }


    public WeaponType GetWeaponType() {
	return weaponType;
    }


    public int GetStartingAmmo() {
	return startingAmmo;
    }


    public void IncrementAmmo(int numRounds) {
	//Debug.Log("numRounds " + numRounds);
	currentAmmo += numRounds;
	UpdateAmmoBarText();
    }


    public void ShootSingleRound(Camera playerCamera) {
	//Debug.Log("Shooting a round----2");
	//Debug.Log("canFire " + canFire);
	//Debug.Log(currentAmmo);
	//Debug.Log(timeUntilReadyToFire);

	if (!canFire) return;
	//Debug.Log("Shooting a round----3");
	ShootRound(playerCamera);
    }


    public void ShootFullyAutomatic(Camera playerCamera) {
	if (!canFire) return;
	if (!canAutoFire) return;
	ShootRound(playerCamera);
    }


    private void InitAmmo() {
	//Debug.Log(weaponType);
	int result = 0;
	if (weaponType == WeaponType.LaserPistol) result = 30;
	else if (weaponType == WeaponType.LaserRepeater) result = 50;
	else if (weaponType == WeaponType.LaserSniper) result = 10;
	else if (weaponType == WeaponType.RocketLauncher) result = 2;
	//Debug.Log(result);

	currentAmmo = result;
    }


    /* CUT THIS OUT  Mon Apr  3 16:22:30 2023 /andrew_bentzen
    private void InitWeapon() {
	PlayerController.instance.SetActiveGun(this);
    }
    * CUT THIS OUT /andrew_bentzen */


    private void ShootRound(Camera playerCamera) {
	AdjustFirePointToAimAtTargetRetical(playerCamera);

	//Debug.Log("Shooting a round----4");

	// Fire a round
	Instantiate(projectile, firePoint.position, firePoint.rotation);
	//Debug.Log("Shooting a round----5");
	currentAmmo -= 1;
	currentAmmo = Mathf.Max(currentAmmo, 0);
	UpdateAmmoBarText();

	// Muzzle flash
	StartCoroutine(MuzzelFlash(muzzleFlashTime));
	muzzleFlash.SetActive(true);
	
	timeUntilReadyToFire = timeBetweenShots;
    }


    private void AdjustFirePointToAimAtTargetRetical(Camera playerCamera) {
	RaycastHit hit;
	if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100.0f)) {
	    if (Vector3.Distance(playerCamera.transform.position, hit.point) > 0.1f) {
		firePoint.LookAt(hit.point);
	    }
	} else {
	    firePoint.LookAt(playerCamera.transform.position + (playerCamera.transform.forward*30.0f));
	}
    }


    private void UpdateAmmoBarText() {
	// Don't update the UI text unless the change pertains to the players current weapon.
	if (weaponType != PlayerController.instance.GetActiveGun().GetWeaponType()) return;
	int ammo = Mathf.Max(currentAmmo, 0);
	UIController.instance.ammoText.text = $"AMMO: {ammo}/{maxAmmo}";
    }


    public float GetZoom() {
	return zoomAmount;
    }


    private IEnumerator MuzzelFlash(float seconds) {
	if (muzzleFlashIsActive) yield break;
        muzzleFlashIsActive = true;
	muzzleFlash.SetActive(true);
	yield return new WaitForSeconds(seconds);
        muzzleFlashIsActive = false;
	muzzleFlash.SetActive(false);
    }
}
