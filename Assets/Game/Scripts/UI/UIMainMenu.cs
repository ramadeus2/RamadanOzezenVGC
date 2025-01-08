using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WheelOfFortune.Stage;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class UIMainMenu: MonoBehaviour {
          private UIStatistics _statisticsPanel;
          private Button _automaticStageButton;
          private Button _manualStageButton;
          private Button _statisticsButton;
          private Button _clearDataButton;
        UIManager _uiManager;
        [Inject]
        private void Constructor(AutomaticStageButton automaticStageButton,ManualStageButton manualStageButton, StatisticsButton statisticsButton, UIStatistics uIStatistics, UIManager uiManager,ClearDataButton clearDataButton)
        { 
            _automaticStageButton = automaticStageButton.Button;
            _manualStageButton = manualStageButton.Button;
            _statisticsButton = statisticsButton.Button;
            _statisticsPanel = uIStatistics;
            _clearDataButton = clearDataButton.Button;
            _uiManager = uiManager;
        }
        private void OnEnable()
        { 
            _automaticStageButton.onClick.AddListener(ActivateAutomaticStage); 
            _manualStageButton.onClick.AddListener(ActivateManualStage); 
            _statisticsButton.onClick.AddListener(OpenStatistics);
            _clearDataButton.onClick.AddListener(ClearData);

        }

        private void OnDisable()
        {
            
            _automaticStageButton.onClick.RemoveListener(ActivateAutomaticStage);
            _manualStageButton.onClick.RemoveListener(ActivateManualStage);
            _statisticsButton.onClick.RemoveListener(OpenStatistics);
            _clearDataButton.onClick.RemoveListener(ClearData);
        }

        private void ClearData()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }

        private void OpenStatistics()
        {
            _statisticsPanel.gameObject.SetActive(true);
        }

        private void ActivateAutomaticStage()
        {
            StageManager.Instance.ActivateAutomaticStageSystem();
            _uiManager.ShowCurrency(false);
            gameObject.SetActive(false);
            
        }
        private void ActivateManualStage()
        {
            StageManager.Instance.ActivateManualStageSystem();
           _uiManager. ShowCurrency(false);
            gameObject.SetActive(false);
        }

    }
}
