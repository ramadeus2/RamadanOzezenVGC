
using UnityEngine;

namespace WheelOfFortune.Reward {

    public class RewardUnit {
        private RewardData _rewardData;
        public RewardData RewardData => _rewardData;
        private Sprite _rewardIcon;
        public Sprite RewardIcon => _rewardIcon;
        private int _appliedRewardAmount;
        public int AppliedRewardAmount=>_appliedRewardAmount;

        public RewardUnit(RewardData rewardData,Sprite rewardIcon, int appliedRewardAmount)
        {
            _rewardData = rewardData;
            _appliedRewardAmount = appliedRewardAmount;
            _rewardIcon = rewardIcon;
        }
    }
}
