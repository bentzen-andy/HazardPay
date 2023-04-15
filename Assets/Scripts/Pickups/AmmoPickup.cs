using System.Collections;
using System.Collections.Generic;
using UnityEngine;
				       
public enum AmmoPickupSize {Small, Large};

public class AmmoPickup : MonoBehaviour {

    //enum WeaponType {Pistol, MachineGun, SniperRifle, RocketLauncher};

    private bool isCollected;
			     
    [SerializeField] private int baseAmmoAmount = 8;
    [SerializeField] private AmmoPickupSize ammoPickupSize = AmmoPickupSize.Small;
    [SerializeField] private WeaponType ammoPickupType = WeaponType.LaserPistol;


    private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player" && !isCollected) {
	    // Give ammo to the player
	    int ammoAmont = GetAmmoAmount(ammoPickupType);
	    Debug.Log("ammoAmont " + ammoAmont);
	    Debug.Log("ammoPickupType " + ammoPickupType);
	    if (PlayerAmmoController.instance.AmmoIsAtMax(ammoPickupType)) return;
	    Debug.Log("ammo is not at max before picking this up.......");

	    PlayerAmmoController.instance.IncrementAmmo(ammoPickupType, ammoAmont);
	    Destroy(gameObject);
	    isCollected = true;
	    AudioManager.instance.PlaySFX(3);
	    




	    /* CUT THIS OUT  Sat Apr 15 15:45:41 2023 /andrew_bentzen
	    List<Gun> playerGuns = other.GetComponent<PlayerController>().GetAllGuns();
	    foreach (Gun gun in playerGuns) {
		WeaponType currWeaponType = gun.GetWeaponType();
		ammoAmont = GetAmmoAmount(currWeaponType);
		if (currWeaponType == ammoPickupType) {
		    Debug.Log(currWeaponType);
		    Debug.Log("ammoAmont " + ammoAmont);
		    if (PlayerAmmoController.instance.AmmoIsAtMax(currWeaponType)) return;
		    PlayerAmmoController.instance.IncrementAmmo(currWeaponType, ammoAmont);
		    Destroy(gameObject);
		    isCollected = true;
		    AudioManager.instance.PlaySFX(3);
		    //break;
		}
	    }
	    * CUT THIS OUT /andrew_bentzen */


	}
    }


    private int GetAmmoAmount(WeaponType weaponType) {
	int result = 0;
	if (weaponType == WeaponType.LaserPistol) result = baseAmmoAmount;
	else if (weaponType == WeaponType.LaserRepeater) result = baseAmmoAmount*2*2;
	else if (weaponType == WeaponType.LaserSniper) result = baseAmmoAmount/2;
	else if (weaponType == WeaponType.RocketLauncher) result = baseAmmoAmount/2/2;

	if (ammoPickupSize == AmmoPickupSize.Large) result *= 4;

	return result;
    }
}
