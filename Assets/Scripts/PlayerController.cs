using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ActionManager actionManager = new ActionManager();
    private Transform playerTransform;
    //movement
    private bool running;
    private bool crouching;
    private bool scoping;
    //health
    private float health;
    private float armor;

    //---------getter and setter---------//
    //run
    public void SetRunning(bool val, bool overwrite) {
        if (val == running) return; //why are you calling this method??

        if (val)
        {
            if (IsCrouching() || IsScoping()) {
                if (overwrite)
                {
                    SetCrouching(false, false);
                    SetScoping(false, false);
                }
                else return;
            }
            actionManager.RunActions(ActionManager.k_OnRun);
        }
        else
            actionManager.RunActions(ActionManager.k_OnUnrun);
        running = val;
    }
    public bool IsRunning() {
        return running;
    }
    
    //crouch
    public void SetCrouching(bool val, bool overwrite){
        if (val == crouching) return; //why are you calling this method??

        if (val)
        {
            if (IsRunning()) {
                if (overwrite)
                    SetRunning(false, false);
                else return;
            }
            actionManager.RunActions(ActionManager.k_OnCrouch);
        }
        else
            actionManager.RunActions(ActionManager.k_OnUncrouch);
        crouching = val;
    }
    public bool IsCrouching() { 
        return crouching;
    }

    //scope
    public void SetScoping(bool val, bool overwrite) {
        if(val == scoping) return; //why are you calling this method??

        if (val)
        {
            if (IsRunning())
            {
                if (overwrite)
                    SetRunning(false, false);
                else return;
            }
            actionManager.RunActions(ActionManager.k_OnScoping);
        }
        else
            actionManager.RunActions(ActionManager.k_OnUnscoping);
        scoping = val;
    }
    public bool IsScoping() { 
        return scoping;
    }

    //action manager
    public ActionManager GetActionManager() {
        return actionManager;
    }

    public Transform GetPlayerTransform() { 
        return playerTransform;
    }

    void Awake()
    {
        playerTransform = transform;
    }
    //TODO add update and start with actionManager calls
}