using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WheelOfFortune.Stage;
using Zenject;

namespace WheelOfFortune.UserInterface {

    public class UIMainMenu: MonoBehaviour {
        #region FIELDS
        private StageManager _stageManager;
        private UIManager _uiManager;
        private UIStatistics _statisticsPanel;
        private Button _automaticStageButton;
        private Button _manualStageButton;
        private Button _statisticsButton;
        private Button _clearDataButton;
        #endregion
        #region INITIALIZATION

        [Inject]
        private void Constructor(AutomaticStageButton automaticStageButton, ManualStageButton manualStageButton, StatisticsButton statisticsButton, UIStatistics uIStatistics, UIManager uiManager, ClearDataButton clearDataButton, StageManager stageManager)
        {
            _automaticStageButton = automaticStageButton.Button;
            _manualStageButton = manualStageButton.Button;
            _statisticsButton = statisticsButton.Button;
            _statisticsPanel = uIStatistics;
            _clearDataButton = clearDataButton.Button;
            _uiManager = uiManager;
            _stageManager = stageManager;
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
        #endregion
        #region BEHAVIOURS

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
            _stageManager.ActivateAutomaticStageSystem();
            _uiManager.ShowCurrency(false);
            gameObject.SetActive(false);

        }
        private void ActivateManualStage()
        {
            _stageManager.ActivateManualStageSystem();
            _uiManager.ShowCurrency(false);
            gameObject.SetActive(false);
        }
        #endregion

    }
}
