using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public float rotateSpeed;

    public float pushPower = 2.0f;

    private Rigidbody rb;
    private Animator anim;

    private bool m_canStand = true;
    public Collider headCollider;
    public float MaxVelocity;

    public float jumpVelocity;

    public float fallMultiplier;
    public float lowJumpMult;

    bool crouching = false;
    bool carryingObject = false;
    GameObject carriedObject;

    public float smoothLerp;

    private bool canJump = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", vertical * movementSpeed * Time.deltaTime);

        if (GetComponent<Ragdoll>().RagdollOn == false)
        {
            Quaternion newRot = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
            transform.rotation = newRot;

        }

        if (crouching)
        {
            headCollider.isTrigger = true;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + 0.5f, Camera.main.transform.position.z);
        }
        else
        {
            headCollider.isTrigger = false;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y + 1.5f, Camera.main.transform.position.z);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouching = true;
        }

        if (m_canStand && !Input.GetKey(KeyCode.LeftControl))
        {
            crouching = false;
        }



        if (Input.GetMouseButton(0))
        {
            Ray raycast = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(raycast, out hit))
            {
                if (hit.collider.tag == "Pickup")
                {
                    carryingObject = true;
                    carriedObject = hit.collider.gameObject;
                    carriedObject.GetComponent<Rigidbody>().isKinematic = true;
                    carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 2, Time.deltaTime * smoothLerp);

                    if(carriedObject.GetComponent<Ragdoll>())
                    {
                        carriedObject.GetComponent<Ragdoll>().RagdollOn = true;
                    }
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (carriedObject != null)
            {
                Rigidbody r2 = carriedObject.GetComponent<Rigidbody>();
                carryingObject = false;
                r2.isKinematic = false;

                carriedObject = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (GetComponent<Ragdoll>().RagdollOn == false)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            // rb.MovePosition(transform.position + transform.forward * vertical * movementSpeed * Time.deltaTime + transform.right * horizontal * movementSpeed / 2 * Time.deltaTime);

            Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;

            rb.AddForce(moveDir * movementSpeed);
        }



        if (Input.GetButtonDown("Jump"))
        {
            if(canJump)
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }

        //better jump
        //if(rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
        //if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        m_canStand = false;
    }

    private void OnTriggerExit(Collider other)
    {
        m_canStand = true;
    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Rigidbody body = hit.collider.attachedRigidbody;
    //    if (body == null || body.isKinematic)
    //        return;
    //    if (hit.moveDirection.y < -0.3f)
    //        return;
    //
    //    Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
    //    body.velocity = pushDir * pushPower;
    //
    //    GetComponent<Ragdoll>().RagdollOn = true;
    //}


    private void OnCollisionExit(Collision col)
    {
        canJump = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        canJump = true;
    }
}
