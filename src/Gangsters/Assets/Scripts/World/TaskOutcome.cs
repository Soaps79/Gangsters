using System.Collections.Generic;

namespace Assets.Scripts.World
{
    [System.Serializable]
    public class TaskOutcome
    {
        public int MoneyReward;
        public List<WorldPropertySO> ExtortedProperties = new List<WorldPropertySO>();

        public void Add(TaskOutcome taskOutcome)
        {
            MoneyReward += taskOutcome.MoneyReward;
            ExtortedProperties.AddRange(taskOutcome.ExtortedProperties);
        }
    }
}