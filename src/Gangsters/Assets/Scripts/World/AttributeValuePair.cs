using UnityEngine;

namespace Assets.Scripts.World
{
    [System.Serializable]
    public class AttributeValuePair
    {
        // allows name to be set from SO's in the editor
        [SerializeField]
        private AttributeSO _attribute;

        private string name;

        public string Name => _attribute != null ? _attribute.Name : name;
        public int Value;

        public AttributeValuePair(string name, int value)
        {
            this.name = name;
            Value = value;
        }
    }
}