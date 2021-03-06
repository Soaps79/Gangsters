using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class GangManager
    {
        public List<Crew> Crews = new List<Crew>();
        public Action OnCrewsUpdated;

        public Wallet Wallet { get; } = new Wallet();

        public List<Crew> GetAbleCrews(List<AttributeValuePair> requirements)
        {
            return Crews.Where(i => i.Attributes.MeetsRequirements(requirements)).ToList();
        }
    }
}