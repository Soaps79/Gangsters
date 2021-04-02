using System;
using Assets.Scripts;
using QGame;
using UnityEngine;

[Serializable]
public class WorldTaskData
{
    public float TotalTime;
    public string DisplayName;
    public int RewardMoney;
}

public class WorldTask : QScript
{
    public float TotalTime;
    public string DisplayName;
    private const string STOPWATCH_KEY = "t";
    public TaskOutcome TaskOutcome;

    public bool IsComplete;

    public Action<WorldTask> OnTaskComplete;

    public float CurrentCraftElapsedAsZeroToOne
    {
        get
        {
            if (IsComplete) return 1f;
            return StopWatch.IsRunning()
                ? StopWatch[STOPWATCH_KEY].ElapsedLifetimeAsZeroToOne : 0f;
        }
    }

    public void Initialize(WorldTaskData data)
    {
        if (data == null)
            throw new UnityException();
        TaskOutcome = new TaskOutcome
        {
            MoneyReward = data.RewardMoney
        };

        TotalTime = data.TotalTime;
        DisplayName = data.DisplayName;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnComplete()
    {
        OnTaskComplete?.Invoke(this);
        IsComplete = true;
    }

    public void StartTimer()
    {
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick = OnComplete;
    }
}
