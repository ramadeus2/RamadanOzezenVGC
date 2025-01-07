using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem:MonoBehaviour {
        protected int _currentStageNo = 0;
        protected StageData _currentStage;  
        protected virtual void OnEnable()
        {
            InitializeNextStage();
        }

        protected virtual void OnDisable()
        {
            _currentStage = null;
            _currentStageNo = 0;
        }
     
       
        protected StageZone GetStageZone()
        { 
            StageZone stageZone = StageZone.DangerZone;
            if(_currentStageNo > 0 && _currentStageNo % GameManager.Instance.GameSettings.StageSuperZoneMultiplier == 0)
            {
                stageZone = StageZone.SuperZone;
            } else if(_currentStageNo > 0 && _currentStageNo % GameManager.Instance.GameSettings.StageSafeZoneMultiplier == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
        
        public abstract void InitializeNextStage();
    }
}
