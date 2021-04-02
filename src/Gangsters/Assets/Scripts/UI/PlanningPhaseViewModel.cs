using QGame;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlanningPhaseViewModel : QScript
    {
        private PlanningPhase _planningPhase;
        public Transform CrewListTransform;
        public GameObject CrewPrefab;
        public TMP_Text AssetsText;

        public void Initialize(PlanningPhase planningPhase)
        {
            _planningPhase = planningPhase;
            foreach (var crew in _planningPhase.TestCrews)
            {
                var go = Instantiate(CrewPrefab, CrewListTransform, false);
                var viewModel = go.GetComponent<CrewViewModel>();
                viewModel.Initialize(crew);
            }

            AssetsText.text = $"${_planningPhase.Money}";
        }
    }
}