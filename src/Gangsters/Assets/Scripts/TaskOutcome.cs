namespace Assets.Scripts
{
    [System.Serializable]
    public class TaskOutcome
    {
        public int MoneyReward;

        public void Add(TaskOutcome taskOutcome)
        {
            MoneyReward += taskOutcome.MoneyReward;
        }
    }
}