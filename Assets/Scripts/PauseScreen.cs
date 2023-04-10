using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

    [SerializeField] private string mainMenuScene;


    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update() {
    }


    public void Resume() {
	GameManager.instance.PauseUnpause();
    }


    public void LoadMainMenu() {
	Time.timeScale = 1f;
	SceneManager.LoadScene(mainMenuScene);
    }


    public void Quit() {
	Application.Quit();
	Debug.Log("quitting game!!!-------");
    }
}
