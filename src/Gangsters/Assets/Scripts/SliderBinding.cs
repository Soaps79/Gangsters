using System;
using QGame;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Slider))]
    public class SliderBinding : QScript
    {
        private Func<float> _func;
        public Slider _slider;

        public void Initialize(Func<float> func)
        {
            _func = func;
            //_slider = GetComponent<Slider>();

            if (_func == null || _slider == null)
                throw new UnityException("SliderBinding initialized with bad things");

            _slider.value = func();
            OnEveryUpdate = UpdateSlider;
        }

        private void UpdateSlider()
        {
            _slider.value = _func();
        }
    }
}