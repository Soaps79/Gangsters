using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Execution;
using Assets.Scripts.Planning.UI;
using Assets.Scripts.World;
using QGame;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Planning
{
    public class PlanningPhase : QScript
    {
        public PlanningPhaseViewModel ViewModelPrefab;

        public List<CrewLeaderSO> TestLeaders;
        public List<WorldTaskData> TestTasks;
        public bool UseTestData;

        public GangManager GangManager { get; private set; }
        public List<PlanningTask> PlanningTasks = new List<PlanningTask>();
        private readonly List<WorldTaskData> _availableWorldTasks = new List<WorldTaskData>();
        public int Money;
        
        public void Start()
        {
            CheckForTestTasks();
            CreatePlanningTasks();
            GetOrCreateGangManager();

            Money = GangManager.Money;
            ViewModelPrefab.Initialize(this);
        }

        private void CheckForTestTasks()
        {
            if (UseTestData)
            {
                _availableWorldTasks.AddRange(TestTasks);
            }
        }

        private void CreatePlanningTasks()
        {
            foreach (var taskData in _availableWorldTasks)
            {
                PlanningTasks.Add(new PlanningTask(taskData));
            }
        }

        private void GetOrCreateGangManager()
        {
            GangManager = ServiceLocator.Get<GangManager>();
            if (GangManager == null)
            {
                GangManager = new GangManager
                {
                    Crews = GetTestCrews()
                };
                ServiceLocator.Register<GangManager>(GangManager);
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