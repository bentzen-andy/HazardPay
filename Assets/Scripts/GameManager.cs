using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool isPaused => (UIController.instance.pauseScreen.activeInHierarchy);
    public bool levelIsEnding;
    public int numGoalsReached;

    public static GameManager instance;
    

    private void Awake() {
	instance = this;
	numGoalsReached = 0;
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
	if (Input.GetKeyDown(KeyCode.Escape)) {
	    PauseUnpause();
	}
    }

    public void ReloadScene() {
	Unpause();
	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void PauseUnpause() {
	if (isPaused) Unpause();
	else Pause();
    }
    
    
    private void Pause() {
	UIController.instance.pauseScreen.SetActive(true);
	Cursor.lockState = CursorLockMode.None;
	Time.timeScale = 0f;
	AudioManager.instance.StopSfxAll();
    }
    
    
    private void Unpause() {
	UIController.instance.pauseScreen.SetActive(false);
	Cursor.lockState = CursorLockMode.Locked;
	Time.timeScale = 1f;
	
    }
}
