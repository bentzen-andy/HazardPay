using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    [SerializeField] private AudioSource backgroundMusic;
    

    private void Awake() {
	instance = this;
    }


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        
    }


    public void StopBackgroundMusic() {
	backgroundMusic.Stop();
    }
}
