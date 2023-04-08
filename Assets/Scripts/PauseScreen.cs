using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

    [SerializeField] private string mainMenuScene;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
    }


    public void Resume() {
	GameManager.instance.PauseUnpause();
    }


    public void MainMenu() {
	Time.timeScale = 1f;
	SceneManager.LoadScene(mainMenuScene);
    }


    public void Quit() {
	Application.Quit();
	Debug.Log("quitting game!!!-------");
    }
}
