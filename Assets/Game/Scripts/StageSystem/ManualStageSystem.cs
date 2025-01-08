using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

public class ManualStageSystem : AbstractStageSystem
{
        protected override void SetStageCount()
        {
           _stageCount = _gameSettings.StagePool.StageDatas.Count; 
        }
        protected override void SetCurrentStage()
        { 
            _currentStage = _gameSettings.StagePool.StageDatas[_currentStageNo-1];  
        }
        protected override List<RewardData> GetRewardList(StageZone stageZone)
        { 
            _currentStage = _gameSettings.StagePool.StageDatas[_currentStageNo-1]; 
            return _currentStage.RewardDatas;

        }

        protected override void RunStage(List<RewardData> rewardDatas, int stageNo, StageZone stageZone)
        {
            _currentStage.RunStage();
        }
    }
}
