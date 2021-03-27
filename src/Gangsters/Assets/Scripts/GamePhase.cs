using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using QGame;
using UnityEngine.UI;

public class GamePhase : QScript
{
    public float TotalTime;
    private const string STOPWATCH_KEY = "phasetimer";
    public SliderBinding SliderBinding;

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
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick = OnComplete;  
        SliderBinding.Initialize(() => CurrentCraftElapsedAsZeroToOne);
    }

    private void OnComplete()
    {
        
    }

    public void StartTimer()
    {
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick = OnComplete;
    }
}
