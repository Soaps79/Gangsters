using Assets.Scripts.World;
using QGame;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    public class CrewAttributeViewModel : QScript
    {
        public TMP_Text ValueText;
        public Image Icon;
        
        private AttributeValuePair _valuePair;

        public void Initialize(AttributeValuePair valuePair)
        {
            _valuePair = valuePair;
            ValueText.text = _valuePair.Value.ToString();
            Icon.sprite = _valuePair.Attribute.LargeIcon;
        }

    }
}