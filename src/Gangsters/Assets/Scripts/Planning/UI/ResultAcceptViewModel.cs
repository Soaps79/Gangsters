using System;
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

        public void Initialize(TaskResults taskResults, ResultsManager resultsManager)
        {
            DisplayText.text = $"{taskResults.TaskName}\n";
            if(taskResults.TaskOutcome.MoneyReward != 0)
                DisplayText.text += $"${taskResults.TaskOutcome.MoneyReward}";

            var button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                resultsManager.Distribute(taskResults);
                BeginFadeOut();
            });
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
