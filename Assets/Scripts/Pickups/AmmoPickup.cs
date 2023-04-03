using System.Collections;
using System.Collections.Generic;
using UnityEngine;
				       
public enum AmmoPickupSize {Small, Large};

public class AmmoPickup : MonoBehaviour {

    //enum WeaponType {Pistol, MachineGun, SniperRifle, RocketLauncher};

    private bool isCollected;
			     
    [SerializeField] private int baseAmmoAmount = 8;
    [SerializeField] private AmmoPickupSize ammoPickupSize = AmmoPickupSize.Small;
    [SerializeField] private WeaponType ammoPickupType = WeaponType.Pistol;


    private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player" && !isCollected) {
	    isCollected = true;
	    // Give ammo to the player
	    // FIXME change this so that it woks on an arry of guns that the player is carrying, rather
	    // than just the active gun.
	    Gun playerGun = other.GetComponent<PlayerController>().GetActiveGun();
	    WeaponType playerWeaponType = playerGun.GetWeaponType();

	    int ammoAmount = GetAmmoAmount(playerWeaponType);
	    if (playerWeaponType == ammoPickupType) {
		playerGun.IncrementAmmo(ammoAmount);
		
	    }
	    
	    Destroy(gameObject);
	}
    }


    private int GetAmmoAmount(WeaponType weaponType) {
	int result = 0;
	if (weaponType == WeaponType.Pistol) result = baseAmmoAmount;
	else if (weaponType == WeaponType.MachineGun) result = baseAmmoAmount*2*2;
	else if (weaponType == WeaponType.SniperRifle) result = baseAmmoAmount/2;
	else if (weaponType == WeaponType.RocketLauncher) result = baseAmmoAmount/2/2;

	if (ammoPickupSize == AmmoPickupSize.Large) result *= 4;

	return result;
    }
}
