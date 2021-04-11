using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World
{
    

    public class TaskTracker
    {
        private List<TaskTemplateSO> _taskTemplates;
        private GangManager _gangManager;
        private WorldManager _worldManager;
        public readonly List<AssignableTask> AllTasks = new List<AssignableTask>();

        public Action OnTaskListUpdated;

        public void Initialize(List<TaskTemplateSO> taskTemplates, GangManager gangManager, WorldManager worldManager)
        {
            _taskTemplates = taskTemplates;
            _worldManager = worldManager;
            
            _gangManager = gangManager;
            _gangManager.Wallet.OnMoneyChanged += AssessTaskAssignability;
            _gangManager.OnCrewsUpdated += AssessTaskAssignability;
        }

        public void GenerateTasks()
        {
            AllTasks.Clear();
            AllTasks.AddRange(GetAvailableTasks().Select(i => new AssignableTask(i)));
            if (AllTasks.Any())
            {
                AssessTaskAssignability();
                OnTaskListUpdated?.Invoke();
            }
        }

        private void AssessTaskAssignability()
        {
            foreach (var assignableTask in AllTasks)
            {
                var originalAssignability = assignableTask.IsAssignable;

                var canAfford = assignableTask.Task.Cost <= _gangManager.Wallet.AvailableMoney;
                var availableCrews = _gangManager.GetAbleCrews(assignableTask.Task.Requirements);
                if (availableCrews.Count != assignableTask.AvailableCrews.Count
                    || availableCrews.Any(i => !assignableTask.AvailableCrews.Contains(i)))
                {
                    assignableTask.AvailableCrews = availableCrews;
                    assignableTask.OnAvailableCrewsUpdated?.Invoke();
                }

                var isAssignable = true;
                var reason = "";
                if (!canAfford)
                {
                    isAssignable = false;
                    reason += "Cannot afford. ";
                }

                if (!assignableTask.AvailableCrews.Any())
                {
                    isAssignable = false;
                    reason += "No able crews. ";
                }

                assignableTask.SetIsAssignable(isAssignable, reason);
            }
        }

        public List<WorldTaskData> GetAvailableTasks()
        {
            var list = new List<WorldTaskData>();

            foreach (var propertyState in _worldManager.Properties)
            {
                foreach (var taskTemplate in _taskTemplates)
                {
                    var baseChance = taskTemplate.Chances.FirstOrDefault(i => i.Status == propertyState.Status);
                    if (baseChance == null) continue;
                    if (Random.value * 100 > baseChance.BasePercentChance) continue;

                    if (TryGenerateTask(taskTemplate, propertyState.WorldProperty, out var task))
                    {
                        list.Add(task);
                    }
                }
            }

            return list;
        }

        private static bool TryGenerateTask(TaskTemplateSO taskTemplate, WorldPropertySO property, out WorldTaskData task)
        {
            task = null;
            var taskOutcome = GenerateTaskOutcome(taskTemplate, property);
            if (taskOutcome == null) return false;

            task = new WorldTaskData
            {
                DisplayName = $"{taskTemplate.Verb} {property.DisplayName}",
                Requirements = property.ExtortionRequirements,
                TaskOutcome = taskOutcome,
                TotalTime = 2f,
                Cost = GenerateCost(taskTemplate, property)
            };
            return true;
        }

        private static int GenerateCost(TaskTemplateSO taskTemplate, WorldPropertySO property)
        {
            switch (taskTemplate.Type)
            {
                case TaskType.Purchase:
                    return property.PurchasePrice;
            }

            return 0;
        }

        private static TaskOutcome GenerateTaskOutcome(TaskTemplateSO taskTemplate, WorldPropertySO property)
        {
            switch (taskTemplate.Type)
            {
                case TaskType.Extortion:
                    return new TaskOutcome
                    {
                        PropertyUpdates = new List<PropertyStatusPair> {
                            new PropertyStatusPair
                            {
                                Property = property, Status = WorldPropertyStatus.Extorted
                            } },
                        MoneyReward = property.ExtortionValue,
                    };

                case TaskType.Collect:
                    return new TaskOutcome { MoneyReward = property.ExtortionValue };

                case TaskType.Purchase:
                    return new TaskOutcome
                    {
                        PropertyUpdates = new List<PropertyStatusPair> {
                            new PropertyStatusPair
                            {
                                Property = property, Status = WorldPropertyStatus.Owned
                            } }
                    };

                default:
                    return null;
            }
        }
    }

}