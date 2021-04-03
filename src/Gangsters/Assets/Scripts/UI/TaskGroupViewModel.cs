using QGame;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class TaskGroupViewModel : QScript
    {
        private ExecutionTaskGroup _executionTaskGroup;
        public WorldTaskViewModel WorldTaskPrefab;
        public Transform TaskListTransform;
        public TMP_Text GroupDisplayNameText;

        public void Initialize(ExecutionTaskGroup executionTaskGroup)
        {
            _executionTaskGroup = executionTaskGroup;

            foreach (var worldTask in _executionTaskGroup.ExecutionTasks)
            {
                var go = Instantiate(WorldTaskPrefab, TaskListTransform.transform, false);
                var viewModel = go.GetComponent<WorldTaskViewModel>();
                viewModel.Initialize(worldTask);
            }

            GroupDisplayNameText.text = executionTaskGroup.CrewDisplayName;
        }
    }
}