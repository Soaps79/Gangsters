using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlanningTask
    {
        public Crew SelectedCrew { get; private set; }
        public WorldTaskData WorldTaskData { get; private set; }
        public Action<PlanningTask> OnCrewChanged;
        public string DisplayName => WorldTaskData.DisplayName;
        public string TimeAsString => $"{WorldTaskData.TotalTime}";

        public PlanningTask(WorldTaskData worldTask)
        {
            WorldTaskData = worldTask;
        }

        public void SetCrew(Crew crew)
        {
            SelectedCrew = crew;
            OnCrewChanged?.Invoke(this);
        }
    }
}