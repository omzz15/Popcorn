using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /**
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
        defineActions();
    }

    // Update is called once per frame
    void Run()
    {
        Move();
        setCrouch();
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
            controller.setRunning(true, true);
        }
        if (Input.GetButton("Run")) { 
            timeSinceLastRun = 0;
            if (moving)
            {
                runTimeLeft -= Time.deltaTime;
                if (runTimeLeft <= 0) {
                    controller.setRunning(false, true);
                    runTimeLeft = 0;
                }
            }
            return;
        }

        timeSinceLastRun += Time.deltaTime;

        if (timeSinceLastRun >= waitTimeBeforeRunFills && runTimeLeft < timeTillRunEmpty) { 
            runTimeLeft += (timeTillRunEmpty/timeTillRunFull) * Time.deltaTime;
        }
    }

    void setCrouch() {
        if (Input.GetButtonDown("Crouch"))
            controller.setCrouching(true, true);
        else if (Input.GetButtonUp("Crouch"))
            controller.setCrouching(false, false);
    }

    void defineActions() {
        controller.getActionManager().addAction(ActionManager.k_OnRun, () => {
            movementSpeedMultiplier *= runSpeedMultiplier;
            movementForceMultiplier *= runForceMultiplier;
        });
        controller.getActionManager().addAction(ActionManager.k_OnUnrun, () => {
            movementSpeedMultiplier /= runSpeedMultiplier;
            movementForceMultiplier /= runForceMultiplier;
        });
        controller.getActionManager().addAction(ActionManager.k_OnCrouch, () => { 
            movementSpeedMultiplier *= crouchSpeedMultiplier;
            movementForceMultiplier *= crouchForceMultiplier;
        });
        controller.getActionManager().addAction(ActionManager.k_OnUncrouch, () => {
            movementSpeedMultiplier /= crouchSpeedMultiplier;
            movementForceMultiplier /= crouchForceMultiplier;
        });
        controller.getActionManager().addAction(ActionManager.k_OnScoping, () => {
            movementSpeedMultiplier *= scopingSpeedMultiplier;
            movementForceMultiplier *= scopingForceMultiplier;
        });
        controller.getActionManager().addAction(ActionManager.k_OnUnscoping, () => { 
            movementSpeedMultiplier /= scopingSpeedMultiplier;
            movementForceMultiplier /= scopingForceMultiplier;
        });

        GameController.getActionManager().addAction(ActionManager.k_WhileGameActive, () =>
        {
            Run();
        });
    }
    **/
}