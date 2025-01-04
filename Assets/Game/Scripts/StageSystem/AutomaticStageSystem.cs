
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public class AutomaticStageSystem: AbstractStageSystem  {

        [SerializeField] private int _stageCount;
        [SerializeField] private StageData _currentStage;
        private int _currentStageNo = -1;

        private void Start()
        {
            InitializeNextStage();
        }
  
        public void InitializeNextStage()
        { 
            _currentStageNo++;
            _currentStage = ScriptableObject.CreateInstance<StageData>();
            StageZone stageZone = GetStageZone();
            List<RewardData> rewardDatas = InitializeRewardsForThisStage(stageZone);
            _currentStage.InitializeStage(rewardDatas, _currentStageNo, stageZone);

        }
        private StageZone GetStageZone()
        {
            StageZone stageZone = StageZone.DangerZone;
            if(_currentStageNo != 0 && _currentStageNo % Consts.STAGE_SUPERZONE_MULTIPLIER == 0)
            {
                stageZone = StageZone.SuperZone;
            } else if(_currentStageNo != 0 && _currentStageNo % Consts.STAGE_SAFEZONE_MULTIPLIER == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
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
    }
}
