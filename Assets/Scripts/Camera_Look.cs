using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Look : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalAngleCap;
    public float horizontalSpeed;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        yRotation += Input.GetAxis("Mouse X") * horizontalSpeed;
        xRotation -= Input.GetAxis("Mouse Y") * verticalSpeed;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}
