using WheelOfFortune.Utilities;

namespace WheelOfFortune.SaveManagement {

    public class SpecialRewardSaveData: AbstractSaveData {
        public SpecialRewardSaveData(string rewardId, int amount) : base(rewardId, amount, Utilities.RewardType.Special)
        {
        }

        public SpecialRewardSaveData(string dataId, int amount, RewardType rewardType) : base(dataId, amount, rewardType)
        {
        }
    }
}
