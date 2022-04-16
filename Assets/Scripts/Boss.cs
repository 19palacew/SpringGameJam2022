using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float rate;
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
        attacking = false;
        activeHand = rightHand;
        activeHandDefault = rightDefault;
        attackState = AttackState.Default;
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
                    activeHand.transform.position = Vector3.Lerp(activeHand.transform.position, activeHandDefault, rate);
                    activeHand.transform.localRotation = Quaternion.Lerp(activeHand.transform.localRotation, Quaternion.Euler(new Vector3()), rate);
                    break;
                }
            case AttackState.Raise:
                {
                    activeHand.transform.position = Vector3.Lerp(activeHand.transform.position, activeHandDefault + new Vector3(0,79,0), rate);
                    activeHand.transform.localRotation = Quaternion.Lerp(activeHand.transform.localRotation, Quaternion.Euler(new Vector3(180,0,0)), rate);
                    break;
                }
            case AttackState.Slam:
                {
                    activeHand.transform.position = Vector3.Lerp(activeHand.transform.position, player.transform.position, rate*2);
                    activeHand.transform.localRotation = Quaternion.Lerp(activeHand.transform.localRotation, Quaternion.Euler(new Vector3(90, 0, 0)), rate);
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
}
