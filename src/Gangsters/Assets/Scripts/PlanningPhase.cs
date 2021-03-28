using System.Collections.Generic;
using QGame;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlanningPhase : QScript
    {
        public PlanningPhaseViewModel ViewModelPrefab;
        public List<Crew> Crews;

        public List<WorldTaskData> TestData;
        public bool UseTestData;

        public void Start()
        {
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
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 2f },
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 1.5f }
                            }
                        },
                        new WorldTaskDataGroup()
                        {
                            CrewDisplayName = "Crew Two",
                            WorldTasks = new List<WorldTaskData>
                            {
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 1f },
                                new WorldTaskData { DisplayName = "Collection Protection", TotalTime = 2f }
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