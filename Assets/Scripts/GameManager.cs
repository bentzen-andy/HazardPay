using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    

    private void Awake() {
	instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
         
    }

    public void ReloadScene() {
	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
