using System.Collections.Generic;
using QGame;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlanningPhase : QScript
    {
        public PlanningPhaseViewModel ViewModelPrefab;
        public List<Crew> TestCrews;

        public List<WorldTaskData> TestData;
        public bool UseTestData;
        private GangManager _gangManager;
        public int Money;

        public void Start()
        {
            _gangManager = ServiceLocator.Get<GangManager>();
            if (_gangManager == null)
            {
                _gangManager = new GangManager
                {
                    Crews = TestCrews
                };
                ServiceLocator.Register<GangManager>(_gangManager);
            }

            Money = _gangManager.Money;
            ViewModelPrefab.Initialize(this);
        }

        public void StartExecutionPhase()
        {
            if (UseTestData)
            {
                var startData = new ExecutionStartData()
                {
                    WorldTasks = new List<WorldTaskDataGroup>
                    {
                        new WorldTaskDataGroup()
                        {
                            CrewDisplayName = "Crew One",
                            WorldTasks = new List<WorldTaskData>
                            {
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 2f, RewardMoney = 50},
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 1.5f, RewardMoney = 35 }
                            }
                        },
                        new WorldTaskDataGroup()
                        {
                            CrewDisplayName = "Crew Two",
                            WorldTasks = new List<WorldTaskData>
                            {
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 1f, RewardMoney = 20 },
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 2f, RewardMoney = 50 }
                            }
                        }
                        
                    }
                };
                ServiceLocator.Register<ExecutionStartData>(startData);
            }
            SceneManager.LoadScene("ExecutionScene", LoadSceneMode.Single);
        }
    }
}