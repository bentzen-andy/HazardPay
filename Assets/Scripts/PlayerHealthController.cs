using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {

    private bool isInvincible;
    private bool isWaitingToRespawn;
    private bool isDead;

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibleTime = 0.5f;

    public static PlayerHealthController instance;
    

    // Start is called before the first frame update
    void Start() {
        instance = this;
	currentHealth = maxHealth;
	UIController.instance.healthSlider.maxValue = maxHealth;
	UpdateHealthBarText();

    }

    // Update is called once per frame
    void Update() {
    }

    public void DamagePlayer(int damageAmount) {
	if (isInvincible) return;
	currentHealth -= damageAmount;
	currentHealth = Mathf.Max(currentHealth, 0);
	UpdateHealthBarText();

	StartCoroutine(MakeInvinsibleForSeconds(invincibleTime));
	if (currentHealth <= 0) {
	    StartCoroutine(PlayerDiesAndWaitToRespawn(4f));
	}
    }


    public void HealPlayer(int healAmount) {
	currentHealth += healAmount;
	currentHealth = Mathf.Min(currentHealth, maxHealth);
	UpdateHealthBarText();
    }


    private IEnumerator PlayerDiesAndWaitToRespawn(float seconds) {
	if (isWaitingToRespawn) yield break;
	isDead = true;
        isWaitingToRespawn = true;
	PlayerController.instance.FreezeMovement();
	yield return new WaitForSeconds(seconds);

	//gameObject.SetActive(false);
	GameManager.instance.ReloadScene();
        isWaitingToRespawn = false;
    }


    private IEnumerator MakeInvinsibleForSeconds(float seconds) {
	if (isInvincible) yield break;
        isInvincible = true;
	yield return new WaitForSeconds(seconds);
        isInvincible = false;
    }

    private void UpdateHealthBarText() {
	int health = Mathf.Max(currentHealth, 0);
	UIController.instance.healthSlider.value = health;
	UIController.instance.healthText.text = $"HEALTH: {health}/{maxHealth}";
    }

    public bool PlayerIsDead() {
	return isDead;
    }

}
