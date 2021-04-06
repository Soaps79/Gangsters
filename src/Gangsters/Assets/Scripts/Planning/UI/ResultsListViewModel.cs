using System;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Planning.UI
{
    public class ResultsListViewModel : MonoBehaviour
    {
        public ResultAcceptViewModel ResultAcceptPrefab;
        public Transform ResultListParent;

        private ResultsManager _resultsManager;

        public Action<ResultsListViewModel> OnComplete;

        public void Initialize(ResultsManager resultsManager)
        {
            _resultsManager = resultsManager;

            foreach (var result in _resultsManager.LastResults)
            {
                var viewModel = Instantiate(ResultAcceptPrefab, ResultListParent, false);
                viewModel.Initialize(result, resultsManager);
                viewModel.OnComplete += () =>
                {
                    viewModel.gameObject.SetActive(false);
                    if (!_resultsManager.HasResultsToBeProcessed)
                        OnComplete?.Invoke(this);
                };
            }
        }
    }
}
