using System; 
using WheelOfFortune.Utilities;

namespace WheelOfFortune.SaveManagement {
    [Serializable] 
    public abstract class AbstractSaveData 
{
        public AbstractSaveData(string dataId,RewardType rewardType )
        {
            _dataId = dataId;
            _rewardType = rewardType;
        }
        public AbstractSaveData(string dataId,  int amount, RewardType rewardType)
        { 
            _dataId = dataId;
            _currentAmount = amount;
            _rewardType = rewardType;
        }
        public int InsertAmount(int amount)
        {
            _currentAmount += amount;
            return CurrentAmount;
        }
        public int ExtractAmount(int amount)
        {
            _currentAmount -= amount;
            return CurrentAmount;
        }
     
        protected int _currentAmount;
        public int CurrentAmount => _currentAmount;
        protected string _dataId;
        public string DataId => _dataId;
        protected RewardType _rewardType;
        public RewardType RewardType => _rewardType;

    }
}
