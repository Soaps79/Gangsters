namespace Assets.Scripts.World
{
    [System.Serializable]
    public class Crew
    {
        private CrewLeaderSO _crewLeaderSo;
        
        public string Id => _crewLeaderSo.name;
        public string CrewName => _crewLeaderSo.CrewName;
        public string LeaderName => _crewLeaderSo.FullName;

        public Crew(CrewLeaderSO leaderSo)
        {
            _crewLeaderSo = leaderSo;
        }

    }
}