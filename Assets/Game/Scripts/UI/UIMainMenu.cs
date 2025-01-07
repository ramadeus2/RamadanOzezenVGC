using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Stage;

namespace WheelOfFortune.UserInterface {

    public class UIMainMenu: MonoBehaviour {
        [SerializeField] private Button _automaticStageButton;
        [SerializeField] private Button _manualStageButton;

        private void Start()
        {
            _automaticStageButton.onClick.RemoveAllListeners();
            _automaticStageButton.onClick.AddListener(ActivateAutomaticStage);
            _manualStageButton.onClick.RemoveAllListeners();
            _manualStageButton.onClick.AddListener(ActivateManualStage);
        }
        private void ActivateAutomaticStage()
        {
            StageManager.Instance.ActivateAutomaticStageSystem();
            gameObject.SetActive(false);
        }
        private void ActivateManualStage()
        {
            StageManager.Instance.ActivateManualStageSystem();
            gameObject.SetActive(false);
        }

    }
}
