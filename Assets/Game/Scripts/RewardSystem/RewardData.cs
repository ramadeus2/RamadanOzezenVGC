 
using UnityEngine;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Reward {

    [CreateAssetMenu(fileName = "NewReward", menuName = "WheelOfFortune/Reward/RewardData")]
    public class RewardData: ScriptableObject {

        private string _rewardId;
        public string RewardId => _rewardId;

        [SerializeField] private string _rewardName;
        public string RewardName => _rewardName;

        //[SerializeField] private RewardRank _rewardRank;
        //public RewardRank RewardRank => _rewardRank;

        [SerializeField] private int _minAmount =5;
        public int MinAmount => _minAmount;

        [SerializeField] private int _maxAmount = 15;
        public int MaxAmount => _maxAmount;


        [SerializeField]
        [Range(0, 100)]
        private int _probability = 100;
        public int Probability =>_probability;

        [SerializeField]
        private bool  _isBomb;
        public bool IsBomb=> _isBomb;

        private string _spriteName;
        public string SpriteName => _spriteName;

        [HideInInspector] public int selectedSpriteIndex;
        public void InitializeId()
        {
            if(string.IsNullOrEmpty(_rewardId))
            {
                _rewardId =System.Guid.NewGuid().ToString();
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
        public int GetRandomAmount()
        {
            return Random.Range(MinAmount, MaxAmount);
        }
    }
}
