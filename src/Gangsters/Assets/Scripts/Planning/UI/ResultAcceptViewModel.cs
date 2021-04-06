using System;
using Assets.Scripts.World;
using QGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    [RequireComponent(typeof(Button))]
    public class ResultAcceptViewModel : QScript
    {
        public TMP_Text DisplayText;

        public Action OnComplete;

        public void Initialize(TaskResults taskResults, ResultsManager resultsManager)
        {
            DisplayText.text = $"{taskResults.TaskName}\n";
            if(taskResults.TaskOutcome.MoneyReward != 0)
                DisplayText.text += $"${taskResults.TaskOutcome.MoneyReward}";

            var button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                resultsManager.Distribute(taskResults);
                    OnComplete?.Invoke();
            });
        }

    
    }
}
