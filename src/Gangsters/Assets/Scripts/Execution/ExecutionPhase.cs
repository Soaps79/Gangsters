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

        public bool UseTestData;
        public ExecutionTask ExecutionTaskPrefab;

        public Canvas MainCanvas;
        public ResultsViewModel ResultsPrefab;

        public GangManager GangManager { get; private set; }

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
            //    foreach (var taskData in TestTasks)
            //    {
            //        CreateExecutionTaskGroup(taskData);
            //    }
            //}

            var startData = ServiceLocator.Get<ExecutionStartData>();
            if (startData != null)
            {
                GangManager = ServiceLocator.Get<GangManager>();

                var tasksByCrew = startData.PlannedTasks
                    .GroupBy(i => i.CrewId);

                foreach (var group in tasksByCrew)
                {
                    CreateExecutionTaskGroup(group.Key, group.ToList());
                }
            }

            ViewModel.Initialize(this);
        }

        private void CreateExecutionTaskGroup(string crewName, List<PlannedTaskData> plannedTasks)
        {
            var crew = GangManager.Crews.FirstOrDefault(i => i.Id == crewName);
            if (crew == null)
                throw new UnityException("ExecutionPhase cannot find crew");

            var group = new ExecutionTaskGroup(crew);
            foreach (var plannedTask in plannedTasks)
            {
                var executionTask = CreateExecutionTask(plannedTask.WorldTaskData, crew);
                group.ExecutionTasks.Add(executionTask);
            }
            ExecutionTaskGroups.Add(group);
        }

        private ExecutionTask CreateExecutionTask(WorldTaskData taskData, Crew crew)
        {
            var task = Instantiate(ExecutionTaskPrefab, transform);
            task.Initialize(taskData, crew);
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
            var resultsManager = ServiceLocator.Get<ResultsManager>();
            
            foreach (var taskGroup in ExecutionTaskGroups)
            {
                foreach (var worldTask in taskGroup.ExecutionTasks)
                {
                    resultsManager.AddOutcome(worldTask.DisplayName, worldTask.TaskOutcome);
                }
            }
            var resultsViewModel = Instantiate<ResultsViewModel>(ResultsPrefab, MainCanvas.transform, false);
            resultsViewModel.Initialize(this);
        }

        public void CompleteScene()
        {
            SceneManager.LoadScene("OfficeScene", LoadSceneMode.Single);
        }
    }
}
