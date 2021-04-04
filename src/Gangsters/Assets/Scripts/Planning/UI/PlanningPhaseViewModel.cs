﻿using QGame;
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
        
        public TMP_Text AssetsText;
        
        public void Initialize(PlanningPhase planningPhase)
        {
            _planningPhase = planningPhase;
            AssetsText.text = $"${_planningPhase.Money}";
         
            InitializeGangViews();
            InitializeTaskViews();
        }

        private void InitializeGangViews()
        {
            foreach (var crew in _planningPhase.GangManager.Crews)
            {
                var viewModel = Instantiate(CrewPrefab, CrewListTransform, false);
                viewModel.Initialize(crew);
            }
        }

        private void InitializeTaskViews()
        {
            foreach (var planningTask in _planningPhase.PlanningTasks)
            {
                var viewModel = Instantiate(TaskPrefab, TaskListTransform, false);
                viewModel.Initialize(planningTask, _planningPhase.GangManager.GetAbleCrews(planningTask.WorldTaskData.Requirements));
            }
        }
    }
}