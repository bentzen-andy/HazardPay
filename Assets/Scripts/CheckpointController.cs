using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour {

    [SerializeField] string checkpointName;

    private string checkpointKey => ($"{SceneManager.GetActiveScene().name}_checkpoint");

    
    // Start is called before the first frame update
    void Start() {
	if (currentCheckpointIsMostRecentCp()) {
	    spawnPlayerAtMostRecentCheckpoint();
	}
    }
    

    // Update is called once per frame
    void Update() {
	if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.L)) {
	    PlayerPrefs.SetString(checkpointKey, "");
	}
    }


    private void OnTriggerEnter(Collider other) {
	if (other.gameObject.tag == "Player") {
	    PlayerPrefs.SetString(checkpointKey, checkpointName);
	    AudioManager.instance.PlaySFX(1);
	}
    }


    private bool currentCheckpointIsMostRecentCp() {
	if (PlayerPrefs.HasKey(checkpointKey)) {
	    if (PlayerPrefs.GetString(checkpointKey) == checkpointName) {
		return true;
	    }
	}
	return false;
    }


    private void spawnPlayerAtMostRecentCheckpoint() {
	PlayerController.instance.transform.position = transform.position;
	Debug.Log("player starting at " + checkpointName);
    }
}
