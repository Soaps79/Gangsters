using QGame;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts
{
    public class ExecutionPhaseViewModel : QScript
    {
        public SliderBinding MainSlider;
        [FormerlySerializedAs("TaskListRectRectTransform")]
        public RectTransform TaskListRectTransform;
        public GameObject WorldTaskPrefab;
        private ExecutionPhase _executionPhase;

        public void Initialize(ExecutionPhase executionPhase)
        {
            _executionPhase = executionPhase;
            MainSlider.Initialize(() => _executionPhase.CurrentCraftElapsedAsZeroToOne);
            foreach (var worldTask in _executionPhase.WorldTasks)
            {
                
                var go = Instantiate(WorldTaskPrefab, TaskListRectTransform.transform, false);
                var viewModel = go.GetComponent<WorldTaskViewModel>();
                viewModel.Initialize(worldTask);
            }
        }
    }
}