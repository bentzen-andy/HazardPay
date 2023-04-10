using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource victoryMusic;
    [SerializeField] private AudioSource footSteps;

    [SerializeField] private AudioSource[] soundEffects;
    

    private void Awake() {
	instance = this;
    }


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        
    }


    public void PlaySFX(int sfxNumber) {
	soundEffects[sfxNumber].Stop();
	soundEffects[sfxNumber].Play();
    }


    public void StopSFX(int sfxNumber) {
	soundEffects[sfxNumber].Stop();
    }


    public void StopSfxAll() {
	foreach (AudioSource sfx in soundEffects) {
	    sfx.Stop();
	}
	footSteps.Stop();
    }


    public void StopBackgroundMusic() {
	backgroundMusic.Stop();
	Destroy(footSteps);
    }


    public void PlayLevelVictoryMusic() {
	StopBackgroundMusic();
	victoryMusic.Play();
    }


	
}
