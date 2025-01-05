using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem: MonoSingleton<AbstractStageSystem> {
        protected int _currentStageNo = -1;
        protected StageData _currentStage;

     
       
        protected StageZone GetStageZone()
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
       
        public abstract void InitializeNextStage();
    }
}
