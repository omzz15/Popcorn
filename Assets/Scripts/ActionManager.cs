using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
    //common actions    
    public static readonly string k_OnRun = "on run";
    public static readonly string k_OnUnrun = "on unrun";
    public static readonly string k_OnCrouch = "on crouch";
    public static readonly string k_OnUncrouch= "on uncrouch";
    public static readonly string k_OnScoping = "on scope";
    public static readonly string k_OnUnscoping = "on unscope";
    public static readonly string k_OnGameActivate = "on game activate";
    public static readonly string k_WhileGameActive = "while game active";
    public static readonly string k_OnGameDeactivate = "on game deactivate";
    public static readonly string k_WhileGameDeactive = "while game deactive";
    public static readonly string k_OnStart = "on start";
    public static readonly string k_OnUpdate = "on update"; 

    //variables
    Dictionary<string, Actions> allActions = new Dictionary<string, Actions>();

    public void addAction(string key, Action action)
    {
        try{
            allActions[key].addAction(action);
        }
        catch (KeyNotFoundException){
            allActions[key] = new Actions();
            addAction(key, action);
        }
        catch (Exception e) {
            Debug.LogWarning("Exeption " + e + " thrown when addAction(" + key + ", " + action + ") was called!");
        }
    }

    public void runActions(string key) {
        try
        {
            allActions[key].runAll();
        }
        catch (Exception e){
            Debug.LogWarning("Exeption " + e + " thrown when runActions(" + key + ") was called!");
        }
    }

    public void clearActions(string key)
    {
        try
        {
            allActions[key].clearAll();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Exeption " + e + " thrown when clearActions(" + key + ") was called!");
        }
    }
}

public class Actions
{
    private LinkedList<Action> actions = new LinkedList<Action>();

    public void addAction(Action action)
    {
        //WARNING unprotected input!!
        actions.AddLast(action);
    }

    public void clearAll()
    {
        actions.Clear();
    }

    public void runAll()
    {
        foreach (Action action in actions)
            action();
    }
}