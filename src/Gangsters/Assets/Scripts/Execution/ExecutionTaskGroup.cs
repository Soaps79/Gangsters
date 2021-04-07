using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World;

namespace Assets.Scripts.Execution
{
    [Serializable]
    public class ExecutionTaskGroup
    {
        public string CrewDisplayName;
        public List<ExecutionTask> ExecutionTasks = new List<ExecutionTask>();

        private readonly Queue<ExecutionTask> _worldTaskQueue = new Queue<ExecutionTask>();
        private ExecutionTask _currentExecutionTask;

        public Crew Crew { get; private set; }

        public ExecutionTaskGroup(Crew crew)
        {
            Crew = crew;
        }

        public void StartTasks()
        {
            ExecutionTasks.ForEach(i => _worldTaskQueue.Enqueue(i));
            TryStartNextWorldTask();
        }

        private void TryStartNextWorldTask()
        {
            if (!_worldTaskQueue.Any()) return;

            _currentExecutionTask = _worldTaskQueue.Dequeue();
            _currentExecutionTask.OnTaskComplete += OnTaskComplete;
            _currentExecutionTask.StartTimer();
        }

        private void OnTaskComplete(ExecutionTask executionTask)
        {
            TryStartNextWorldTask();
        }
    }
}