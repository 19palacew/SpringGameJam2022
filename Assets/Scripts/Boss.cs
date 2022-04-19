using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float rate;
    public GameObject canvas;
    public GameObject thanks;
    private float frameRateMakeUp;
    private GameObject player;
    private GameObject head;
    private GameObject body;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject activeHand;
    private Vector3 rightDefault;
    private Vector3 leftDefault;
    private Vector3 activeHandDefault;
    private AttackState attackState;
    private bool attacking;

    private enum AttackState{
        Default,
        Raise,
        Slam
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        head = body.transform.GetChild(0).gameObject;
        rightHand = transform.GetChild(6).GetChild(0).GetChild(0).gameObject;
        leftHand = transform.GetChild(6).GetChild(1).GetChild(0).gameObject;
        rightDefault = rightHand.transform.position;
        leftDefault = leftHand.transform.position;
        attacking = true;
        activeHand = rightHand;
        activeHandDefault = rightDefault;
        attackState = AttackState.Default;
        frameRateMakeUp = 400;
    }

    // Update is called once per frame
    void Update()
    {
        head.transform.LookAt(player.transform);

        if (!attacking)
        {
            if (activeHand.Equals(rightHand))
            {
                activeHand = leftHand;
                activeHandDefault = leftDefault;
            }
            else
            {
                activeHand = rightHand;
                activeHandDefault = rightDefault;
            }
            Invoke("Raise", 2f);
            attacking = true;
        }

        switch (attackState)
        {
            case AttackState.Default:
                {
                    activeHand.transform.position = Vector3.Lerp(activeHand.transform.position, activeHandDefault, rate * Time.deltaTime * frameRateMakeUp);
                    activeHand.transform.localRotation = Quaternion.Lerp(activeHand.transform.localRotation, Quaternion.Euler(new Vector3()), rate * Time.deltaTime * frameRateMakeUp);
                    break;
                }
            case AttackState.Raise:
                {
                    activeHand.transform.position = Vector3.Lerp(activeHand.transform.position, activeHandDefault + new Vector3(0,79,0), rate * Time.deltaTime * frameRateMakeUp);
                    activeHand.transform.localRotation = Quaternion.Lerp(activeHand.transform.localRotation, Quaternion.Euler(new Vector3(180,0,0)), rate * Time.deltaTime * frameRateMakeUp);
                    break;
                }
            case AttackState.Slam:
                {
                    activeHand.transform.position = Vector3.Lerp(activeHand.transform.position, player.transform.position, rate*2 * Time.deltaTime * frameRateMakeUp);
                    activeHand.transform.localRotation = Quaternion.Lerp(activeHand.transform.localRotation, Quaternion.Euler(new Vector3(90, 0, 0)), rate * Time.deltaTime * frameRateMakeUp);
                    break;
                }
            default: break;
        }
    }

    private void Raise()
    {
        Debug.Log("Raise");
        attackState = AttackState.Raise;
        Invoke("Slam", 2f);
    }

    private void Slam()
    {
        Debug.Log("Slam");
        attackState = AttackState.Slam;
        Invoke("DoneSlam", 2f);
    }

    private void DoneSlam()
    {
        attackState = AttackState.Default;
        attacking = false;
    }

    public void StartFight()
    {
        rightDefault = rightHand.transform.position;
        leftDefault = leftHand.transform.position;
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.GetChild(0).GetComponent<VideoPlayer>().enabled = true;
            player.transform.GetChild(0).GetComponent<VideoPlayer>().Play();
            player.transform.GetChild(0).transform.GetChild(1).GetComponent<Camera>().enabled = false;
            player.transform.position = new Vector3(0,1000,0);
            canvas.GetComponent<Canvas>().enabled = false;
            Invoke("EndGame", 10f);
        }
    }

    private void EndGame()
    {
        canvas.transform.GetChild(0).GetComponent<Text>().enabled = false;
        canvas.GetComponent<Canvas>().enabled = true;
        Instantiate(thanks, canvas.transform);
        Invoke("FreeMouse", 2f);
    }

    private void FreeMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
