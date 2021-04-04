﻿using Assets.Scripts.World;
using QGame;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Planning.UI
{
    public class CrewViewModel : QScript
    {
        public TMP_Text LeaderNameText;
        public TMP_Text CrewNameText;
        public Image CrewLeaderPortrait;
        private Crew _crew;

        public void Initialize(Crew crew)
        {
            _crew = crew;
            LeaderNameText.text = _crew.LeaderName;
            CrewNameText.text = _crew.CrewName;
            CrewLeaderPortrait.sprite = _crew.LeaderProtraitSprite;
        }
    }
}