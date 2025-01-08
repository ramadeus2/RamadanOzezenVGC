using WheelOfFortune.Utilities; 
namespace WheelOfFortune.SaveManagement {

    public class NormalRewardSaveData: AbstractSaveData {
       
        public NormalRewardSaveData(string rewardId,int amount) : base(rewardId,amount, Utilities.RewardType.Normal)
        {
        }

        public NormalRewardSaveData(string dataId, int amount, RewardType rewardType) : base(dataId, amount, rewardType)
        {
        }
    }
}
