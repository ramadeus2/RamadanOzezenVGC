
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
        protected override void OnEnable()
        {
            base.OnEnable();
            UIManager.Instance.InitializeStageBar(_stageCount); 
        }
        public override void InitializeNextStage()
        {
            if(!_rewardPool)
            {
                _rewardPool = GameManager.Instance.GameSettings.RewardPool;
            }  
            _currentStageNo++;
            _currentStage = ScriptableObject.CreateInstance<StageData>();
            StageZone stageZone = Helpers.GetStageZone(_currentStageNo); 
            List<RewardData> rewardDatas = InitializeRewardsForThisStage(stageZone); 
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone); 

        }
       
        private List<RewardData> InitializeRewardsForThisStage(StageZone stageZone)
        {

            return _rewardPool.GetRandomRewards(_stageCount, stageZone );

        }
       
    }
}
