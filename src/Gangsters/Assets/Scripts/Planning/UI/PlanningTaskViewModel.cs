using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World;
using QGame;
using TMPro;
using UIWidgets;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    

    public class PlanningTaskViewModel : ListViewItem, IViewData<AssignableTask>
    {
        public TMP_Dropdown Dropdown;
        public TMP_Text TaskNameText;
        private const string _emptySelectionText = "None";
        public Toggle ReadyToggle;
        private AssignableTask _assignableTask;

        public void SetData(AssignableTask assignableTask)
        {
            Initialize(assignableTask);
        }

        public void Initialize(AssignableTask assignableTask)
        {
            _assignableTask = assignableTask;
            _assignableTask.OnIsAssignableUpdated += UpdateReadyToggle;
            _assignableTask.OnAvailableCrewsUpdated += SetDropdownOptions;
            
            TaskNameText.text = $"{_assignableTask.Task.DisplayName} : ${_assignableTask.Task.TaskOutcome.MoneyReward}";
            Dropdown.onValueChanged.AddListener(OnDropdownSelectionChanged);
            
            SetDropdownOptions();
            UpdateReadyToggle();
        }

        private void SetDropdownOptions()
        {
            Dropdown.options.Add(new TMP_Dropdown.OptionData(_emptySelectionText));
            foreach (var availableCrew in _assignableTask.AvailableCrews)
            {
                Dropdown.options.Add(new TMP_Dropdown.OptionData(availableCrew.CrewName));
            }
        }

        private void OnDropdownSelectionChanged(int arg0)
        {
            var crewName = Dropdown.options[arg0].text;
            if (crewName == _emptySelectionText)
            {
                _assignableTask.AssignedCrew = null;
            }
            else
            {
                var crew = _assignableTask.AvailableCrews.FirstOrDefault(i => i.CrewName == crewName);
                if (crew == null)
                    throw new UnityException("Invalid crew selected from dropdown");
                _assignableTask.SetCrew(crew);
            }

            UpdateReadyToggle();
        }

        private void UpdateReadyToggle()
        {
            ReadyToggle.isOn = _assignableTask.IsReady;
        }
    }
}