using Assets.Scripts.World;
using QGame;
using TMPro;

namespace Assets.Scripts.Planning.UI
{
    public class CrewViewModel : QScript
    {
        public TMP_Text LeaderNameText;
        public TMP_Text CrewNameText;
        private Crew _crew;

        public void Initialize(Crew crew)
        {
            _crew = crew;
            LeaderNameText.text = _crew.LeaderName;
            CrewNameText.text = _crew.CrewName;
        }
    }
}