using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Stage {

    public class StageManager: MonoSingleton<StageManager> {
        [ SerializeField] private ManualStageSystem _manualStageSystem;
        [ SerializeField] private AutomaticStageSystem _automaticStageSystem;
        private AbstractStageSystem _currentStageSystem;

        public void ActivateAutomaticStageSystem()
        {
            _manualStageSystem.gameObject.SetActive(false);

            _automaticStageSystem.gameObject.SetActive(true);

            _currentStageSystem = _automaticStageSystem;
        }
        public void ActivateManualStageSystem()
        {
            _automaticStageSystem.gameObject.SetActive(false);

            _manualStageSystem.gameObject.SetActive(true);

            _currentStageSystem = _automaticStageSystem;
        }

        public void InitializeNextStage() => _currentStageSystem.InitializeNextStage();
        
    }
}
