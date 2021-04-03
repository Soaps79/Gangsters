using QGame;
using TMPro;

namespace Assets.Scripts
{
    public class CrewViewModel : QScript
    {
        public TMP_Text DisplayText;
        private Crew _crew;

        public void Initialize(Crew crew)
        {
            _crew = crew;
            DisplayText.text = _crew.CrewName;
        }
    }
}