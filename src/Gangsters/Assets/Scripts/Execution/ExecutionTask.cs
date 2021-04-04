using System;
using Assets.Scripts.World;
using QGame;
using UnityEngine;

namespace Assets.Scripts.Execution
{


    public class ExecutionTask : QScript
    {
        public float TotalTime;
        public string DisplayName;
        private const string STOPWATCH_KEY = "t";
        public TaskOutcome TaskOutcome;

        public bool IsComplete;

        public Action<ExecutionTask> OnTaskComplete;
        public Crew Crew { get; private set; }

        public float CurrentCraftElapsedAsZeroToOne
        {
            get
            {
                if (IsComplete) return 1f;
                return StopWatch.IsRunning()
                    ? StopWatch[STOPWATCH_KEY].ElapsedLifetimeAsZeroToOne : 0f;
            }
        }

        public void Initialize(WorldTaskData data, Crew crew)
        {
            if (data == null || crew == null)
                throw new UnityException();

            Crew = crew;
            TaskOutcome = new TaskOutcome
            {
                MoneyReward = data.RewardMoney
            };

            TotalTime = data.TotalTime;
            DisplayName = data.DisplayName;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        public void OnComplete()
        {
            OnTaskComplete?.Invoke(this);
            IsComplete = true;
        }

        public void StartTimer()
        {
            StopWatch.AddNode(STOPWATCH_KEY, TotalTime, true).OnTick = OnComplete;
        }
    }
}