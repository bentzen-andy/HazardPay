using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float lifetime = 5.0f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int baseBulletDamage = 20;
    [SerializeField] private bool allowedToDamagePlayer;
    [SerializeField] private bool allowedToDamageEnemy;


    // Start is called before the first frame update
    void Start() {
        
    }


    // Update is called once per frame
    void Update() {
	if (GameManager.instance.levelIsEnding) {
	    Destroy(gameObject);
	    return;
	}
	rb.velocity = transform.forward * moveSpeed;

	// destroy bullets after x seconds
	lifetime -= Time.deltaTime;
	if (lifetime <= 0.0f) Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other) {
	if (!allowedToDamagePlayer && other.gameObject.tag == "Player") return;
	if (!allowedToDamageEnemy && other.gameObject.tag == "Enemy") return;

	if (allowedToDamageEnemy && other.gameObject.tag == "Enemy") {
	    GameObject obj = other.gameObject;
	    EnemyHealthController controller = obj.GetComponent<EnemyHealthController>();
											   
	    if (other == controller.head) controller.DamageEnemyHeadshot(baseBulletDamage);
	    else if (other == controller.body) controller.DamageEnemy(baseBulletDamage);
	    else controller.DamageEnemy(baseBulletDamage);
	}

	if (allowedToDamagePlayer && other.gameObject.tag == "Player") {
	    PlayerHealthController.instance.DamagePlayer(baseBulletDamage);
	}
			    
	Destroy(gameObject);
			    
	Instantiate(impactEffect,
		    //transform.position + transform.forward*(-moveSpeed*Time.deltaTime),
		    other.ClosestPointOnBounds(transform.position),
		    transform.rotation);
    }

}
