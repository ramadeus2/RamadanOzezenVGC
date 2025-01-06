using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Reward;

namespace WheelOfFortune.UserInterface {

public class UIRewardContent : MonoBehaviour
{
        [SerializeField] private Image _rewardImg;
        [SerializeField] private TMP_Text _rewardAmountTxt;
        private string _rewardDataId;
        public string RewardDataId => _rewardDataId;
        private int _totalEarnedReardAmount;
        public int TotalEarnedReardAmount => _totalEarnedReardAmount;
        public void InitializeRewardContent(RewardUnit rewardUnit)
        {
            _rewardDataId = rewardUnit.RewardData.RewardId;
            _rewardImg.sprite = rewardUnit.RewardIcon; 
            ApplyRewardAmount(rewardUnit.AppliedRewardAmount);
        }
        public void ApplyRewardAmount(int rewardAmount)
        {
            _totalEarnedReardAmount += rewardAmount;
            _rewardAmountTxt.text = _totalEarnedReardAmount.ToString();
        }
        
    }
}
