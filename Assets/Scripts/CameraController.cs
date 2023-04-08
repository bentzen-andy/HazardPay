using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //[SerializeField] private Transform target;
    //[SerializeField] private float startFOV;
    //[SerializeField] private float targetFOV;
    //[SerializeField] private float zoomSpeed = 1f;
    //[SerializeField] private Camera camera;


    // Start is called before the first frame update
    void Start() {
	//startFOV = camera.fieldOfView;
	//targetFOV = startFOV;
    }


    // Update is called once per frame
    void LateUpdate() {
        //transform.position = target.position;
        //transform.rotation = target.rotation;
	//camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV zoomSpeed * Time.deltaTime);
    }


    //public void ZoomIn(float newZoom) {
	//targetFOV = newZoom;
    //}


    //public void ZoomIn() {
	//targetFOV = startFOV;
    //}
}
