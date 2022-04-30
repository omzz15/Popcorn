using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    //general
    private static ActionManager actionManager = new ActionManager();
    private static bool gameActive;

    public static void activeGame()
    {
        actionManager.runActions(ActionManager.k_OnGameActivate);
        gameActive = true;
    }

    public static void deactivateGame()
    {
        actionManager.runActions(ActionManager.k_OnGameDeactivate);
        gameActive = false;
    }

    public static ActionManager getActionManager() { 
        return actionManager;
    }

    public static bool isGameActive() { 
        return gameActive;
    }

    void Start()
    {
        actionManager.runActions(ActionManager.k_OnStart);    
    }

    void Update()
    {
        actionManager.runActions(ActionManager.k_OnUpdate);
        
        if (gameActive)
            actionManager.runActions(ActionManager.k_WhileGameActive);
        else
            actionManager.runActions(ActionManager.k_WhileGameDeactive);
    }
}
