using UnityEngine;
using Zenject;

namespace WheelOfFortune.Stage {

    public class StageManager: MonoBehaviour {

       private ManualStageSystem _manualStageSystem;
       public ManualStageSystem ManualStageSystem => _manualStageSystem; 


       private AutomaticStageSystem _automaticStageSystem;
        public AutomaticStageSystem AutomaticStageSystem =>  _automaticStageSystem;


        private AbstractStageSystem _currentStageSystem;

        [Inject]
        private void Constructor(ManualStageSystem manualStageSystem, AutomaticStageSystem automaticStageSystem  )
        {
            _manualStageSystem = manualStageSystem;
            _automaticStageSystem = automaticStageSystem; 
        }

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

            _currentStageSystem = _manualStageSystem;
        }

        public bool InitializeNextStage() => _currentStageSystem.InitializeNextStage();
        
    }
}
