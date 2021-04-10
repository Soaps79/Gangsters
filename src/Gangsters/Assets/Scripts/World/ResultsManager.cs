using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class TaskResults
    {
        public string TaskName;
        public TaskOutcome TaskOutcome;
    }

    public interface IMoneyCollection
    {
        public void AcceptMoney(int amount);
    }

    public interface IPropertyCollection
    {
        public void UpdateProperty(WorldPropertySO property, WorldPropertyStatus status);
    }

    public class ResultsManager
    {
        public List<TaskResults> LastResults { get; } = new List<TaskResults>();
        private readonly IMoneyCollection _moneyCollection;
        private readonly IPropertyCollection _propertyCollection;

        public Action OnNextDistributionComplete;

        public ResultsManager(IMoneyCollection moneyCollection, IPropertyCollection propertyCollection)
        {
            _moneyCollection = moneyCollection;
            _propertyCollection = propertyCollection;
        }

        public bool HasResultsToBeProcessed => LastResults.Any();

        public void AddOutcome(string taskName, TaskOutcome taskOutcome)
        {
            LastResults.Add(new TaskResults { TaskName = taskName, TaskOutcome = taskOutcome });
        }

        public void Distribute(TaskResults results)
        {
            if(results.TaskOutcome.MoneyReward != 0)
                _moneyCollection.AcceptMoney(results.TaskOutcome.MoneyReward);

            if (results.TaskOutcome.PropertyUpdates.Any())
            {
                foreach (var pair in results.TaskOutcome.PropertyUpdates)
                {
                    _propertyCollection.UpdateProperty(pair.Property, pair.Status);
                }
            }

            LastResults.Remove(results);

            if (!LastResults.Any())
            {
                OnNextDistributionComplete?.Invoke();
                OnNextDistributionComplete = null;
            }
        }
    }
}