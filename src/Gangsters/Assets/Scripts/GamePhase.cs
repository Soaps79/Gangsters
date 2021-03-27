using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using QGame;
using UnityEngine.UI;

public class GamePhase : QScript
{
    public float TotalTime;
    private const string STOPWATCH_KEY = "phasetimer";
    public GamePhaseViewModel ViewModel;
    public List<WorldTask> WorldTasks;
    private readonly Queue<WorldTask> _worldTaskQueue = new Queue<WorldTask>();

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
        foreach (var worldTask in WorldTasks)
        {
            _worldTaskQueue.Enqueue(worldTask);
        }
        ViewModel.Initialize(this);
    }

    private void TryStartNextWorldTask()
    {
        var worldTask = _worldTaskQueue.Dequeue();
        worldTask.OnTaskComplete += OnTaskComplete;
        worldTask.StartTimer();
    }

    private void OnTaskComplete()
    {
        if (_worldTaskQueue.Any())
            TryStartNextWorldTask();
    }

    public void StartTimer()
    {
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true);
        TryStartNextWorldTask();
    }
}
