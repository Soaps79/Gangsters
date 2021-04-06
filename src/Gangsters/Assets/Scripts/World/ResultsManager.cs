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

    public class ResultsManager
    {
        public List<TaskResults> LastResults { get; } = new List<TaskResults>();
        private readonly IMoneyCollection _moneyCollection;

        public ResultsManager(IMoneyCollection moneyCollection)
        {
            _moneyCollection = moneyCollection;
        }

        public bool HasResultsToBeProcessed => LastResults.Any();

        public void AddOutcome(string taskName, TaskOutcome taskOutcome)
        {
            LastResults.Add(new TaskResults { TaskName = taskName, TaskOutcome = taskOutcome });
        }

        public void Distribute(TaskResults results)
        {
            _moneyCollection.AcceptMoney(results.TaskOutcome.MoneyReward);
            LastResults.Remove(results);
        }
    }
}