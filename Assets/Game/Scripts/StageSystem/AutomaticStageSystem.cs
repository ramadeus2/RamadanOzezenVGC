
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public class AutomaticStageSystem: AbstractStageSystem  {

        [SerializeField] private int _stageCount;
          private RewardPool _rewardPool;
         
        public override void InitializeNextStage()
        {
            if(!_rewardPool)
            {
                _rewardPool = GameManager.Instance.GameSettings.RewardPool;
            }  
            _currentStageNo++;
            _currentStage = ScriptableObject.CreateInstance<StageData>();
            StageZone stageZone = GetStageZone(); 
            List<RewardData> rewardDatas = InitializeRewardsForThisStage(stageZone); 
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone); 

        }
       
        private List<RewardData> InitializeRewardsForThisStage(StageZone stageZone)
        {
             
            List<RewardData> rewardDatas =new List<RewardData>();
            for(int i = 0; i < _rewardPool.NormalRewardDatas.Count; i++)
            {
                rewardDatas.Add(_rewardPool.NormalRewardDatas[i]);
            }

            for(int i = 0; i < _rewardPool.CurrencyDatas.Count; i++)
            {
                rewardDatas.Add(_rewardPool.CurrencyDatas[i]);
            }

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

            for(int i = 0; i < GameManager.Instance.GameSettings.StageRewardAmount - manuallyAddedItemCount; i++) // if we added the bomb
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
            return selectedRewards;

        }
       
    }
}
