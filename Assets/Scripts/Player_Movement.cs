using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float playerSpeed;
    public float maxSpeed;
    public float inAirSpeed;
    public float jumpForce;
    public float runTransition;
    public float dashForce;
    public float dashCooldown;
    private int colliderAmount;
    private Vector3 forward;
    private Vector3 right;
    private Rigidbody rb;
    private State state;
    private KeyCode dashButton;
    private bool canDash;

    enum State
    {
        Default,
        InAir
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Default;
        forward = new Vector3();
        right = new Vector3();
        rb = GetComponent<Rigidbody>();
        dashButton = KeyCode.LeftShift;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement Helper
        Vector3 cameraVector = Camera.main.transform.rotation.eulerAngles;
        cameraVector.x = cameraVector.z = 0;

        forward = Quaternion.Euler(cameraVector) * Vector3.forward;
        right = Quaternion.Euler(cameraVector) * Vector3.right;
        forward.y = right.y = 0;


        // State Machine
        if (colliderAmount > 0)
        {
            state = State.Default;
        }
        else
        {
            state = State.InAir;
        }

        //Raycast

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, out hit, 100))
            {
                
            }

        }

        //Movement

        switch (state)
        {
            case State.Default:
                {
                    GroundMovement();
                    break;
                }
            case State.InAir:
                {
                    InAirMovement();
                    break;
                }
            default: break;
        }
    }

    private void InAirMovement()
    {
        if (XVelocityGood())
        {
            rb.AddForce(Input.GetAxis("Vertical") * forward * inAirSpeed * rb.mass * Time.deltaTime);
        }
        if (ZVelocityGood())
        {
            rb.AddForce(Input.GetAxis("Horizontal") * right * inAirSpeed * rb.mass * Time.deltaTime);
        }
        if (Input.GetKeyDown(dashButton))
        {
            Dash();
        }
    }

    private void GroundMovement()
    {
        Vector3 movementVec = Vector3.Lerp(rb.velocity, (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right) * playerSpeed, runTransition * Time.deltaTime);
        movementVec.y = rb.velocity.y;
        rb.velocity = movementVec;
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(dashButton))
        {
            Dash();
        }
    }

    private void Dash()
    {
        if (canDash)
        {
            print("Dashed");
            canDash = false;
            rb.velocity = dashForce * (Input.GetAxis("Vertical") * forward + Input.GetAxis("Horizontal") * right);
            Invoke("ResetDash", dashCooldown);
        }
    }

    private void ResetDash()
    {
        canDash = true;
    }

    private bool XVelocityGood()
    {
        return -maxSpeed < rb.velocity.x && rb.velocity.x < maxSpeed;
    }

    private bool ZVelocityGood()
    {
        return -maxSpeed < rb.velocity.z && rb.velocity.z < maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        colliderAmount++;
    }

    private void OnTriggerExit(Collider other)
    {
        colliderAmount--;
    }
}
