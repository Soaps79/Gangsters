using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using QGame;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExecutionPhase : QScript
{
    public float TotalTime;
    private const string STOPWATCH_KEY = "phasetimer";
    public ExecutionPhaseViewModel ViewModel;

    public List<WorldTaskGroup> WorldTaskGroups;
    private WorldTask _currentWorldTask;

    public List<WorldTaskDataGroup> TestData;
    public bool UseTestData;
    public WorldTask WorldTaskPrefab;

    public TaskOutcome TaskOutcome;

    public Canvas MainCanvas;
    public ResultsViewModel ResultsPrefab;

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
                CreateWorldTaskGroup(taskData);
            }
        }

        var startData = ServiceLocator.Get<ExecutionStartData>();
        if (startData != null)
        {
            foreach (var taskDataGroup in startData.WorldTasks)
            {
                CreateWorldTaskGroup(taskDataGroup);
            }
        }

        ViewModel.Initialize(this);
    }

    private void CreateWorldTaskGroup(WorldTaskDataGroup taskDataGroup)
    {
        var group = new WorldTaskGroup {CrewDisplayName = taskDataGroup.CrewDisplayName};
        foreach (var worldTaskData in taskDataGroup.WorldTasks)
        {
            var worldTask = CreateWorldTask(worldTaskData);
            group.WorldTasks.Add(worldTask);
        }
        WorldTaskGroups.Add(group);
    }

    private WorldTask CreateWorldTask(WorldTaskData taskData)
    {
        var task = Instantiate<WorldTask>(WorldTaskPrefab, transform);
        task.Initialize(taskData);
        task.DisplayName = taskData.DisplayName;
        task.TotalTime = taskData.TotalTime;
        return task;
    }

    public void StartTimer()
    {
        foreach (var worldTaskGroup in WorldTaskGroups)
        {
            worldTaskGroup.StartTasks();
        }

        StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick += OnTimeComplete;
    }

    private void OnTimeComplete()
    {
        foreach (var taskGroup in WorldTaskGroups)
        {
            foreach (var worldTask in taskGroup.WorldTasks)
            {
                TaskOutcome.Add(worldTask.TaskOutcome);
            }
        }
        var resultsViewModel = Instantiate<ResultsViewModel>(ResultsPrefab, MainCanvas.transform, false);
        resultsViewModel.Initialize(this);
        var gangManager = ServiceLocator.Get<GangManager>();
        if (gangManager != null)
            gangManager.Money += TaskOutcome.MoneyReward;
    }

    public void CompleteScene()
    {
        SceneManager.LoadScene("OfficeScene", LoadSceneMode.Single);
    }
}
