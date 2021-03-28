using QGame;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlanningPhaseViewModel : QScript
    {
        private PlanningPhase _planningPhase;
        public Transform CrewListTransform;
        public GameObject CrewPrefab;

        public void Initialize(PlanningPhase planningPhase)
        {
            _planningPhase = planningPhase;
            foreach (var crew in _planningPhase.Crews)
            {
                var go = Instantiate(CrewPrefab, CrewListTransform, false);
                var viewModel = go.GetComponent<CrewViewModel>();
                viewModel.Initialize(crew);
            }
        }
    }
}