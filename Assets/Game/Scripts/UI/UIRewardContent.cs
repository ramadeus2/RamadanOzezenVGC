using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UserInterface {

public class UIRewardContent : MonoBehaviour
{
        [SerializeField] private Image _rewardImg;
        [SerializeField] private TMP_Text _rewardAmountTxt;

        private string _rewardDataId;
        public string RewardDataId => _rewardDataId;

        private RewardType _rewardType;
        public RewardType RewardType => _rewardType;

        private int _totalEarnedReardAmount;
        public int TotalEarnedRewardAmount => _totalEarnedReardAmount;
        public void InitializeRewardContent(RewardUnit rewardUnit)
        {
            _rewardDataId = rewardUnit.RewardData.RewardId;
            _rewardImg.sprite = rewardUnit.RewardIcon;
            _rewardType = rewardUnit.RewardData.RewardType;
            ApplyRewardAmount(rewardUnit.AppliedRewardAmount);
        }

        public void ApplyRewardAmount(int rewardAmount)
        {
            _totalEarnedReardAmount += rewardAmount;
            _rewardAmountTxt.text = _totalEarnedReardAmount.ToString();
        }
        
    }
}
