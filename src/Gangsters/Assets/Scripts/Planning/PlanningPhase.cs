using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Execution;
using Assets.Scripts.Planning.UI;
using Assets.Scripts.World;
using QGame;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Planning
{
    public class PlanningPhase : QScript
    {
        public PlanningPhaseViewModel ViewModelPrefab;

        public List<CrewLeaderSO> TestLeaders;
        public bool UseTestLeaders;
        
        public List<WorldTaskData> TestTasks;
        public bool UseTestTasks;

        public Transform MainCanvas;
        public ResultsListViewModel ResultsListPrefab;
        
        public GangManager GangManager { get; private set; }

        public TaskTracker TaskTracker { get; private set; }
        //public List<AssignableTask> PlanningTasks = new List<AssignableTask>();
        //public Action OnTaskListUpdate;

        //private readonly List<WorldTaskData> _availableWorldTasks = new List<WorldTaskData>();
        public WorldManager WorldManager { get; private set; }

        public void Start()
        {
            GangManager = Locator.GangManager;
            if (GangManager == null)
                throw new UnityException("PlanningPhase could not find a GangManager");

            CheckForTestCrews();

            WorldManager = Locator.WorldManager;
            if (WorldManager == null)
                throw new UnityException("PlanningPhase could not find a WorldManager");

            TaskTracker = new TaskTracker();
            TaskTracker.Initialize(WorldManager.TaskTemplates, GangManager, WorldManager);

            InitializePhaseUI();

            if (CheckForResults())
                ServiceLocator.Get<ResultsManager>().OnNextDistributionComplete += () =>
                {
                    OnNextUpdate += GenerateTasks;
                };
            else
                GenerateTasks();
        }

        private bool CheckForResults()
        {
            var resultsManager = ServiceLocator.Get<ResultsManager>();
            if (resultsManager.HasResultsToBeProcessed)
            {
                var viewModel = Instantiate(ResultsListPrefab, MainCanvas, false);
                viewModel.Initialize(resultsManager);
                return true;
            }

            return false;
        }

        private void InitializePhaseUI()
        {
            var viewModel = Instantiate(ViewModelPrefab, MainCanvas, false);
            viewModel.Initialize(this);
        }

        private void CheckForTestCrews()
        {
            if (!UseTestLeaders) return;

            if(!GangManager.Crews.Any())
                GangManager.Crews = GetTestCrews();
        }

        private void GenerateTasks()
        {
            TaskTracker.GenerateTasks();

            if (UseTestTasks)
            {
                // add to tracker
            }
        }

        private List<Crew> GetTestCrews()
        {
            var crews = new List<Crew>();
            foreach (var leaderSo in TestLeaders)
            {
                var crew = new Crew(leaderSo);
                crews.Add(crew);
            }

            return crews;
        }

        private void PrepareForExecutionPhase()
        {
            var executionData = new ExecutionStartData();
            foreach (var planningTask in TaskTracker.AllTasks.Where(i => i.IsReady))
            {
                if (planningTask.Task.Cost > 0)
                    GangManager.Wallet.AcceptMoney(-planningTask.Task.Cost);

                var plannedData = new PlannedTaskData
                {
                    CrewId = planningTask.AssignedCrew.Id,
                    WorldTaskData = planningTask.Task
                };
                executionData.PlannedTasks.Add(plannedData);
            }
            ServiceLocator.Register<ExecutionStartData>(executionData);

            GangManager.Wallet.ClearReservation();
        }

        // Called from editor UI
        public void StartExecutionPhase()
        {
            PrepareForExecutionPhase();
            SceneManager.LoadScene("ExecutionScene", LoadSceneMode.Single);
        }
    }
}