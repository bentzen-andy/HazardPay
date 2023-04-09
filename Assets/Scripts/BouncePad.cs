using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour {

    [SerializeField] private float bounceForce = 16000f;

    private void OnTriggerEnter(Collider other) {
	if (other.tag == "Player") {
	    PlayerController.instance.Bounce(bounceForce);
	}
    }



    private void OnTriggerExit(Collider other) {
	if (other.tag == "Player") {
	    PlayerController.instance.EndBounce(0f);
	}
    }
}
