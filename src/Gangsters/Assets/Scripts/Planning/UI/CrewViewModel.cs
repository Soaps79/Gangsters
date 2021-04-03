using Assets.Scripts.World;
using QGame;
using TMPro;

namespace Assets.Scripts.Planning.UI
{
    public class CrewViewModel : QScript
    {
        public TMP_Text DisplayText;
        private Crew _crew;

        public void Initialize(Crew crew)
        {
            _crew = crew;
            DisplayText.text = _crew.CrewFullName;
        }
    }
}