﻿using QGame;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SliderBinding))]
    public class WorldTaskViewModel : QScript
    {
        public SliderBinding SliderBinding;
        public TMP_Text NameText;

        public WorldTask _worldTask;

        public void Initialize(WorldTask worldTask)
        {
            _worldTask = worldTask;
            SliderBinding.Initialize(() => _worldTask.CurrentCraftElapsedAsZeroToOne);
            NameText.text = _worldTask.DisplayName;
        }
    }
}