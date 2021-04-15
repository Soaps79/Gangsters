using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class AttributeContainer
    {
        private readonly Dictionary<AttributeSO, int> _attributes = new Dictionary<AttributeSO, int>();

        public void AddValue(AttributeValuePair attributeValuePair)
        {
            if (!_attributes.ContainsKey(attributeValuePair.Attribute))
            {
                _attributes.Add(attributeValuePair.Attribute, 0);
            }

            _attributes[attributeValuePair.Attribute] += attributeValuePair.Value;
        }

        public bool MeetsRequirements(IEnumerable<AttributeValuePair> requirements)
        {
            return requirements.All(i => _attributes.ContainsKey(i.Attribute) && _attributes[i.Attribute] >= i.Value);
        }

        public List<AttributeValuePair> GetAll()
        {
            return _attributes.Select(i => new AttributeValuePair(i.Key, i.Value)).ToList();
        }

        public void Clear()
        {
            _attributes.Clear();
        }

        public override string ToString()
        {
            return _attributes.Aggregate("Attributes: ", (s, pair) => s += $"  {pair.Key}-{pair.Value}");
        }
    }
}