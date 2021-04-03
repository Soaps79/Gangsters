using System.Collections.Generic;

namespace Assets.Scripts.World
{
    [System.Serializable]
    public class TaskOutcome
    {
        public int MoneyReward;
        public List<WorldProperty> GainedProperties = new List<WorldProperty>();

        public void Add(TaskOutcome taskOutcome)
        {
            MoneyReward += taskOutcome.MoneyReward;
            GainedProperties.AddRange(taskOutcome.GainedProperties);
        }
    }
}