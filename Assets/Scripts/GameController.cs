using UnityEngine;

public class GameController : MonoBehaviour
{
    //general
    private static ActionManager actionManager = new ActionManager();
    private static bool gameActive;

    public static void ActiveGame()
    {
        actionManager.RunActions(ActionManager.k_OnGameActivate);
        gameActive = true;
    }

    public static void DeactivateGame()
    {
        actionManager.RunActions(ActionManager.k_OnGameDeactivate);
        gameActive = false;
    }

    public static ActionManager GetActionManager()
    {
        return actionManager;
    }

    public static bool IsGameActive()
    {
        return gameActive;
    }

    void Start()
    {
        actionManager.RunActions(ActionManager.k_OnStart);
    }

    void Update()
    {
        actionManager.RunActions(ActionManager.k_OnUpdate);

        if (gameActive)
            actionManager.RunActions(ActionManager.k_WhileGameActive);
        else
            actionManager.RunActions(ActionManager.k_WhileGameDeactive);
    }
}
