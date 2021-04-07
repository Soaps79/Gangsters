using System;
using System.Collections.Generic;

namespace Assets.Scripts.World
{
    [Serializable]
    public class WorldTaskData
    {
        public float TotalTime;
        public string DisplayName;
        public int RewardMoney;
        public List<WorldPropertySO> ExtortedProperties = new List<WorldPropertySO>();
        public List<AttributeValuePair> Requirements = new List<AttributeValuePair>();
    }
}