using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class AttributeContainer
    {
        private readonly Dictionary<string, int> _attributes = new Dictionary<string, int>();

        public void AddValue(AttributeValuePair attributeValuePair)
        {
            if (!_attributes.ContainsKey(attributeValuePair.Name))
            {
                _attributes.Add(attributeValuePair.Name, 0);
            }

            _attributes[attributeValuePair.Name] += attributeValuePair.Value;
        }

        public int GetValue(string name)
        {
            return _attributes.ContainsKey(name) ? _attributes[name] : 0;
        }

        public bool MeetsRequirements(IEnumerable<AttributeValuePair> requirements)
        {
            return requirements.All(i => _attributes.ContainsKey(i.Name) && _attributes[i.Name] >= i.Value);
        }

        public List<AttributeValuePair> GetAll()
        {
            return _attributes.Select(i => new AttributeValuePair(i.Key, i.Value)).ToList();
        }

        public void Clear()
        {
            _attributes.Clear();
        }
    }
}