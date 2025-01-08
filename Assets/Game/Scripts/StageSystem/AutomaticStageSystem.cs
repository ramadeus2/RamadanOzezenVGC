
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public class AutomaticStageSystem: AbstractStageSystem {

        protected override void SetStageCount()
        {
            _stageCount = _gameSettings.AutomaticStageSystemStageCount;
        }
        protected override void SetCurrentStage()
        {
            _currentStage = ScriptableObject.CreateInstance<StageData>(); 
        }
        protected override List<RewardData> GetRewardList(StageZone stageZone)
        {
            return _gameSettings.RewardPool.GetRandomRewards(_stageCount, stageZone);
        }
            protected override void RunStage(List<RewardData> rewardDatas, int stageNo, StageZone stageZone)
        {
            _currentStage.RunStage(rewardDatas, stageNo, stageZone);
        }
         

    }
}
