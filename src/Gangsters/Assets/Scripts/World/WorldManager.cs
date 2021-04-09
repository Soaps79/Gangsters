using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class WorldManager : IPropertyCollection
    {
        public List<WorldPropertyState> Properties = new List<WorldPropertyState>();
        private readonly List<TaskTemplateSO> _taskTemplates;

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

                    switch (taskTemplate.Type)
                    {
                        case TaskType.Extortion:
                            list.Add(GenerateExtortionTask(taskTemplate, propertyState.WorldProperty));
                            break;
                    }
                }
            }

            return list;
        }

        private static WorldTaskData GenerateExtortionTask(TaskTemplateSO taskTemplate, WorldPropertySO property)
        {
            var task = new WorldTaskData
            {
                DisplayName = $"{taskTemplate.Verb} {property.DisplayName}",
                Requirements = property.ExtortionRequirements,
                ExtortedProperties = new List<WorldPropertySO> {property},
                RewardMoney = property.ExtortionValue,
                TotalTime = 2f
            };
            return task;
        }

        public void ExtortProperty(WorldPropertySO property)
        {
            var propertyState = Properties.FirstOrDefault(i => i.WorldProperty == property);
            if (propertyState != null)
                propertyState.Status = WorldPropertyStatus.Extorted;
        }
    }
}