using QGame;
using UnityEngine;

namespace Assets.Scripts
{
    public class TaskGroupViewModel : QScript
    {
        private WorldTaskGroup _worldTaskDataGroup;
        public WorldTaskViewModel WorldTaskPrefab;
        public Transform TaskListTransform;

        public void Initialize(WorldTaskGroup worldTaskDataGroup)
        {
            _worldTaskDataGroup = worldTaskDataGroup;

            foreach (var worldTask in _worldTaskDataGroup.WorldTasks)
            {
                var go = Instantiate(WorldTaskPrefab, TaskListTransform.transform, false);
                var viewModel = go.GetComponent<WorldTaskViewModel>();
                viewModel.Initialize(worldTask);
            }
        }
    }
}