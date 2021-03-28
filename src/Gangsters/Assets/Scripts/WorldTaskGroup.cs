using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    [Serializable]
    public class WorldTaskGroup
    {
        public string CrewDisplayName;
        public List<WorldTask> WorldTasks = new List<WorldTask>();

        public TaskOutcome TaskOutcome = new TaskOutcome();

        private readonly Queue<WorldTask> _worldTaskQueue = new Queue<WorldTask>();
        private WorldTask _currentWorldTask;

        public void StartTasks()
        {
            WorldTasks.ForEach(i => _worldTaskQueue.Enqueue(i));
            TryStartNextWorldTask();
        }

        private void TryStartNextWorldTask()
        {
            if (_worldTaskQueue.Any())
            {
                _currentWorldTask = _worldTaskQueue.Dequeue();
                _currentWorldTask.OnTaskComplete += OnTaskComplete;
                _currentWorldTask.StartTimer();
            }
        }

        private void OnTaskComplete(WorldTask worldTask)
        {
            if (worldTask.TaskOutcome != null)
                TaskOutcome.Add(worldTask.TaskOutcome);

            TryStartNextWorldTask();
        }
    }
}