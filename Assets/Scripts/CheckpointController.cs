using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour {

    [SerializeField] string checkpointName;

    private string checkpointKey => ($"{SceneManager.GetActiveScene().name}_checkpoint");

    
    // Start is called before the first frame update
    void Start() {
	if (currentCheckpointIsMostRecentCp()) {
	    spawnPlayerAtMostRecentCheckpoint();
	}
    }
    

    // Update is called once per frame
    void Update() {
	if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.L)) {
	    PlayerPrefs.SetString(checkpointKey, "");
	    //PlayerPrefs.DeleteAll();
	}
    }


    private void OnTriggerEnter(Collider other) {
	if (other.gameObject.tag == "Player") {
	    PlayerPrefs.SetString(checkpointKey, checkpointName);
	    AudioManager.instance.PlaySFX(1);
	    StoreAmmo();
	    StoreGuns();
	    StoreHealth();
	}
    }


    private bool currentCheckpointIsMostRecentCp() {
	if (PlayerPrefs.HasKey(checkpointKey)) {
	    if (PlayerPrefs.GetString(checkpointKey) == checkpointName) {
		return true;
	    }
	}
	return false;
    }


    private void spawnPlayerAtMostRecentCheckpoint() {
	PlayerController.instance.transform.position = transform.position;
	Debug.Log("player starting at " + checkpointName);
    }



    private void StoreAmmo() {
	int pistolAmmo = PlayerAmmoController.instance.GetCurrentAmmo(WeaponType.LaserPistol);
	int repeaterAmmo = PlayerAmmoController.instance.GetCurrentAmmo(WeaponType.LaserRepeater);
	int sniperAmmo = PlayerAmmoController.instance.GetCurrentAmmo(WeaponType.LaserSniper);
	int rocketAmmo = PlayerAmmoController.instance.GetCurrentAmmo(WeaponType.RocketLauncher);
	PlayerPrefs.SetString("pistolAmmo", $"{pistolAmmo}");
	PlayerPrefs.SetString("repeaterAmmo", $"{repeaterAmmo}");
	PlayerPrefs.SetString("sniperAmmo", $"{sniperAmmo}");
	PlayerPrefs.SetString("rocketAmmo", $"{rocketAmmo}");
    }


    private void StoreGuns() {
	// store active gun
	WeaponType wt = PlayerController.instance.GetActiveGun().GetWeaponType();
	if (wt == WeaponType.LaserPistol) PlayerPrefs.SetString("activeGun", "pistolGunActive");
	if (wt == WeaponType.LaserRepeater) PlayerPrefs.SetString("activeGun", "repeaterGunActive");
	if (wt == WeaponType.LaserSniper) PlayerPrefs.SetString("activeGun", "sniperGunActive");
	if (wt == WeaponType.RocketLauncher) PlayerPrefs.SetString("activeGun", "rocketGunActive");

	// store all guns
	foreach (Gun gun in PlayerController.instance.GetAllCollectedGuns()) {
	    WeaponType wType = gun.GetWeaponType();
	    if (wType == WeaponType.LaserPistol) PlayerPrefs.SetString("pistolGun", "pistolGun");
	    else if (wType == WeaponType.LaserRepeater) PlayerPrefs.SetString("repeaterGun", "repeaterGun");
	    else if (wType == WeaponType.LaserSniper) PlayerPrefs.SetString("sniperGun", "sniperGun");
	    else if (wType == WeaponType.RocketLauncher) PlayerPrefs.SetString("rocketGun", "rocketGun");
	}
    }


    private void StoreHealth() {
	int playerHealth = PlayerHealthController.instance.GetCurrentHealth();
	PlayerPrefs.SetString("playerHealth", $"{playerHealth}");
    }


}
