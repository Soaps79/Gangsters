using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WorldState")]
    public class WorldStateSO : ScriptableObject
    {
        public List<WorldPropertyState> Properties;
    }
}
