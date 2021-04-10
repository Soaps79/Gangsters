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
        public bool UseTestLeaders;
        
        public List<WorldTaskData> TestTasks;
        public bool UseTestTasks;

        public Transform MainCanvas;
        public ResultsListViewModel ResultsListPrefab;
        
        public GangManager GangManager { get; private set; }
        public List<PlanningTask> PlanningTasks = new List<PlanningTask>();
        public Action OnTaskListUpdate;

        private readonly List<WorldTaskData> _availableWorldTasks = new List<WorldTaskData>();
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

            InitializePhaseUI();

            if (CheckForResults())
                ServiceLocator.Get<ResultsManager>().OnNextDistributionComplete += () =>
                {
                    OnNextUpdate += ClearAndCreatePlanningTasks;
                };
            else
                ClearAndCreatePlanningTasks();
        }

        private bool CheckForResults()
        {
            var resultsManager = ServiceLocator.Get<ResultsManager>();
            if (resultsManager.HasResultsToBeProcessed)
            {
                var viewModel = Instantiate(ResultsListPrefab, MainCanvas, false);
                viewModel.Initialize(resultsManager);
                viewModel.OnComplete += OnAcceptResultsComplete;
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

        private void ClearAndCreatePlanningTasks()
        {
            _availableWorldTasks.Clear();
            if (UseTestTasks)
            {
                //if(!_availableWorldTasks.Any())
                //    _availableWorldTasks.AddRange(TestTasks);
            }

            _availableWorldTasks.AddRange(WorldManager.GetAvailableTasks());

            PlanningTasks.Clear();
            foreach (var taskData in _availableWorldTasks)
            {
                PlanningTasks.Add(new PlanningTask(taskData));
            }
            OnTaskListUpdate?.Invoke();
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

        private void OnAcceptResultsComplete(ResultsListViewModel viewModel)
        {
            Destroy(viewModel.gameObject);
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