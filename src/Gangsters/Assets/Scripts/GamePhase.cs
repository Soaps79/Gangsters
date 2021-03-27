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
    private WorldTask _currentWorldTask;
    private readonly Queue<WorldTask> _worldTaskQueue = new Queue<WorldTask>();

    public TaskOutcome TaskOutcome;

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

    public void StartTimer()
    {
        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true);
        TryStartNextWorldTask();
    }

    private void TryStartNextWorldTask()
    {
        if (_worldTaskQueue.Any())
        {
            _currentWorldTask = _worldTaskQueue.Dequeue();
            _currentWorldTask.OnTaskComplete += OnTaskComplete;
            _currentWorldTask.StartTimer();
        }
    }

    private void OnTaskComplete(WorldTask worldTask)
    {
        if(worldTask.TaskOutcome != null)
            TaskOutcome.Add(worldTask.TaskOutcome);
        
        TryStartNextWorldTask();
    }
}
