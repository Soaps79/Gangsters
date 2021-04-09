using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WorldProperty")]
    public class WorldPropertySO : ScriptableObject
    {
        public string DisplayName;
        public int PurchasePrice;
        public int ExtortionValue;
        public List<AttributeValuePair> ExtortionRequirements;
    }
}
