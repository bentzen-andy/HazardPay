using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {

    [SerializeField] private string mainMenuScene;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
    }


    public void MainMenu() {
	Time.timeScale = 1f;
	SceneManager.LoadScene(mainMenuScene);
    }
}
