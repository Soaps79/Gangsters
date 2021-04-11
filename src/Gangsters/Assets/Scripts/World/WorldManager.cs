using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class WorldManager : IPropertyCollection
    {
        public List<WorldPropertyState> Properties = new List<WorldPropertyState>();

        // TODO: Find a better home for these
        public readonly List<TaskTemplateSO> TaskTemplates;

        public Action OnPropertiesChanged;

        public WorldManager(List<TaskTemplateSO> taskTemplates)
        {
            TaskTemplates = taskTemplates;
        }

        public void Initialize(WorldStateSO worldState)
        {
            foreach (var property in worldState.Properties)
            {
                Properties.Add(new WorldPropertyState(property));
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