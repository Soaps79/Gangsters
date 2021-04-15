using System.Linq;
using Assets.Scripts.World;
using QGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    public class CrewViewModel : QScript
    {
        public TMP_Text LeaderNameText;
        public TMP_Text CrewNameText;
        public Image CrewLeaderPortrait;

        public CrewAttributeViewModel AttributePrefab;
        public Transform AttributeContainer;

        private Crew _crew;

        public void Initialize(Crew crew)
        {
            _crew = crew;
            LeaderNameText.text = _crew.LeaderName;
            CrewNameText.text = _crew.CrewName;
            CrewLeaderPortrait.sprite = _crew.LeaderPortraitSprite;

            foreach (var valuePair in _crew.Attributes.GetAll())
            {
                var viewModel = Instantiate(AttributePrefab, AttributeContainer, false);
                viewModel.Initialize(valuePair);
                viewModel.gameObject.SetActive(true);
            }

            Debug.Log(crew.ToString());
        }
    }
}