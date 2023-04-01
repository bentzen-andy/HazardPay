using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour {

    [SerializeField] private int currentHealth = 100;
    public CapsuleCollider head;
    public CapsuleCollider body;
    

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
	
	if (currentHealth <= 0) Destroy(gameObject);
        
    }


    public void DamageEnemy(int damageAmount) {
	currentHealth -= damageAmount;
    }


    public void DamageEnemyHeadshot(int damageAmount) {
	currentHealth -= damageAmount*2;
    }
}
