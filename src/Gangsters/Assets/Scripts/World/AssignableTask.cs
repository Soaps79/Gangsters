using System;
using System.Collections.Generic;

namespace Assets.Scripts.World
{
    public class AssignableTask
    {
        public WorldTaskData Task { get; }

        public List<Crew> AvailableCrews = new List<Crew>();
        public Crew AssignedCrew;

        public bool IsAssignable { get; private set; }
        public string UnassignableReason { get; private set; }

        public bool IsReady => IsAssignable && AssignedCrew != null;
        
        public Action OnAvailableCrewsUpdated;
        public Action OnAssignedCrewUpdated;
        public Action OnIsAssignableUpdated;
        public Action OnIsReadyUpdated;
        
        public AssignableTask(WorldTaskData taskData)
        {
            Task = taskData;
        }

        public void SetCrew(Crew crew)
        {
            if (AssignedCrew == crew)
                return;

            var wasReady = IsReady;
            AssignedCrew = crew;

            if(wasReady != IsReady)
                OnIsReadyUpdated?.Invoke();
            OnAssignedCrewUpdated?.Invoke();
        }

        public void SetIsAssignable(bool isAssignable, string reason)
        {
            UnassignableReason = reason;
            if (IsAssignable == isAssignable) 
                return;

            var wasReady = IsReady;
            IsAssignable = isAssignable;
            
            if (wasReady != IsReady)
                OnIsReadyUpdated?.Invoke();
            OnIsAssignableUpdated?.Invoke();
        }
    }
}