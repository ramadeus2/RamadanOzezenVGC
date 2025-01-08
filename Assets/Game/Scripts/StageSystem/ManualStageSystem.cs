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
        protected override void SetCurrentStage()
        { 
            _currentStage = _gameSettings.StagePool.StageDatas[_currentStageNo];  
        }
        protected override List<RewardData> GetRewardList(StageZone stageZone)
        {
            _currentStage = _gameSettings.StagePool.StageDatas[_currentStageNo];
            return _currentStage.RewardDatas;

        }

    }
}
