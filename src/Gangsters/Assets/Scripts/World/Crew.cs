using UnityEngine;

namespace Assets.Scripts.World
{
    [System.Serializable]
    public class Crew
    {
        private CrewLeaderSO _crewLeaderSo;
        
        public string Id => _crewLeaderSo.name;
        public string CrewName => _crewLeaderSo.CrewName;
        public string LeaderName => _crewLeaderSo.FullName;
        public Sprite LeaderPortraitSprite => _crewLeaderSo.PortraitSprite;
        public AttributeContainer Attributes { get; } = new AttributeContainer();

        public Crew(CrewLeaderSO leaderSo)
        {
            _crewLeaderSo = leaderSo;
            RefreshAttributes();
        }

        private void RefreshAttributes()
        {
            Attributes.Clear();
            foreach (var valuePair in _crewLeaderSo.BaseAttributes)
            {
                Attributes.AddValue(valuePair);
            }
        }
    }
}