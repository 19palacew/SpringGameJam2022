using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTank_Walk : MonoBehaviour
{
    public float speed;
    public GameObject foot_1;
    public GameObject foot_2;
    public GameObject foot_3;
    public GameObject foot_4;
    public GameObject target_1;
    public GameObject target_2;
    public GameObject target_3;
    public GameObject target_4;
    private float offset_1;
    private float offset_2;
    // Start is called before the first frame update
    void Start()
    {
        // Offsets so the feet arent mirrored (otherwise all feet would leave the ground together on walk)
        offset_1 = -Mathf.PI / 2;
        offset_2 = Mathf.PI/2;

        // Should also be noted that the walking is an illusion, the spider is just a ridgidbody sphere that drags itself, theres no actual "feet"
    }

    // Update is called once per frame
    void Update()
    {
        // think of this as "x" increasing as you follow a graph for sin(x);
        offset_1 += Time.deltaTime * speed;
        offset_2 += Time.deltaTime * speed;

        // Raycasts to put each foot on the floor
        Cast(foot_1, target_1);
        Cast(foot_2, target_2);
        Cast(foot_3, target_3);
        Cast(foot_4, target_4);

        // Foot rotation to give walking illusion
        SinRot(target_1, offset_1, -1.2f);
        SinRot(target_2, offset_2, 1.2f);
        SinRot(target_3, offset_1, 1.2f);
        SinRot(target_4, offset_2, -1.2f);
    }

    private void Cast(GameObject foot, GameObject target)
    {
        RaycastHit hit;
        if (Physics.Raycast(foot.transform.position + new Vector3(0, 1, 0), Vector3.down, out hit, 100, ~LayerMask.GetMask("Enemy")))
        {
            // So the spider cant consider itself ground
            if (!hit.transform.CompareTag("Enemy"))
            {
                target.transform.position = new Vector3(target.transform.position.x, hit.point.y, target.transform.position.z);
            }
        }

        Debug.DrawRay(foot.transform.position + new Vector3(0, 1, 0), Vector3.down * 10, Color.green);
    }

    // Leg movement is just a sin function, the rest is to adjust the curve to make the movement look more natural
    private void SinRot(GameObject target, float offset, float footOffset)
    {
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + Mathf.Pow((Mathf.Sin(offset) + 1), 2)/5, target.transform.position.z);
        target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, Mathf.Sin(offset - Mathf.PI / 2) + footOffset);
    }

    // This is largely to change the speed based on how fast the Enemy_Movement script wants to go
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
