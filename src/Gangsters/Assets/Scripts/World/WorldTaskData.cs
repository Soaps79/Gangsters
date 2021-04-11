using System;
using System.Collections.Generic;

namespace Assets.Scripts.World
{
    [Serializable]
    public class WorldTaskData
    {
        public float TotalTime;
        public string DisplayName;
        public int Cost;
        public List<AttributeValuePair> Requirements = new List<AttributeValuePair>();
        public TaskOutcome TaskOutcome = new TaskOutcome();
    }
}