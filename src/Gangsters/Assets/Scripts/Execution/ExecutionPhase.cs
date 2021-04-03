using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Execution.UI;
using Assets.Scripts.Planning;
using Assets.Scripts.World;
using QGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Execution
{
    public class ExecutionPhase : QScript
    {
        public float TotalTime;
        private const string STOPWATCH_KEY = "phasetimer";
        public ExecutionPhaseViewModel ViewModel;

        public List<ExecutionTaskGroup> ExecutionTaskGroups;
        private ExecutionTask _currentExecutionTask;

        public List<WorldTaskDataGroup> TestData;
        public bool UseTestData;
        public ExecutionTask ExecutionTaskPrefab;

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
            //if (UseTestData)
            //{
            //    foreach (var taskData in TestData)
            //    {
            //        CreateExecutionTaskGroup(taskData);
            //    }
            //}

            var startData = ServiceLocator.Get<ExecutionStartData>();
            if (startData != null)
            {
                var tasksByCrew = startData.PlannedTasks
                    .GroupBy(i => i.CrewName);

                foreach (var group in tasksByCrew)
                {
                    CreateExecutionTaskGroup(group.Key, group.ToList());
                }
            }

            ViewModel.Initialize(this);
        }

        private void CreateExecutionTaskGroup(string crewName, List<PlannedTaskData> plannedTasks)
        {
            var group = new ExecutionTaskGroup {CrewDisplayName = crewName};
            foreach (var plannedTask in plannedTasks)
            {
                var executionTask = CreateExecutionTask(plannedTask.WorldTaskData);
                group.ExecutionTasks.Add(executionTask);
            }
            ExecutionTaskGroups.Add(group);
        }

        private ExecutionTask CreateExecutionTask(WorldTaskData taskData)
        {
            var task = Instantiate<ExecutionTask>(ExecutionTaskPrefab, transform);
            task.Initialize(taskData);
            task.DisplayName = taskData.DisplayName;
            task.TotalTime = taskData.TotalTime;
            return task;
        }

        public void StartTimer()
        {
            foreach (var worldTaskGroup in ExecutionTaskGroups)
            {
                worldTaskGroup.StartTasks();
            }

            StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick += OnTimeComplete;
        }

        private void OnTimeComplete()
        {
            foreach (var taskGroup in ExecutionTaskGroups)
            {
                foreach (var worldTask in taskGroup.ExecutionTasks)
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
}
