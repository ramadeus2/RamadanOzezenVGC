using System;
using UnityEngine;
namespace WheelOfFortune {

    [CreateAssetMenu(fileName = "NewReward", menuName = "WheelOfFortune/WheelSystem/RewardData")]
    public class RewardData: ScriptableObject {

        private string _rewardId;
        public string RewardId => _rewardId;

        [SerializeField] private string _rewardName;
        public string RewardName => _rewardName;

        [SerializeField] private RewardRank _rewardRank;
        public RewardRank RewardRank => _rewardRank;

        [SerializeField] private int _minAmount =5;
        public int MinAmount => _minAmount;

        [SerializeField] private int _maxAmount = 15;
        public int MaxAmount => _maxAmount;

        [SerializeField]
        [Range(0, 100)]
        private int _probability = 100;
        public int Probability =>_probability;


        private string _spriteName;
        public string SpriteName => _spriteName;


        [HideInInspector] public int selectedSpriteIndex;
        public void InitializeId()
        {
            if(string.IsNullOrEmpty(_rewardId))
            {
                _rewardId = Guid.NewGuid().ToString();
            }
        }
        public void InitializeSpriteName(string name)
        {
            _spriteName = name;
        }

        public void SetRewardName()
        {
            if(string.IsNullOrEmpty(RewardName))
            {
                _rewardName = name;
            }
        }
    }
}
