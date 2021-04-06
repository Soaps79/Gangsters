using QGame;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Execution.UI
{
    public class ResultsViewModel : QScript
    {
        public TMP_Text RewardsText;
        public Button FinishButton;


        public void Initialize(ExecutionPhase executionPhase)
        {
            FinishButton.onClick.AddListener(executionPhase.CompleteScene);
        }
    }
}
