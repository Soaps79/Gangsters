using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    [Serializable]
    public class ExecutionTaskGroup
    {
        public string CrewDisplayName;
        public List<ExecutionTask> ExecutionTasks = new List<ExecutionTask>();

        public TaskOutcome TaskOutcome = new TaskOutcome();

        private readonly Queue<ExecutionTask> _worldTaskQueue = new Queue<ExecutionTask>();
        private ExecutionTask _currentExecutionTask;

        public void StartTasks()
        {
            ExecutionTasks.ForEach(i => _worldTaskQueue.Enqueue(i));
            TryStartNextWorldTask();
        }

        private void TryStartNextWorldTask()
        {
            if (_worldTaskQueue.Any())
            {
                _currentExecutionTask = _worldTaskQueue.Dequeue();
                _currentExecutionTask.OnTaskComplete += OnTaskComplete;
                _currentExecutionTask.StartTimer();
            }
        }

        private void OnTaskComplete(ExecutionTask executionTask)
        {
            if (executionTask.TaskOutcome != null)
                TaskOutcome.Add(executionTask.TaskOutcome);

            TryStartNextWorldTask();
        }
    }
}