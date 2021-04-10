using System;
using System.Linq;
using Assets.Scripts.World;
using DG.Tweening;
using QGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class ResultAcceptViewModel : QScript
    {
        public TMP_Text DisplayText;

        public Action OnComplete;
        private TaskResults _taskResults;

        public void Initialize(TaskResults taskResults, ResultsManager resultsManager)
        {
            _taskResults = taskResults;
            SetTaskName();
            SetMoneyAmount();
            SetExtortionRewards();

            var button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                resultsManager.Distribute(taskResults);
                BeginFadeOut();
            });
        }

        private void SetTaskName()
        {
            DisplayText.text = $"{_taskResults.TaskName}\n";
        }

        private void SetMoneyAmount()
        {
            DisplayText.text += $"${_taskResults.TaskOutcome.MoneyReward}";
        }

        private void SetExtortionRewards()
        {
            if (!_taskResults.TaskOutcome.PropertyUpdates.Any()) return;

            DisplayText.text += _taskResults.TaskOutcome.PropertyUpdates.Aggregate(
                "", (current, pair) => current + $"{pair.Property.DisplayName} : {pair.Status}     ");
        }

        public void BeginFadeOut()
        {
            var image = GetComponent<CanvasGroup>();
            image.DOFade(0f, .5f).onComplete += () =>
            {
                OnComplete?.Invoke();
                gameObject.SetActive(false);
            };
        }
    }
}
