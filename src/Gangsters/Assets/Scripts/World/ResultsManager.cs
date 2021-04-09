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
        public void ExtortProperty(WorldPropertySO property);
    }

    public class ResultsManager
    {
        public List<TaskResults> LastResults { get; } = new List<TaskResults>();
        private readonly IMoneyCollection _moneyCollection;
        private readonly IPropertyCollection _propertyCollection;

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

            if (results.TaskOutcome.ExtortedProperties.Any())
            {
                foreach (var property in results.TaskOutcome.ExtortedProperties)
                {
                    _propertyCollection.ExtortProperty(property);
                }
            }
            LastResults.Remove(results);
        }
    }
}