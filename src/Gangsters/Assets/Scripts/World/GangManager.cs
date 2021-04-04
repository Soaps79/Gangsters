using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class GangManager
    {
        public int Money;
        public List<Crew> Crews = new List<Crew>();
        public List<WorldProperty> Properties = new List<WorldProperty>();

        public List<Crew> GetAbleCrews(List<AttributeValuePair> requirements)
        {
            return Crews.Where(i => i.Attributes.MeetsRequirements(requirements)).ToList();
        }
    }
}