using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    [SerializeField] private int healAmount;


    private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player") {
	    PlayerHealthController.instance.HealPlayer(healAmount);
	    Destroy(gameObject);
	}
    }
}
