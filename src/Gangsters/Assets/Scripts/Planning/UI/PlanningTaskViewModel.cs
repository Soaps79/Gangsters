using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World;
using QGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    public class PlanningTaskViewModel : QScript
    {
        public TMP_Dropdown Dropdown;
        public TMP_Text TaskNameText;
        private List<Crew> AvailableCrews;
        private PlanningTask _planningTask;
        private const string _emptySelectionText = "None";
        public Toggle ReadyToggle;

        public void Initialize(PlanningTask planningTask, List<Crew> availableCrews)
        {
            AvailableCrews = availableCrews;
            _planningTask = planningTask;

            TaskNameText.text = $"{_planningTask.DisplayName} : ${_planningTask.WorldTaskData.RewardMoney}";
            Dropdown.options.Add(new TMP_Dropdown.OptionData(_emptySelectionText));
            foreach (var availableCrew in AvailableCrews)
            {
                Dropdown.options.Add(new TMP_Dropdown.OptionData(availableCrew.CrewName));
            }
            Dropdown.onValueChanged.AddListener(OnDropdownSelectionChanged);
            UpdateReadyToggle();
        }

        private void OnDropdownSelectionChanged(int arg0)
        {
            var crewName = Dropdown.options[arg0].text;
            if (crewName == _emptySelectionText)
            {
                _planningTask.SetCrew(null);
            }
            else
            {
                var crew = AvailableCrews.FirstOrDefault(i => i.CrewName == crewName);
                if (crew == null)
                    throw new UnityException("Invalid crew selected from dropdown");
                _planningTask.SetCrew(crew);
            }

            UpdateReadyToggle();
        }

        private void UpdateReadyToggle()
        {
            ReadyToggle.isOn = _planningTask.SelectedCrew != null;
        }
    }
}