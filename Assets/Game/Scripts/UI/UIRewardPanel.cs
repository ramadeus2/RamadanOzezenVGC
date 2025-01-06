using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;

namespace WheelOfFortune.UserInterface {

    public class UIRewardPanel: MonoBehaviour {
        [SerializeField] private UIRewardContent _rewardContentPrefab;
        [SerializeField] private Transform _rewardsHolder;
        private List<UIRewardContent> _rewardContents;
        private void Start()
        {
            _rewardContents = new List<UIRewardContent>();
        }
        public void InitializeReward(RewardUnit rewardUnit)
        {
            bool contains = false;
            for(int i = 0; i < _rewardContents.Count; i++)
            {
                if(_rewardContents[i].RewardDataId == rewardUnit.RewardData.RewardId)
                {
                    _rewardContents[i].ApplyRewardAmount(rewardUnit.AppliedRewardAmount);
                    contains = true;
                    break;
                }
            }
            if(!contains)
            {
                UIRewardContent rewardContent = Instantiate(_rewardContentPrefab, _rewardsHolder);
                _rewardContents.Add(rewardContent);
                rewardContent.InitializeRewardContent(rewardUnit);
            }
        }

    }
}
