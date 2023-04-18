using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private EnemyHealthController enemyHealthController;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
	if (enemyHealthController.EnemyDidTakeDamage()) {
	    Destroy(gameObject);

	    //Instantiate(impactEffect,
			//transform.position + transform.forward*(-moveSpeed*Time.deltaTime),
			//transform.rotation);
	    Instantiate(impactEffect, transform.position, transform.rotation);
	}
    }

}
