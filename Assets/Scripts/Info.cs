using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Info
{
    private static bool running;
    private static bool crouching;
    private static bool scoping;

    public static Gun currentGun;

    public static void SetRunning(bool val, bool overwrite)
    {
        if (val == running) return; //why are you calling this method??

        if (val)
        {
            if (IsCrouching() || IsScoping())
            {
                if (overwrite)
                {
                    SetCrouching(false, false);
                    SetScoping(false, false);
                }
                else return;
            }
            GameController.GetActionManager().RunActions(ActionManager.k_OnRun);
        }
        else
            GameController.GetActionManager().RunActions(ActionManager.k_OnUnrun);
        running = val;
    }
    public static bool IsRunning()
    {
        return running;
    }

    //crouch
    public static void SetCrouching(bool val, bool overwrite)
    {
        if (val == crouching) return; //why are you calling this method??

        if (val)
        {
            if (IsRunning())
            {
                if (overwrite)
                    SetRunning(false, false);
                else return;
            }
            GameController.GetActionManager().RunActions(ActionManager.k_OnCrouch);
        }
        else
            GameController.GetActionManager().RunActions(ActionManager.k_OnUncrouch);
        crouching = val;
    }
    public static bool IsCrouching()
    {
        return crouching;
    }

    //scope
    public static void SetScoping(bool val, bool overwrite)
    {
        if (val == scoping) return; //why are you calling this method??

        if (val)
        {
            if (IsRunning())
            {
                if (overwrite)
                    SetRunning(false, false);
                else return;
            }
            GameController.GetActionManager().RunActions(ActionManager.k_OnScoping);
        }
        else
            GameController.GetActionManager().RunActions(ActionManager.k_OnUnscoping);
        scoping = val;
    }
    public static bool IsScoping()
    {
        return scoping;
    }
}
