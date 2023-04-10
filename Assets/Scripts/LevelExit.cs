using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] private string nextLevel;
    [SerializeField] private float waitTimeToLoadNextLevel = 2f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }


    private void OnTriggerExit(Collider other) {
	if (other.tag == "Player") {
	    StartCoroutine(EndLevel());
	}

    }




    private IEnumerator EndLevel() {
	GameManager.instance.levelIsEnding = true;
	yield return new WaitForSeconds(waitTimeToLoadNextLevel);
	GameManager.instance.levelIsEnding = false;
	SceneManager.LoadScene(nextLevel);
	
    }

}
