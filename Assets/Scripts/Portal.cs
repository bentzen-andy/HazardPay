using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField] private Transform warpTo;
    [SerializeField] private float warpTime = 0.5f;
    [SerializeField] private bool portalIsAtEndOfRoom;
    [SerializeField] private Transform portalToBeDestoyedAfterWarp;
    [SerializeField] private Transform newPortalPosition;

    private bool isWarping;


    // Start is called before the first frame update
    void Start() {
        //if (onlyShowPortalAfterGoal) {
	    //gameObject.SetActive(false);
	//}
    }

    // Update is called once per frame
    void Update() {
	if (newPortalPosition != null && GameManager.instance.numGoalsReached == 5) {
	    transform.position = newPortalPosition.position;
	}

					 
	//Debug.Log("goals: " + GameManager.instance.numGoalsReached);
	//Debug.Log("onlyShowPortalAfterGoal: " + onlyShowPortalAfterGoal);
        //if (onlyShowPor/talAfterGoal && GameManager.instance.numGoalsReached == 5) {
	    //gameObject.SetActive(true);
	    //Debug.Log("Show the portal");
	//}

    }
    
    
    private void OnTriggerExit(Collider other) {
	if (warpTo == null) return;
	if (other.tag == "Player" && !isWarping) {
	    AudioManager.instance.PlaySFX(1);
	    if (portalIsAtEndOfRoom) GameManager.instance.numGoalsReached++;
	    Debug.Log("goals: " + GameManager.instance.numGoalsReached);
	    StartCoroutine(Warp(other));
	    
	    if (portalToBeDestoyedAfterWarp != null) {
		//portalToBeDestoyedAfterWarp.gameObject.SetActive(false);
		Destroy(portalToBeDestoyedAfterWarp.gameObject);
	    }
	}

    }




    private IEnumerator Warp(Collider other) {
	isWarping = true;
	GameManager.instance.levelIsEnding = true;
	yield return new WaitForSeconds(warpTime);
	isWarping = false;
	GameManager.instance.levelIsEnding = false;
	other.transform.position = warpTo.position;
	other.transform.rotation = warpTo.transform.rotation;
	
    }


}
