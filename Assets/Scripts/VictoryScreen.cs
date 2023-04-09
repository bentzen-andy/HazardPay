using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour {

    [SerializeField] private string mainMenuScene;
    [SerializeField] private float timeBetweenShowing = 1f;
    [SerializeField] private GameObject textBox, returnButton;
    [SerializeField] private Image blackScreen;
    [SerializeField] private float blackScreenFadeTime = 2f;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(ShowObjects());
    }


    // Update is called once per frame
    void Update() {
	float alpha = Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenFadeTime * Time.deltaTime);
	blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, alpha);
    }


    public void MainMenu() {
	Time.timeScale = 1f;
	SceneManager.LoadScene(mainMenuScene);
    }


    public IEnumerator ShowObjects() {
	yield return new WaitForSeconds(timeBetweenShowing);
	textBox.SetActive(true);
	yield return new WaitForSeconds(timeBetweenShowing);
	returnButton.SetActive(true);
    }
}
