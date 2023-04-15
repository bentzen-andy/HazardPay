using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] private string nextLevel;
    [SerializeField] private float waitTimeToLoadNextLevel = 2f;

    private string checkpointKey => ($"{SceneManager.GetActiveScene().name}_checkpoint");

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }


    private void OnTriggerExit(Collider other) {
	if (other.tag == "Player") {
	    StartCoroutine(EndLevel());
	    AudioManager.instance.PlayLevelVictoryMusic();
	}

    }




    private IEnumerator EndLevel() {
	// Set the new checkpoint to be at the beginnig of the next level
	PlayerPrefs.SetString(nextLevel + "_checkpoint", "");
	PlayerPrefs.SetString(checkpointKey, "");

	// run the end-of-level sequence 
	GameManager.instance.levelIsEnding = true;
	yield return new WaitForSeconds(waitTimeToLoadNextLevel);
	GameManager.instance.levelIsEnding = false;
	SceneManager.LoadScene(nextLevel);
	
    }

}
