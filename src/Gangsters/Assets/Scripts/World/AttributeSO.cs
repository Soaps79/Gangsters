using UnityEngine;

namespace Assets.Scripts.World
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Attribute")]
    public class AttributeSO : ScriptableObject
    {
        public string Name;
    }
}