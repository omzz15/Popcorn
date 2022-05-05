using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform feet;
    [SerializeField] private Transform head;
    [SerializeField] private Transform[] headPositions;
    public float playerSpeed = 4f;
    public float rotSpeed = 0.3f;

    [Space]
    [Header("Run")]
    public float runSpeedMultiplier = 2;

    [Space]
    [Header("Crouch")]
    public float crouchSpeedMultiplier = 0.5f;



    private float sprint;

    Vector3 velocity = Vector3.zero;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        DefineActions();
    }

    // Update is called once per frame
    void Update()
    {

        //Normal Rotate
        float rotate = Input.GetAxis("Horizontal")*Time.deltaTime * rotSpeed + transform.localEulerAngles.y; 

        transform.localRotation = Quaternion.Euler(0,  rotate, 0);

        setCrouch();
        SetRun();

        //Player Movement
        Vector3 move = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;


        if (!Physics.Raycast(feet.position, Vector3.down, 0.3f, ground)){

            velocity.y += gravity * Time.deltaTime * Time.deltaTime /2;

        }

        controller.Move(move * Time.deltaTime * playerSpeed * sprint + velocity);
    }

    void SetRun()
    {
        if (Input.GetButtonDown("Run"))
        {
            Info.SetRunning(true, true);
        }
        if (Input.GetButtonUp("Run"))
        {
            Info.SetRunning(false, true);
        }
    }

    void setCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
            Info.SetCrouching(true, true);
        else if (Input.GetButtonUp("Crouch"))
            Info.SetCrouching(false, false);
    }

    private void DefineActions() {
        GameController.GetActionManager().AddAction(ActionManager.k_OnRun, () => {
            sprint = runSpeedMultiplier;
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnUnrun, () =>
        {
            sprint = 1;
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnCrouch, () => {
            sprint = crouchSpeedMultiplier;
            head.position = headPositions[1].position;
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnUncrouch, () => {
            sprint = 1;
            head.position = headPositions[0].position;
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnScoping, () => {
            sprint = Info.currentGun.zoomSpeedMultiplyer;
        });
        GameController.GetActionManager().AddAction(ActionManager.k_OnUnscoping, () => {
            sprint = 1;
        });
    }
}
