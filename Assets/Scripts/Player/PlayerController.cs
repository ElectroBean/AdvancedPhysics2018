using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSpeed;
    public float rotateSpeed;

    public float pushPower = 2.0f;

    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        float vertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", vertical * movementSpeed * Time.deltaTime);

        Quaternion newRot = new Quaternion(transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, Camera.main.transform.rotation.w);
        transform.rotation = newRot;
	}

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        rb.MovePosition(transform.position + transform.forward * vertical * movementSpeed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;

        GetComponent<Ragdoll>().RagdollOn = true;
    }
}
