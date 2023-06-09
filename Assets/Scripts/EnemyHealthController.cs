using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    public bool isBoss;
    public Collider head;
    public Collider body;
    

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
	
	if (currentHealth <= 0) Death();
        
    }


    public void DamageEnemy(int damageAmount) {
	currentHealth -= damageAmount;
    }


    public void DamageEnemyHeadshot(int damageAmount) {
	currentHealth -= damageAmount*2;
    }


    private void Death() {
	if (isBoss) BossFight.instance.numBossesDefeated++;
	AudioManager.instance.PlaySFX(2);
	Destroy(gameObject);
    }


    public bool EnemyDidTakeDamage() {
	return currentHealth < maxHealth;
    }
}
