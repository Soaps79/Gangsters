using System.Linq;
using Assets.Scripts;
using QGame;
using TMPro;
using UnityEngine.UI;

public class ResultsViewModel : QScript
{
    public TMP_Text RewardsText;
    public Button FinishButton;


    public void Initialize(ExecutionPhase executionPhase)
    {
        RewardsText.text = GetResultsText(executionPhase.TaskOutcome);
        FinishButton.onClick.AddListener(executionPhase.CompleteScene);
    }

    private string GetResultsText(TaskOutcome taskOutcome)
    {
        string s = "";
        if (taskOutcome.MoneyReward != 0)
            s += "Money: " + taskOutcome.MoneyReward + "\n";
        if (taskOutcome.GainedProperties.Any())
        {
            s += "Properties Gained:\n";
            foreach (var gainedProperty in taskOutcome.GainedProperties)
            {
                s += $"{gainedProperty.Name} ({gainedProperty.ExtorionValue})\n";
            }
        }

        return s;
    }
}
