using UnityEngine;

namespace Assets.Scripts.World
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WorldLocation", order = 1)]
    public class WorldLocationSO : ScriptableObject
    {
        public string DisplayName;
        public int PurchaseValue;
        public int ExtortionValue;
    }
}
