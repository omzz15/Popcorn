using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;

    [Space]
    [Header("Walk")]
    public float baseForwardSpeed;
    public float baseBackwardSpeed;
    public float baseSideSpeed;
    public float baseForce;

    [Space]
    [Header("Run")]
    public float runSpeedMultiplier;
    public float runForceMultiplier;
    [Header("Run Times")]
    public float waitTimeBeforeRunFills;
    public float timeTillRunEmpty;
    public float timeTillRunFull;

    [Space]
    [Header("Crouch")]
    public float crouchSpeedMultiplier;
    public float crouchForceMultiplier;

    [Space]
    [Header("Scoping")]
    public float scopingSpeedMultiplier;
    public float scopingForceMultiplier;

    //other variables
    Rigidbody rb;

    float movementSpeedMultiplier = 1;
    float movementForceMultiplier = 1;

    //running
    bool moving;
    float runTimeLeft;
    float timeSinceLastRun;



    public void Awake()
    {
        runTimeLeft = timeTillRunEmpty;
        rb = GetComponent<Rigidbody>();
        DefineActions();
    }

    // Update is called once per frame
    void Run()
    {
        Move();
        SetCrouch();
        SetRun();
    }

    void Move()
    {
        float xForce = Input.GetAxis("Horizontal") * movementForceMultiplier * baseForce;
        float yForce = Input.GetAxis("Vertical") * movementForceMultiplier * baseForce;

        moving = Mathf.Abs(xForce) > 0 || Mathf.Abs(yForce) > 0;

        if (moving)
        {
            rb.AddRelativeForce(new Vector3(xForce, 0, yForce), ForceMode.Force);

            Vector3 vel = transform.InverseTransformVector(rb.velocity);

            vel.z = Mathf.Clamp(vel.z, -baseBackwardSpeed * movementSpeedMultiplier, baseForwardSpeed * movementSpeedMultiplier);
            vel.x = Mathf.Clamp(vel.x, -baseSideSpeed * movementSpeedMultiplier, baseSideSpeed * movementSpeedMultiplier);

            rb.velocity = transform.TransformVector(vel);
        }
    }

    void SetRun() {
        if (Input.GetButtonDown("Run")) {
            controller.SetRunning(true, true);
        }
        if (Input.GetButton("Run")) { 
            timeSinceLastRun = 0;
            if (moving)
            {
                runTimeLeft -= Time.deltaTime;
                if (runTimeLeft <= 0) {
                    controller.SetRunning(false, true);
                    runTimeLeft = 0;
                }
            }
            return;
        }
        if (Input.GetButtonUp("Run"))
        {
            controller.SetRunning(false, true);
        }

            timeSinceLastRun += Time.deltaTime;

        if (timeSinceLastRun >= waitTimeBeforeRunFills && runTimeLeft < timeTillRunEmpty) { 
            runTimeLeft += (timeTillRunEmpty/timeTillRunFull) * Time.deltaTime;
        }
    }

    void SetCrouch() {
        if (Input.GetButtonDown("Crouch"))
            controller.SetCrouching(true, true);
        else if (Input.GetButtonUp("Crouch"))
            controller.SetCrouching(false, false);
    }

    void DefineActions() {
        controller.GetActionManager().AddAction(ActionManager.k_OnRun, () => {
            movementSpeedMultiplier *= runSpeedMultiplier;
            movementForceMultiplier *= runForceMultiplier;
        });
        controller.GetActionManager().AddAction(ActionManager.k_OnUnrun, () => {
            movementSpeedMultiplier /= runSpeedMultiplier;
            movementForceMultiplier /= runForceMultiplier;
        });
        controller.GetActionManager().AddAction(ActionManager.k_OnCrouch, () => { 
            movementSpeedMultiplier *= crouchSpeedMultiplier;
            movementForceMultiplier *= crouchForceMultiplier;
        });
        controller.GetActionManager().AddAction(ActionManager.k_OnUncrouch, () => {
            movementSpeedMultiplier /= crouchSpeedMultiplier;
            movementForceMultiplier /= crouchForceMultiplier;
        });
        controller.GetActionManager().AddAction(ActionManager.k_OnScoping, () => {
            movementSpeedMultiplier *= scopingSpeedMultiplier;
            movementForceMultiplier *= scopingForceMultiplier;
        });
        controller.GetActionManager().AddAction(ActionManager.k_OnUnscoping, () => { 
            movementSpeedMultiplier /= scopingSpeedMultiplier;
            movementForceMultiplier /= scopingForceMultiplier;
        });

        GameController.GetActionManager().AddAction(ActionManager.k_WhileGameActive, () =>
        {
            Run();
        });
    }
}