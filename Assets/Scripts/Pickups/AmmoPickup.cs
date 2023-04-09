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
	    isCollected = true;
	    AudioManager.instance.PlaySFX(3);
	    // Give ammo to the player
	    int ammoAmont = 0;
	    List<Gun> playerGuns = other.GetComponent<PlayerController>().GetAllGuns();
	    foreach (Gun gun in playerGuns) {
		WeaponType currWeaponType = gun.GetWeaponType();
		ammoAmont = GetAmmoAmount(currWeaponType);
		if (currWeaponType == ammoPickupType) {
		    Debug.Log(currWeaponType);
		    gun.IncrementAmmo(ammoAmont);
		    Destroy(gameObject);
		    //break;
		}
	    }
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
