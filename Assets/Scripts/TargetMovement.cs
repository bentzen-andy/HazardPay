using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour {

    [SerializeField] private bool shouldMove;
    [SerializeField] private bool shouldRotate;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (shouldMove) {
	    transform.position += new Vector3(moveSpeed, 0f, 0f)*Time.deltaTime;
	}

	if (shouldRotate) {
	    transform.rotation =
	      Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotateSpeed*Time.deltaTime, 0f));
	}
    }
}