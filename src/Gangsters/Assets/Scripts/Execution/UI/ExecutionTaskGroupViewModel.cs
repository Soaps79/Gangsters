using QGame;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Execution.UI
{
    public class ExecutionTaskGroupViewModel : QScript
    {
        private ExecutionTaskGroup _executionTaskGroup;
        public ExecutionTaskViewModel ExecutionTaskPrefab;
        public Transform TaskListTransform;
        public TMP_Text GroupDisplayNameText;

        public void Initialize(ExecutionTaskGroup executionTaskGroup)
        {
            _executionTaskGroup = executionTaskGroup;

            foreach (var worldTask in _executionTaskGroup.ExecutionTasks)
            {
                var go = Instantiate(ExecutionTaskPrefab, TaskListTransform.transform, false);
                var viewModel = go.GetComponent<ExecutionTaskViewModel>();
                viewModel.Initialize(worldTask);
            }

            GroupDisplayNameText.text = executionTaskGroup.CrewDisplayName;
        }
    }
}