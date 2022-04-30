using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public PlayerController controller;

    public float mouseSensitivity = 100f;
    public float MaxAngle = 45f;
    public float minAngle = -45f;
    public Transform playerBody;
    public float baseFOV;

    float xRotation = 0f;

    void Awake()
    {
        defineActions();
        gameObject.GetComponent<Camera>().fieldOfView = baseFOV;
    }

    void run()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minAngle, MaxAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void lockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void unlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void defineActions() {
        GameController.getActionManager().addAction(ActionManager.k_OnGameActivate, () => { 
            lockCursor(); 
        });
        GameController.getActionManager().addAction(ActionManager.k_OnGameDeactivate, () => {
            unlockCursor();
        });
        GameController.getActionManager().addAction(ActionManager.k_WhileGameActive, () => {
            if (Input.GetButton("Cancel"))
                GameController.deactivateGame();
            run();
        });
        GameController.getActionManager().addAction(ActionManager.k_WhileGameDeactive, () =>{
            if (Input.GetMouseButton(0))
                GameController.activeGame();
        });
    }
}
