using System;
using Assets.Scripts;
using QGame;

public class WorldTask : QScript
{
    public float TotalTime;
    public string DisplayName;
    private const string STOPWATCH_KEY = "t";
    public TaskOutcome TaskOutcome;

    public Action<WorldTask> OnTaskComplete;

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
        OnTaskComplete?.Invoke(this);
    }

    public void StartTimer()
    {
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick = OnComplete;
    }
}
