using System.Linq;
using Assets.Scripts.World;
using QGame;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Planning.UI
{
    public class PlanningPhaseViewModel : QScript
    {
        private PlanningPhase _planningPhase;

        public CrewViewModel CrewPrefab;
        public Transform CrewListTransform;

        public PlanningTaskListView TaskListView;
        
        public TMP_Text MoneyText;
        public TMP_Text PropertiesText;
        
        public void Initialize(PlanningPhase planningPhase)
        {
            _planningPhase = planningPhase;
            _planningPhase.GangManager.OnMoneyChanged += UpdateMoney;
            _planningPhase.WorldManager.OnPropertiesChanged += UpdateProperties;

            _planningPhase.OnTaskListUpdate += () =>
                TaskListView.UpdateList(_planningPhase.PlanningTasks);

            UpdateMoney();
            UpdateProperties();
         
            InitializeGangViews();
        }

        private void UpdateMoney()
        {
            MoneyText.text = $"${_planningPhase.GangManager.Money}";
        }

        private readonly WorldPropertyStatus[] _monitoredStatuses = 
            {WorldPropertyStatus.Extorted, WorldPropertyStatus.Owned};

        private void UpdateProperties()
        {
            var text = "";
            foreach (var status in _monitoredStatuses)
            {
                text += $"{status}:\n";

                var extortedProperties =
                    _planningPhase.WorldManager.Properties.Where(i => i.Status == status);

                if (extortedProperties.Any())
                {
                    text += string.Join("\n", extortedProperties.Select(i => i.WorldProperty.DisplayName));
                }

                text += "\n\n";
            }

            PropertiesText.text = text;
        }

        private void InitializeGangViews()
        {
            foreach (var crew in _planningPhase.GangManager.Crews)
            {
                var viewModel = Instantiate(CrewPrefab, CrewListTransform, false);
                viewModel.Initialize(crew);
            }
        }

        public void OnDestroy()
        {
            _planningPhase.GangManager.OnMoneyChanged -= UpdateMoney;
        }
    }
}