using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] private string firstLevel;

    // Start is called before the first frame update
    void Start()  {
        
    }

    // Update is called once per frame
    void Update()  {
        
    }


    public void PlayGame() {
	PlayerPrefs.DeleteAll();
	SceneManager.LoadScene(firstLevel);
    }


    public void PlayGameGodMode() {
	PlayerPrefs.DeleteAll();
	PlayerPrefs.SetString("godMode", "godModeYes");
	SceneManager.LoadScene(firstLevel);
    }


    public void QuitGame() {
	Application.Quit();
	Debug.Log("quitting game!!!");
    }
}
