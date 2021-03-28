using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using QGame;

public class ExecutionPhase : QScript
{
    public float TotalTime;
    private const string STOPWATCH_KEY = "phasetimer";
    public ExecutionPhaseViewModel ViewModel;

    public List<WorldTask> WorldTasks;
    private WorldTask _currentWorldTask;
    private readonly Queue<WorldTask> _worldTaskQueue = new Queue<WorldTask>();

    public List<WorldTaskData> TestData;
    public WorldTask WorldTaskPrefab;
    public bool UseTestData;

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
        if (UseTestData)
        {
            foreach (var taskData in TestData)
            {
                CreateWorldTask(taskData);
            }
        }

        var startData = ServiceLocator.Get<ExecutionStartData>();
        if (startData != null)
        {
            foreach (var taskData in startData.WorldTasks)
            {
                CreateWorldTask(taskData);
            }
        }

        ViewModel.Initialize(this);
    }

    private void CreateWorldTask(WorldTaskData taskData)
    {
        var task = Instantiate<WorldTask>(WorldTaskPrefab, transform);
        task.DisplayName = taskData.DisplayName;
        task.TotalTime = taskData.TotalTime;
        WorldTasks.Add(task);
        _worldTaskQueue.Enqueue(task);
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
