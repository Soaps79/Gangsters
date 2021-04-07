using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class GangManager : IMoneyCollection
    {
        public int Money { get; private set; }
        public List<Crew> Crews = new List<Crew>();
        public List<WorldProperty> Properties = new List<WorldProperty>();

        public Action<int> OnMoneyChanged;

        public List<Crew> GetAbleCrews(List<AttributeValuePair> requirements)
        {
            return Crews.Where(i => i.Attributes.MeetsRequirements(requirements)).ToList();
        }

        public void AcceptMoney(int amount)
        {
            Money += amount;
            OnMoneyChanged?.Invoke(Money);
        }
    }
}