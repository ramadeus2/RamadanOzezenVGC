using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Stage;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class UIMainMenu: MonoBehaviour {
        [SerializeField] private GameObject _statisticsPanel;
          private Button _automaticStageButton;
          private Button _manualStageButton;
          private Button _statisticsButton;
        [Inject]
        private void Constructor(AutomaticStageButton automaticStageButton,ManualStageButton manualStageButton, StatisticsButton statisticsButton)
        { 
            _automaticStageButton = automaticStageButton.Button;
            _manualStageButton = manualStageButton.Button;
            _statisticsButton = statisticsButton.Button;
        }
        private void OnEnable()
        { 
            _automaticStageButton.onClick.AddListener(ActivateAutomaticStage); 
            _manualStageButton.onClick.AddListener(ActivateManualStage); 
            _statisticsButton.onClick.AddListener(OpenStatistics); 

        }

        private void OnDisable()
        {
            
            _automaticStageButton.onClick.RemoveListener(ActivateAutomaticStage);
            _manualStageButton.onClick.RemoveListener(ActivateManualStage);
            _statisticsButton.onClick.RemoveListener(OpenStatistics); 
        }
        private void OpenStatistics()
        {
            _statisticsPanel.SetActive(true);
        }

        private void ActivateAutomaticStage()
        {
            StageManager.Instance.ActivateAutomaticStageSystem();
            UIManager.Instance.ShowCurrency(false);
            gameObject.SetActive(false);
            
        }
        private void ActivateManualStage()
        {
            StageManager.Instance.ActivateManualStageSystem();
            UIManager.Instance.ShowCurrency(false);
            gameObject.SetActive(false);
        }

    }
}
