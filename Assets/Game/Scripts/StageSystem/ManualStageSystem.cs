using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

public class ManualStageSystem : AbstractStageSystem
{
        [SerializeField] private StagePool _stagePool;
        private void Start()
        {
            InitializeNextStage();

        }

        public override void InitializeNextStage()
        {

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
