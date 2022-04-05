using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Rigidbody rb;
    private bool isAttacking;
    public Animation midRangeAnimation;
    public Animation shortRangeAnimation;
    public int damage;
    public double attackCoolDown;
    public double attackDuration;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        isAttacking = false;
        //midRangeAnimation = GetComponent<Animation>();
        //shortRangeAnimation = GetComponent<Animation>();
        //damage = 5;
        //attackCoolDown = 1; all can be specified in the game engine
        //attackDuration = 0.5;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCoolDown > 0) 
        {
            if(Vector3.Distance(transform.position, player.transform.position) < 3 
            && Vector3.Distance(transform.position, player.transform.position) > 1) 
            {
                doAnimation(midRangeAnimation);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) <= 1) 
            {
                doAnimation(shortRangeAnimation);
            }
            attackCoolDown -= Time.deltaTime;
        }
        else 
        {
            attackCoolDown = 1;
        }
    }
    void doAnimation(Animation anim) {
        if(attackDuration > 0) {
             isAttacking = true;
             if(isAttacking) {
                anim.Play();  
             }
             else {
                 isAttacking = false;    
             }
             attackDuration -= Time.deltaTime;   
            }
            // RaycastHit hit;
            // if (hit.RigidBody(player)) { //maybe player collider instead of RigidBody?
            //    player.isHit(true);
            // }
    }
}
