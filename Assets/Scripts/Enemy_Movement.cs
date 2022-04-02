using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed;
    private GameObject player;
    private Rigidbody rb;
    private SpiderTank_Walk walkScript;
    private float walkSpeed;
    private float lookAngle;
    public float lookSpeed;
    private Quaternion temp;
    private Quaternion lookAtPlayerAngle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        walkScript = GetComponent<SpiderTank_Walk>();
        walkSpeed = walkScript.speed;

        // Makes the enemy walk in a different direction every 2 seconds
        InvokeRepeating("ChangeAngles", 2f, 2f);

        temp = new Quaternion();
        lookAtPlayerAngle = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is within a certain distance
        if (Vector3.Distance(transform.position, player.transform.position)<10)
        {
            // The rotation is confusing here because I have to smooth the rotation but I'd need to create a new gameObject to calculate the direcion.
            // So instead I save the current rotation and then rotate it for the quick calculation and change it back.
            // Then using the difference I can smooth the location.

            walkScript.setSpeed(walkSpeed);
            // Record Current Rotation
            temp = transform.rotation;
            // Get the rotation that the enemy needs to have to look at player
            transform.LookAt(player.transform);
            lookAtPlayerAngle = transform.rotation;
            // Set rotation back to currect current rotation
            transform.rotation = temp;
            // Smooth between current and needed rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtPlayerAngle, Time.deltaTime * lookSpeed * 2);
            // Scrap Extra Angles
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            rb.velocity = transform.forward * speed + new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            // Random Movement when no in range of player; "lookAngle" gets changed every two seconds by an Invoking method in Start()
            walkScript.setSpeed(walkSpeed/2);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, lookAngle, 0), Time.deltaTime * lookSpeed);
            rb.velocity = transform.forward * speed/2 + new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void ChangeAngles()
    {
        lookAngle = Random.Range(1f,360f);
    }
}
