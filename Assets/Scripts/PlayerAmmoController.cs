using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// UpdateAmmoBarText
public class PlayerAmmoController : MonoBehaviour {

    [SerializeField] private int pistolAmmo;
    [SerializeField] private int repeaterAmmo;
    [SerializeField] private int sniperAmmo;
    [SerializeField] private int rocketAmmo;

    [SerializeField] private int pistolAmmoMax = 100;
    [SerializeField] private int repeaterAmmoMax = 200;
    [SerializeField] private int sniperAmmoMax = 40;
    [SerializeField] private int rocketAmmoMax = 20;

    [SerializeField] private int pistolAmmoStarting = 30;
    [SerializeField] private int repeaterAmmoStarting = 50;
    [SerializeField] private int sniperAmmoStarting = 10;
    [SerializeField] private int rocketAmmoStarting = 2;

    private WeaponType activeWeaponType;
    private int activeGunAmmo;

    public static PlayerAmmoController instance;


    // Start is called before the first frame update
    void Start() {
	instance = this;
	InitAmmo();
	UpdateAmmoBarText();
    }


    private void InitAmmo() {
	pistolAmmo = pistolAmmoStarting;
	repeaterAmmo = repeaterAmmoStarting;
	sniperAmmo = sniperAmmoStarting;
	rocketAmmo = rocketAmmoStarting;
    }


    public int GetActiveGunAmmo() {
	int res = 0;
	WeaponType wt = PlayerController.instance.GetActiveWeaponType();
	res = GetCurrentAmmo(wt);
	return res;
    }


    public int GetActiveGunAmmoMax() {
	int res = 0;
	WeaponType wt = PlayerController.instance.GetActiveWeaponType();
	res = GetMaxAmmo(wt);
	return res;
    }


    public int GetCurrentAmmo(WeaponType wt) {
	int res = 0;
	if (wt == WeaponType.LaserPistol) res = pistolAmmo;
	else if (wt == WeaponType.LaserRepeater) res = repeaterAmmo;
	else if (wt == WeaponType.LaserSniper) res = sniperAmmo;
	else if (wt == WeaponType.RocketLauncher) res = rocketAmmo;
	return res;
    }


    public int GetStartingAmmo(WeaponType wt) {
	int res = 0;
	if (wt == WeaponType.LaserPistol) res = pistolAmmoStarting;
	else if (wt == WeaponType.LaserRepeater) res = repeaterAmmoStarting;
	else if (wt == WeaponType.LaserSniper) res = sniperAmmoStarting;
	else if (wt == WeaponType.RocketLauncher) res = rocketAmmoStarting;
	return res;
    }


    public int GetMaxAmmo(WeaponType wt) {
	int res = 0;
	if (wt == WeaponType.LaserPistol) res = pistolAmmoMax;
	else if (wt == WeaponType.LaserRepeater) res = repeaterAmmoMax;
	else if (wt == WeaponType.LaserSniper) res = sniperAmmoMax;
	else if (wt == WeaponType.RocketLauncher) res = rocketAmmoMax;
	return res;
    }


    public bool AmmoIsAtMax(WeaponType wt) {
	bool res = false;
	if (wt == WeaponType.LaserPistol) res = pistolAmmo >= pistolAmmoMax;
	else if (wt == WeaponType.LaserRepeater) res = repeaterAmmo >= repeaterAmmoMax;
	else if (wt == WeaponType.LaserSniper) res = sniperAmmo >= sniperAmmoMax;
	else if (wt == WeaponType.RocketLauncher) res = rocketAmmo >= rocketAmmoMax;
	return res;
    }


    public void IncrementAmmo(WeaponType wt, int numRounds) {
	Debug.Log("incrementing ammo.......................");

	if (wt == WeaponType.LaserPistol) pistolAmmo += numRounds;
	else if (wt == WeaponType.LaserRepeater) repeaterAmmo += numRounds;
	else if (wt == WeaponType.LaserSniper) sniperAmmo += numRounds;
	else if (wt == WeaponType.RocketLauncher) rocketAmmo += numRounds;
	UpdateAmmoBarText();
    }


    public void DecrementAmmo(WeaponType wt, int numRounds) {
	if (wt == WeaponType.LaserPistol) pistolAmmo -= numRounds;
	else if (wt == WeaponType.LaserRepeater) repeaterAmmo -= numRounds;
	else if (wt == WeaponType.LaserSniper) sniperAmmo -= numRounds;
	else if (wt == WeaponType.RocketLauncher) rocketAmmo -= numRounds;
	UpdateAmmoBarText();
    }


    private void ValidateAmmo() {
	if (pistolAmmo > pistolAmmoMax) pistolAmmo = pistolAmmoMax;
	if (repeaterAmmo > repeaterAmmoMax) repeaterAmmo = repeaterAmmoMax;
	if (sniperAmmo > sniperAmmoMax) sniperAmmo = sniperAmmoMax;
	if (rocketAmmo > rocketAmmoMax) rocketAmmo = rocketAmmoMax;

	if (pistolAmmo < 0) pistolAmmo = 0;
	if (repeaterAmmo < 0) repeaterAmmo = 0;
	if (sniperAmmo < 0) sniperAmmo = 0;
	if (rocketAmmo < 0) rocketAmmo = 0;
    }


    public void UpdateAmmoBarText() {
	// Don't update the UI text unless the change pertains to the players current weapon.
	ValidateAmmo();
	int ammo = GetActiveGunAmmo();
	int maxAmmo = GetActiveGunAmmoMax();
	UIController.instance.ammoText.text = $"AMMO: {ammo}/{maxAmmo}";
    }
}
