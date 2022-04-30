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
        DefineActions();
        gameObject.GetComponent<Camera>().fieldOfView = baseFOV;
    }

    void Run()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minAngle, MaxAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void LockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void DefineActions() {
        GameController.GetActionManager().AddAction(ActionManager.k_OnGameActivate, () => { 
            LockCursor(); 
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnGameDeactivate, () => {
            UnlockCursor();
        });
        GameController.GetActionManager().AddAction(ActionManager.k_WhileGameActive, () => {
            if (Input.GetButton("Cancel"))
                GameController.DeactivateGame();
            Run();
        });
        GameController.GetActionManager().AddAction(ActionManager.k_WhileGameDeactive, () =>{
            if (Input.GetMouseButton(0))
                GameController.ActiveGame();
        });
    }
}
