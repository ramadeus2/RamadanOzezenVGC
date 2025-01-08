

namespace WheelOfFortune.SaveManagement {

    public class RewardSaveData: AbstractSaveData {
       
        public RewardSaveData(string rewardId,int amount) : base(rewardId,amount, Utilities.RewardType.Normal)
        {
        }


    }
}
