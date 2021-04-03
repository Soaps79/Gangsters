using QGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts
{
    public class ExecutionPhaseViewModel : QScript
    {
        public SliderBinding MainSlider;
        [FormerlySerializedAs("TaskListRectRectTransform")]
        public RectTransform TaskListRectTransform;

        public TaskGroupViewModel TaskGroupPrefab;
        private ExecutionPhase _executionPhase;

        public void Initialize(ExecutionPhase executionPhase)
        {
            _executionPhase = executionPhase;
            MainSlider.Initialize(() => _executionPhase.CurrentCraftElapsedAsZeroToOne);
            foreach (var taskGroup in _executionPhase.ExecutionTaskGroups)
            {
                var viewModel = Instantiate<TaskGroupViewModel>(TaskGroupPrefab, TaskListRectTransform.transform, false);
                viewModel.Initialize(taskGroup);
            }
        }
    }
}