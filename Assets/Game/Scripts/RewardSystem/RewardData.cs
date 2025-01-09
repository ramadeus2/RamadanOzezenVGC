using UnityEngine;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Reward {

    [CreateAssetMenu(fileName = "NewReward", menuName = "WheelOfFortune/Reward/RewardData")]
    public class RewardData: ScriptableObject {

        [SerializeField] private string _rewardId;
        public string RewardId => _rewardId;

        [SerializeField] private string _rewardName;
        public string RewardName => _rewardName; 

        [SerializeField] private int _minAmount =5; // for randomizing the amount of the reward
        public int MinAmount => _minAmount;

        [SerializeField] private int _maxAmount = 15;// for randomizing the amount of the reward
        public int MaxAmount => _maxAmount;


        [SerializeField]
        [Range(0, 100)]
        private int _probability = 100;
        public int Probability =>_probability; // for automatic stage system, reward datas are being called randomly. this value sets the probability of its getting picked.

    
        [SerializeField] private RewardType _rewardType;
        public RewardType RewardType => _rewardType;

        [SerializeField] private string _spriteName;
        public string SpriteName => _spriteName; 


        [HideInInspector] public int selectedSpriteIndex;  // for editor dropdown menu
        public void InitializeId()
        {
            if(string.IsNullOrEmpty(_rewardId))
            {
                _rewardId = System.Guid.NewGuid().ToString();
            }
        }
        
        public void InitializeSpriteName(string name)
        {
            _spriteName = name;
        }
        
        public void InitializeRewardType(RewardType rewardType)
        {
            _rewardType = rewardType;
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
