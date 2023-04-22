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

    public Image blackScreen;
    public float blackScreenFadeTime = 1.5f;

    public Image whiteScreen;
    public float whiteScreenFadeTime = 0.1f;

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

	// fade the screen out if the player gets killed
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

	// fade the screen in at the start of the level
	float blackScreenAlpha;
	float whiteScreenAlpha;

	if (!GameManager.instance.levelIsEnding) {
	    blackScreenAlpha = Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFadeTime * Time.deltaTime);
	    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreenAlpha);
	    whiteScreen.color = new Color(whiteScreen.color.r, whiteScreen.color.g, whiteScreen.color.b, 0f);
	}

	// fade the screen to black at the end of a level
	if (GameManager.instance.levelIsEnding) {
	    blackScreenAlpha = Mathf.MoveTowards(blackScreen.color.a, 1f, blackScreenFadeTime * Time.deltaTime);
	    blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreenAlpha);

	    whiteScreenAlpha = Mathf.MoveTowards(whiteScreen.color.a, 1f, whiteScreenFadeTime * Time.deltaTime);
	    whiteScreen.color = new Color(whiteScreen.color.r, whiteScreen.color.g, whiteScreen.color.b, whiteScreenAlpha);
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
