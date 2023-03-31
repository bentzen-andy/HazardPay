using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = transform.forward*moveSpeed;
    }
}
