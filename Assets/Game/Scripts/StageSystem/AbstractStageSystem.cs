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
     
       
       
        
        public abstract void InitializeNextStage();
    }
}
