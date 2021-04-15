using UnityEngine;

namespace Assets.Scripts.World
{
    [System.Serializable]
    public class AttributeValuePair
    {
        // allows name to be set from SO's in the editor
        [SerializeField]
        public AttributeSO Attribute;

        private string name;

        public string Name => Attribute != null ? Attribute.Name : name;
        public int Value;

        public AttributeValuePair(AttributeSO attribute, int value)
        {
            Attribute = attribute;
            Value = value;
        }
    }
}