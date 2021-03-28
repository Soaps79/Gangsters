using QGame;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts
{
    public class GamePhaseViewModel : QScript
    {
        public SliderBinding MainSlider;
        [FormerlySerializedAs("TaskListRectRectTransform")]
        public RectTransform TaskListRectTransform;
        public GameObject WorldTaskPrefab;
        public Canvas Canvas;
        private GamePhase _gamePhase;

        public void Initialize(GamePhase gamePhase)
        {
            _gamePhase = gamePhase;
            MainSlider.Initialize(() => _gamePhase.CurrentCraftElapsedAsZeroToOne);
            foreach (var worldTask in _gamePhase.WorldTasks)
            {
                
                var go = Instantiate(WorldTaskPrefab, TaskListRectTransform.transform, false);
                var viewModel = go.GetComponent<WorldTaskViewModel>();
                viewModel.Initialize(worldTask);
            }
        }
    }
}