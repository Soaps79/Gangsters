using Assets.Scripts.UI;
using QGame;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Execution.UI
{
    [RequireComponent(typeof(SliderBinding))]
    public class ExecutionTaskViewModel : QScript
    {
        public SliderBinding SliderBinding;
        public TMP_Text NameText;

        private ExecutionTask _executionTask;

        public void Initialize(ExecutionTask executionTask)
        {
            _executionTask = executionTask;
            SliderBinding.Initialize(() => _executionTask.CurrentCraftElapsedAsZeroToOne);
            NameText.text = _executionTask.DisplayName;
        }
    }
}