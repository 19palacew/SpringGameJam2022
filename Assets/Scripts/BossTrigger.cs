using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private GameObject boss;
    private bool start;
    private bool done;
    private bool hasSounded;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("GiantRobot");
        done = false;
        hasSounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(-83, -32.73f, -440.8f), .005f * Time.deltaTime * 200);
        }
        if (Vector3.Distance(boss.transform.position, new Vector3(-83, -32.73f, -440.8f)) < 1f && !done)
        {
            boss.GetComponent<Boss>().StartFight();
            Debug.Log("Fight");
            done = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            start = true;
        }
        if (!hasSounded)
        {
            hasSounded = true;
            boss.GetComponent<AudioSource>().Play();
        }
    }
}
