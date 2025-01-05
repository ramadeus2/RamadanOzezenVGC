
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public class AutomaticStageSystem: AbstractStageSystem  {

        [SerializeField] private int _stageCount;
        private RewardPool _rewardPool;

        private void Start()
        {

            PrepareNextStage();
        }
  
        private void InitializeNextStage()
        { 
           
            _currentStage = ScriptableObject.CreateInstance<StageData>();
            StageZone stageZone = GetStageZone();
            List<RewardData> rewardDatas = InitializeRewardsForThisStage(stageZone);
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone);
            ReleaseRewardPool();
            _currentStageNo++;

        }
        public override void PrepareNextStage()
        {
            if(_currentStageNo >= _stageCount)
            {
                return;
            }
            GetRewardPool(InitializeNextStage);

        }
       
        private List<RewardData> InitializeRewardsForThisStage(StageZone stageZone)
        {
             
            List<RewardData> rewardDatas = _rewardPool.NormalRewardDatas;



            int totalPossibility = 0;
            foreach(var reward in rewardDatas)
            {
                totalPossibility += reward.Probability;
            }

            List<RewardData> selectedRewards = new List<RewardData>();
            bool willThereBeBomb = stageZone == StageZone.DangerZone;
            int manuallyAddedItemCount = 0;
            if(willThereBeBomb)
            {
                selectedRewards.Add(_rewardPool.BombData);
                manuallyAddedItemCount++;
            }


            List<RewardData> availableRewards = new List<RewardData>(rewardDatas);

            for(int i = 0; i < Consts.STAGE_REWARD_AMOUNT - manuallyAddedItemCount; i++) // if we added the bomb
            {
                if(availableRewards.Count == 0)
                    break;

                int randomValue = Random.Range(0, totalPossibility);
                int cumulativePossibility = 0;

                foreach(var reward in availableRewards)
                {
                    cumulativePossibility += reward.Probability;

                    if(randomValue < cumulativePossibility)
                    {
                        selectedRewards.Add(reward);

                        totalPossibility -= reward.Probability;
                        availableRewards.Remove(reward);

                        break;
                    }
                }
            }
            //for(int i = 0; i < selectedRewards.Count; i++)
            //{
            //    Debug.Log(selectedRewards[i]);

            //}

            return selectedRewards;

        }
        protected void GetRewardPool(System.Action task)
        {
            AddressablesManager.Instance.GetRewardPool((rewardPool) =>
            {
                _rewardPool = rewardPool;
                task?.Invoke();
            });
        }
        protected void ReleaseRewardPool()
        {
            _rewardPool = null;
            AddressablesManager.Instance.ReleaseRewardPool();
        }
    }
}
