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
       private StagePool _stagePool;
     
        protected override void OnEnable()
        {
            base.OnEnable();
            UIManager.Instance.InitializeStageBar(_stagePool.StageDatas.Count);
        }
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
            StageZone stageZone =Helpers. GetStageZone(_currentStageNo);
            List<RewardData> rewardDatas =_currentStage.RewardDatas;
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone);
      

        }

    }
}
