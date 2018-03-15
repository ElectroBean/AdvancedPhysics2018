using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRagdoll : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", speed);
	}

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * 0.01f);
    }
}
