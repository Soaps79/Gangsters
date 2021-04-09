using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CrewLeader")]
    public class CrewLeaderSO : ScriptableObject
    {
        public Sprite PortraitSprite;

        [SerializeField]
        private string _firstName;
        public string FirstName => _firstName;

        [SerializeField]
        private string _lastName;
        public string LastName => _lastName;

        [SerializeField]
        private string _crewName;
        public string CrewName => _crewName;

        public List<AttributeValuePair> BaseAttributes;

        public string FullName => $"{FirstName} {LastName}";
    }
}
