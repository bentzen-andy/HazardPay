using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    [SerializeField] private int baseBulletDamage = 200;
    [SerializeField] private bool allowedToDamagePlayer;
    [SerializeField] private bool allowedToDamageEnemy;


    // Start is called before the first frame update
    void Start() {
        
    }


    // Update is called once per frame
    void Update() {
        
    }


    private void OnTriggerEnter(Collider other) {
	if (allowedToDamageEnemy && other.gameObject.tag == "Enemy") {
	    GameObject obj = other.gameObject;
	    EnemyHealthController controller = obj.GetComponent<EnemyHealthController>();

											   
	    //if (other == controller.head) controller.DamageEnemyHeadshot(baseBulletDamage);
	    if (other == controller.body) controller.DamageEnemy(baseBulletDamage);
	    else controller.DamageEnemy(baseBulletDamage);
	}

	if (allowedToDamagePlayer && other.gameObject.tag == "Player") {
	    PlayerHealthController.instance.DamagePlayer(baseBulletDamage);
	}

	//Destroy(gameObject);

	//Instantiate(impactEffect,
		    //transform.position + transform.forward*(-moveSpeed*Time.deltaTime),
		    //transform.rotation);
    }
}
