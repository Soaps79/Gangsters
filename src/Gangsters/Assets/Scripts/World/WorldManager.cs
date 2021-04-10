using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World
{
    public class WorldManager : IPropertyCollection
    {
        public List<WorldPropertyState> Properties = new List<WorldPropertyState>();
        private readonly List<TaskTemplateSO> _taskTemplates;

        public Action OnPropertiesChanged;

        public WorldManager(List<TaskTemplateSO> taskTemplates)
        {
            _taskTemplates = taskTemplates;
        }

        public void Initialize(WorldStateSO worldState)
        {
            foreach (var property in worldState.Properties)
            {
                Properties.Add(new WorldPropertyState(property));
            }
        }

        public List<WorldTaskData> GetAvailableTasks()
        {
            var list = new List<WorldTaskData>();

            foreach (var propertyState in Properties)
            {
                foreach (var taskTemplate in _taskTemplates)
                {
                    var baseChance = taskTemplate.Chances.FirstOrDefault(i => i.Status == propertyState.Status);
                    if(baseChance == null) continue;
                    if (Random.value * 100 > baseChance.BasePercentChance) continue;

                    WorldTaskData task = null;
                    if (TryGenerateTask(taskTemplate, propertyState.WorldProperty, out task))
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
                TotalTime = 2f
            };
            return true;
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
                                Property = property,Status = WorldPropertyStatus.Extorted
                            } },
                        MoneyReward = property.ExtortionValue,
                    };
                case TaskType.Collect:
                    return new TaskOutcome {MoneyReward = property.ExtortionValue};
                default:
                    return null;
            }
        }

        public void UpdateProperty(WorldPropertySO property, WorldPropertyStatus status)
        {
            var propertyState = Properties.FirstOrDefault(i => i.WorldProperty == property);
            if (propertyState == null)
            {
                propertyState = new WorldPropertyState {WorldProperty = property, Status = status};
                Properties.Add(propertyState);
                Debug.Log($"WorldManager update called on unknown Property {property.DisplayName}");
            }

            propertyState.Status = status;
            OnPropertiesChanged?.Invoke();
        }
    }
}