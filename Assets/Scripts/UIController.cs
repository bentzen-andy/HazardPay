using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Slider healthSlider;
    public Text healthText;
    public Text ammoText;

    public static UIController instance;

    // Start is called before the first frame update
    private void Awake() {
	instance = this;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
