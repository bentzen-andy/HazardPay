using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {

    private bool isInvincible;
    private bool isWaitingToRespawn;
    private bool isDead;

    [SerializeField] private bool godMode;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float invincibleTime = 0.5f;

    public static PlayerHealthController instance;
    

    // Start is called before the first frame update
    void Start() {
        instance = this;
	InitHealth();
	UIController.instance.healthSlider.maxValue = maxHealth;
	UpdateHealthBarText();

    }

    // Update is called once per frame
    void Update() {
	if (currentHealth >= 0 && transform.position.y < -25) {
	  StartCoroutine(PlayerDiesAndWaitToRespawn(2f));
	}

    }


    public int GetCurrentHealth() {
	return currentHealth;
    }


    public bool PlayerHasFullHealth() {
	return currentHealth >= maxHealth;
    }


    public void DamagePlayer(int damageAmount) {
	if (isInvincible) return;
	if (GameManager.instance.levelIsEnding) return;

	if (!godMode) currentHealth -= damageAmount;
	currentHealth = Mathf.Max(currentHealth, 0);
	UpdateHealthBarText();
	UIController.instance.ShowDamage();
	AudioManager.instance.PlaySFX(7);

	StartCoroutine(MakeInvinsibleForSeconds(invincibleTime));
	if (currentHealth <= 0) {
	    UIController.instance.ShowDeath();
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
	AudioManager.instance.StopBackgroundMusic();
	AudioManager.instance.StopSFX(7);
	AudioManager.instance.PlaySFX(6);

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


    private void InitHealth() {
	currentHealth = maxHealth;
	string startingHealth = PlayerPrefs.GetString("playerHealth");
	if (!string.IsNullOrWhiteSpace(startingHealth)) int.TryParse(startingHealth, out currentHealth);
	
	string godModeString = PlayerPrefs.GetString("godMode");
	if (!string.IsNullOrWhiteSpace(godModeString)) {
	    if (godModeString == "godModeYes") {
		godMode = true;
	    }
	} else {
	    godMode = false;
	}
    }
}
