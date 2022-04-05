using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public double health;
    public Animation death;
    public Animation hurt;
    private bool isDead; // Not inherently useful right now, but could come in handy later
    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isHurt = false;
        death = GetComponent<Animation>();
        hurt = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0) {
            isDead = true;
            if(isDead) {
                death.Play("DeathAnimation");
                if(!death.IsPlaying("DeathAnimation")) {
                    EndGame();
                }
            }
        }
        if(isHit(isHurt)) {
            hurt.Play();
            if(!hurt.IsPlaying("HurtAnimation"))
            isHurt = false;
        }
    }

    //changed the enemy attack script to use method rather than raw damage
    bool isHit(bool isHurt) {
        health -= 10;
        isHurt = true;
        return isHurt;
    }
    void EndGame() {
        // stuff to end the game
    }
}
