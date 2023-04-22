using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

    [SerializeField] private GameObject impactEffect;
    //[SerializeField] private EnemyHealthController enemyHealthController;

    private bool barrelIsDestroyed = false;
    private bool barrelIsExploding = false;


    // Start is called before the first frame update
    void Start() {
        
    }

    
    // Update is called once per frame
    void OnTriggerEnter(Collider other) {
	if (barrelIsDestroyed) return;
	if (barrelIsExploding) return;
	if (other.gameObject.layer != LayerMask.NameToLayer("Bullets")) return;
	//if (!enemyHealthController.EnemyDidTakeDamage()) return;
	
	barrelIsDestroyed = true;
	StartCoroutine(PauseBeforeExploding(0.1f));
    }
    
    
    private IEnumerator PauseBeforeExploding(float seconds) {
	barrelIsExploding = true;
	yield return new WaitForSeconds(seconds);
	barrelIsExploding = false;
	
	Instantiate(impactEffect, transform.position, Quaternion.identity);
	Destroy(gameObject);
    }
}
