using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Stage;

namespace WheelOfFortune.UserInterface {

    public class UIManager: MonoSingleton<UIManager> {
        [SerializeField] private UIRewardPanel _rewardPanel;
        [SerializeField] private UICardPanel _cardPanel;


        public void RewardEarned(RewardUnit rewardUnit)
        {
            bool isBomb = rewardUnit.RewardData.IsBomb;
            _cardPanel.InitializePanel(rewardUnit.RewardIcon, isBomb);
            if(!isBomb)
            {
                _rewardPanel.InitializeReward(rewardUnit);
            }
        }
        public void GiveUpRewards()
        {
            _cardPanel.ClosePanel(); 
        }
        public void RequestRevive()
        {
            _cardPanel.ClosePanel(); 
        }
        public void SetNextStage()
        {
            AbstractStageSystem.Instance.InitializeNextStage();
        }
    }
}
