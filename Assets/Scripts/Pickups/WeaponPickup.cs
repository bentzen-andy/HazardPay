using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    private bool isCollected;

    [SerializeField] private Gun gunPickup;


    private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player" && !isCollected) {
	    isCollected = true;
	    AudioManager.instance.PlaySFX(4);

	    // Give weapon to the player
	    HandlePickup(other);
	    Destroy(gameObject);
	}
    }
    
    
    private void HandlePickup(Collider other) {
	List<Gun> playerCollectedGuns = other.GetComponent<PlayerController>().GetAllCollectedGuns();

	// check if the player already has this gun
	foreach (Gun playerGun in playerCollectedGuns) {
	    WeaponType currWeaponType = playerGun.GetWeaponType();
	    if (currWeaponType == gunPickup.GetWeaponType()) {
		PlayerAmmoController ammoController = PlayerAmmoController.instance;
		int ammoCount = ammoController.GetStartingAmmo(currWeaponType);
		ammoController.IncrementAmmo(currWeaponType, ammoCount);
		return;
	    }
	}

	// if this is a new weapon
	playerCollectedGuns.Add(gunPickup);

	// switch to the new weapon
	List<Gun> playerGuns = PlayerController.instance.GetAllGuns();
	int gunIndex = playerGuns.IndexOf(gunPickup);
	PlayerController.instance.SwapGun(gunIndex);
    }
}
