using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public double health;
    //public Animation death;
    //public Animation hurt;
    private bool isDead; // Not inherently useful right now, but could come in handy later
    //private bool isHurt;

    private Rigidbody rb;
    public Text HealthText;

    public Text loseText;

    
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        //isHurt = false;
        rb = GetComponent<Rigidbody>();
        //death = GetComponent<Animation>();
        //hurt = GetComponent<Animation>();
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0) {
            isDead = true;
            SceneManager.LoadScene("1stLevel");
            if (isDead) {
                //death.Play("DeathAnimation");
                //if(!death.IsPlaying("DeathAnimation")) {
                    EndGame();
                //}
            }
        }
        // if(isHurt) {
        //     //hurt.Play();
        //     //if(!hurt.IsPlaying("HurtAnimation"))
        //     isHurt = false;
        // }
    }

    void EndGame() {
        // stuff to end the game
    }
    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Enemy")) {
            if(health >= 10)
            health -= 10;
            SetCountText();
        }
    }
    void SetCountText() {
        HealthText.text = "Health: " + health.ToString();
        // if (health <= 0) {
        //     loseText.text = "You lose!";
        // }
    }
    
}
