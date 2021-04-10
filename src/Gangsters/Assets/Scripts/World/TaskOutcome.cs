using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assets.Scripts.World
{
    public class PropertyStatusPair
    {
        public WorldPropertySO Property;
        public WorldPropertyStatus Status;
    }

    [System.Serializable]
    public class TaskOutcome
    {
        public int MoneyReward;
        public List<PropertyStatusPair> PropertyUpdates = new List<PropertyStatusPair>();

        public void Add(TaskOutcome taskOutcome)
        {
            MoneyReward += taskOutcome.MoneyReward;
            PropertyUpdates.AddRange(taskOutcome.PropertyUpdates);
        }
    }
}