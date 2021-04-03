namespace Assets.Scripts.World
{
    [System.Serializable]
    public class Crew
    {
        public string Id => _crewLeaderSo.name;
        private CrewLeaderSO _crewLeaderSo;

        public string CrewFullName => $"{_crewLeaderSo.FullName}'s Crew";
        public string CrewShortName => _crewLeaderSo.NickName;

        public Crew(CrewLeaderSO leaderSo)
        {
            _crewLeaderSo = leaderSo;
        }

    }
}