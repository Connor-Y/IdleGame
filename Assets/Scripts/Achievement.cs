using UnityEngine;
using System.Collections;
using System;

public class Achievement {

    private string name;
    private int objectId;
    private int goal;
    private bool completed;
    private StatTracker stats;
    private Func<bool> goalFunction;
    private EntityHandler entityHandler;

    public Achievement(string name, Func<bool> goalFunction)
    {
        this.name = name;
        this.goalFunction = goalFunction;
        stats = StatTracker.Instance;
        completed = false;
    }

    public bool checkCompletion()
    {
        if (completed)
            return completed;

    
        completed = goalFunction();

        return completed;
    }

    public void setEntityHandler(EntityHandler entityHandler)
    {
        if (this.entityHandler != null)
        {
            Debug.LogError("Error: Attemping to change a set EntityHandler for Achievement - " + this.name);
            return;
        }
        this.entityHandler = entityHandler;
    }

}
