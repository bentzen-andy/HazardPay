using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public int numBossesDefeated;
    public int numGoalsReached;

    public static BossFight instance;


    // Start is called before the first frame update
    void Start() {
	instance = this;
	numBossesDefeated = 0;
	numGoalsReached = 0;
    }

    // Update is called once per frame
    void Update() {
	if (numBossesDefeated >= 2) gameObject.SetActive(false);
    }
}
