using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Execution;
using Assets.Scripts.Planning.UI;
using Assets.Scripts.World;
using QGame;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Planning
{
    public class PlanningPhase : QScript
    {
        public PlanningPhaseViewModel ViewModelPrefab;

        public List<CrewLeaderSO> TestLeaders;
        public List<WorldTaskData> TestTasks;
        public bool UseTestData;

        public Transform MainCanvas;
        public ResultsListViewModel ResultsListPrefab;
        
        public GangManager GangManager { get; private set; }
        public List<PlanningTask> PlanningTasks = new List<PlanningTask>();
        private readonly List<WorldTaskData> _availableWorldTasks = new List<WorldTaskData>();

        public void Start()
        {
            GangManager = Locator.GangManager;
            if (GangManager == null)
                throw new UnityException("PlanningPhase could not find a GangManager");

            CheckForTestData();

            var worldManager = Locator.WorldManager;
            if (worldManager == null)
                throw new UnityException("PlanningPhase could not find a WorldManager");

            // TODO: distribute rewards before generating tasks, make UI react better
            _availableWorldTasks.AddRange(worldManager.GetAvailableTasks());
            CreatePlanningTasks();

            InitializePhaseUI();
            CheckForResults();
        }

        private void InitializePhaseUI()
        {
            var viewModel = Instantiate(ViewModelPrefab, MainCanvas, false);
            viewModel.Initialize(this);
        }

        private void CheckForTestData()
        {
            if (!UseTestData) return;
            
            //if(!_availableWorldTasks.Any())
            //    _availableWorldTasks.AddRange(TestTasks);
            if(!GangManager.Crews.Any())
                GangManager.Crews = GetTestCrews();
        }

        private void CreatePlanningTasks()
        {
            foreach (var taskData in _availableWorldTasks)
            {
                PlanningTasks.Add(new PlanningTask(taskData));
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

        private void CheckForResults()
        {
            var resultsManager = ServiceLocator.Get<ResultsManager>();
            if (resultsManager.HasResultsToBeProcessed)
            {
                var viewModel = Instantiate(ResultsListPrefab, MainCanvas, false);
                viewModel.Initialize(resultsManager);
                viewModel.OnComplete += OnAcceptResultsComplete;
            }
        }

        private void OnAcceptResultsComplete(ResultsListViewModel viewModel)
        {
            viewModel.gameObject.SetActive(false);
            InitializePhaseUI();
        }

        private void PrepareForExecutionPhase()
        {
            var executionData = new ExecutionStartData();
            foreach (var planningTask in PlanningTasks.Where(i => i.IsComplete))
            {
                var plannedData = new PlannedTaskData
                {
                    CrewId = planningTask.SelectedCrew.Id,
                    WorldTaskData = planningTask.WorldTaskData
                };
                executionData.PlannedTasks.Add(plannedData);
            }
            ServiceLocator.Register<ExecutionStartData>(executionData);
        }

        public void StartExecutionPhase()
        {
            PrepareForExecutionPhase();
            SceneManager.LoadScene("ExecutionScene", LoadSceneMode.Single);
        }
    }
}