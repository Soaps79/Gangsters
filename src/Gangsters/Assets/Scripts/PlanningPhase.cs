using System.Collections.Generic;
using QGame;

namespace Assets.Scripts
{
    public class PlanningPhase : QScript
    {
        public PlanningPhaseViewModel ViewModelPrefab;
        public List<Crew> Crews;

        public void Start()
        {
            ViewModelPrefab.Initialize(this);
        }
    }
}