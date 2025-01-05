using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

public class ManualStageSystem : AbstractStageSystem
{
        private StagePool _stagePool;
        private void Start()
        {
            PrepareNextStage();
        }
        public override void PrepareNextStage()
        {
            
            GetStagePool(InitializeNextStage);

        }
        private void InitializeNextStage()
        {

            _currentStageNo++;
            if(_currentStageNo>= _stagePool.StageDatas.Count)
            {
                ReleaseStagePool();
                return;
            }
            _currentStage = _stagePool.StageDatas[_currentStageNo];
            StageZone stageZone = GetStageZone();
            List<RewardData> rewardDatas =_currentStage.RewardDatas;
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone);
            ReleaseStagePool();

        }
        protected void GetStagePool(System.Action task)
        {
            AddressablesManager.Instance.GetStagePool((stagePool) =>
            {
                _stagePool = stagePool;
                task?.Invoke();
            });
        }
        protected void ReleaseStagePool()
        {
            _stagePool = null;
            AddressablesManager.Instance.ReleaseStagePool();
        }
    }
}
