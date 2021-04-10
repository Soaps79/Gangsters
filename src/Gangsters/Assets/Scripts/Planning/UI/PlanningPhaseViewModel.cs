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

        public PlanningTaskViewModel TaskPrefab;
        public Transform TaskListTransform;
        
        public TMP_Text MoneyText;
        public TMP_Text PropertiesText;
        
        public void Initialize(PlanningPhase planningPhase)
        {
            _planningPhase = planningPhase;
            _planningPhase.GangManager.OnMoneyChanged += UpdateMoney;
            _planningPhase.GangManager.OnPropertiesChanged += UpdateProperties;
            _planningPhase.OnTaskListUpdate += UpdateTaskViews;

            UpdateMoney();
            UpdateProperties();
         
            InitializeGangViews();
        }

        private void UpdateProperties()
        {
            var text = "Extorted Properties:\n";
            if (_planningPhase.GangManager.ExtortedProperties.Any())
            {
                text += string.Join("\n",  _planningPhase.GangManager.ExtortedProperties.Select(i => i.DisplayName));
            }

            PropertiesText.text = text;
        }

        private void UpdateMoney()
        {
            MoneyText.text = $"${_planningPhase.GangManager.Money}";
        }

        private void InitializeGangViews()
        {
            foreach (var crew in _planningPhase.GangManager.Crews)
            {
                var viewModel = Instantiate(CrewPrefab, CrewListTransform, false);
                viewModel.Initialize(crew);
            }
        }

        private void UpdateTaskViews()
        {
            TaskListTransform.gameObject.DestroyAllChildren();
            foreach (var planningTask in _planningPhase.PlanningTasks)
            {
                var viewModel = Instantiate(TaskPrefab, TaskListTransform, false);
                viewModel.Initialize(planningTask, _planningPhase.GangManager.GetAbleCrews(planningTask.WorldTaskData.Requirements));
            }
        }

        public void OnDestroy()
        {
            _planningPhase.GangManager.OnMoneyChanged -= UpdateMoney;
        }
    }
}