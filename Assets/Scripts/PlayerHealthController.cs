using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {

    private bool isInvincible;
    
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    public static PlayerHealthController instance;
    

    // Start is called before the first frame update
    void Start() {
        instance = this;
	currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
	//Debug.Log(isInvincible);
    }

    public void DamagePlayer(int damageAmount) {
	if (isInvincible) return;
	currentHealth  -= damageAmount;
	if (currentHealth <= 0) gameObject.SetActive(false);
	Debug.Log("before");
	StartCoroutine(MakeInvinsibleForSeconds(1f));
	Debug.Log("after");
    }


    private IEnumerator MakeInvinsibleForSeconds(float seconds) {
	Debug.Log("durring");
	Debug.Log(isInvincible);
	if (isInvincible) yield break;
        isInvincible = true;
	Debug.Log(isInvincible);
	yield return new WaitForSeconds(seconds);
        isInvincible = false;
	Debug.Log(isInvincible);
    }

}
