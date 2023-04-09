using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    private bool isCollected;

    [SerializeField] private int healAmount;


    private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player" && !isCollected) {
	    isCollected = true;
	    AudioManager.instance.PlaySFX(5);
	    PlayerHealthController.instance.HealPlayer(healAmount);
	    Destroy(gameObject);
	}
    }
}
