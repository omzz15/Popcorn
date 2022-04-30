using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ActionManager actionManager = new ActionManager();
    //movement
    private bool running;
    private bool crouching;
    private bool scoping;
    //health
    private float health;
    private float armor;

    //---------getter and setter---------//
    //run
    public void setRunning(bool val, bool overwrite) {
        if (val == running) return; //why are you calling this method??

        if (val)
        {
            if (isCrouching() || isScoping()) {
                if (overwrite)
                {
                    setCrouching(false, false);
                    setScoping(false, false);
                }
                else return;
            }
            actionManager.runActions(ActionManager.k_OnRun);
        }
        else
            actionManager.runActions(ActionManager.k_OnUnrun);
        running = val;
    }
    public bool isRunning() {
        return running;
    }
    
    //crouch
    public void setCrouching(bool val, bool overwrite){
        if (val == crouching) return; //why are you calling this method??

        if (val)
        {
            if (isRunning()) {
                if (overwrite)
                    setRunning(false, false);
                else return;
            }
            actionManager.runActions(ActionManager.k_OnCrouch);
        }
        else
            actionManager.runActions(ActionManager.k_OnUncrouch);
        crouching = val;
    }
    public bool isCrouching() { 
        return crouching;
    }

    //scope
    public void setScoping(bool val, bool overwrite) {
        if(val == scoping) return; //why are you calling this method??

        if (val)
        {
            if (isRunning())
            {
                if (overwrite)
                    setRunning(false, false);
                else return;
            }
            actionManager.runActions(ActionManager.k_OnScoping);
        }
        else
            actionManager.runActions(ActionManager.k_OnUnscoping);
        scoping = val;
    }
    public bool isScoping() { 
        return scoping;
    }

    //action manager
    public ActionManager getActionManager() {
        return actionManager;
    }

    //TODO add update and start with actionManager calls
}