using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float MaxAngle = 45f;
    public float minAngle = -45f;
    public Transform playerBody;
    public Transform playerHead;
    public float baseFOV;

    float xRotation = 0f;
    bool unlocked = false;

    void Awake()
    {
        if (Time.timeScale > 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Start()
    {
        gameObject.GetComponent<Camera>().fieldOfView = baseFOV;
    }

    void Update()
    {
        if (unlocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minAngle, MaxAngle);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);


        }

        if ()) 
        {
            if (unlocked)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            unlocked = !unlocked;
        }
    }
}
