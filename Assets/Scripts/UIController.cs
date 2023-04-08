using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Slider healthSlider;
    public Text healthText;
    public Text ammoText;
    public Image damageEffect;
    public float damageColorAlpha = 0.25f;
    public float damageColorFadeTime = 2f;
    public float deathColorFadeTime = 0.5f;
    public GameObject pauseScreen;

    public static UIController instance;

    // Start is called before the first frame update
    private void Awake() {
	instance = this;
    }

    // Update is called once per frame
    void Update() {
	// Fade the screen back to normal after taking damage
	float currDamageR;
	float currDamageG;
	float currDamageB;
	float currDamageAlpha;
	Color color;

	if (PlayerHealthController.instance.PlayerIsDead()) {
	    currDamageR = Mathf.MoveTowards(damageEffect.color.r, 0f, deathColorFadeTime/2 * Time.deltaTime);
	    currDamageG = Mathf.MoveTowards(damageEffect.color.g, 0f, deathColorFadeTime/2 * Time.deltaTime);
	    currDamageB = Mathf.MoveTowards(damageEffect.color.b, 0f, deathColorFadeTime/2 * Time.deltaTime);
	    currDamageAlpha = Mathf.MoveTowards(damageEffect.color.a, 1f, deathColorFadeTime * Time.deltaTime);
	    color = damageEffect.color;
	    damageEffect.color = new Color(currDamageR, currDamageG, currDamageB, currDamageAlpha);
	} else {
	    currDamageAlpha = Mathf.MoveTowards(damageEffect.color.a, 0f, damageColorFadeTime * Time.deltaTime);
	    color = damageEffect.color;
	    damageEffect.color = new Color(color.r, color.g, color.b, currDamageAlpha);
	}
    }


    public void ShowDamage() {
	Color color = damageEffect.color;
	damageEffect.color = new Color(color.r, color.g, color.b, damageColorAlpha);
    }


    public void ShowDeath() {
	Debug.Log("death");
    }
}
