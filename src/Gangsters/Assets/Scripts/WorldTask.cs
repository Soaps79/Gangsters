using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using QGame;
using UnityEngine;

public class WorldTask : QScript
{
    public float TotalTime;
    public string DisplayName;
    private const string STOPWATCH_KEY = "t";

    public Action OnTaskComplete;

    public float CurrentCraftElapsedAsZeroToOne
    {
        get
        {
            var returnVal = StopWatch.IsRunning()
                ? StopWatch[STOPWATCH_KEY].ElapsedLifetimeAsZeroToOne : 0f;
            return returnVal;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnComplete()
    {
        if (OnTaskComplete != null)
            OnTaskComplete();
    }

    public void StartTimer()
    {
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick = OnComplete;
    }
}
