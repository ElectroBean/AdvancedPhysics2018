using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour {

    private Animator animator = null;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    Rigidbody mainRB;
    Collider mainCollider;

    private void Awake()
    {
        mainRB = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
    }

    public bool RagdollOn {
        get { return !animator.enabled; }
        set
        {
            animator.enabled = !value;
            foreach (Rigidbody r in rigidbodies)
                r.isKinematic = !value;

            mainRB.isKinematic = true;
            mainCollider.enabled = false;
        }
    }

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        foreach (Rigidbody r in rigidbodies)
            r.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
        
}
