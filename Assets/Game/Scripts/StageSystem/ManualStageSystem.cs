using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

public class ManualStageSystem : AbstractStageSystem
{
       private StagePool _stagePool;
       

        public override void InitializeNextStage()
        {
            if(!_stagePool)
            {
                _stagePool = GameManager.Instance.GameSettings.StagePool;
            }
            _currentStageNo++;
            if(_currentStageNo>= _stagePool.StageDatas.Count)
            { 
                return;
            }
            _currentStage = _stagePool.StageDatas[_currentStageNo];
            StageZone stageZone = GetStageZone();
            List<RewardData> rewardDatas =_currentStage.RewardDatas;
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone);
            //ReleaseStagePool();

        }
        
    }
}
