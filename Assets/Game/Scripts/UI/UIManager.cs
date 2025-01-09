using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.SaveManagement;
using WheelOfFortune.Stage;
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune.UserInterface {

    #region FIELDS
    public class UIManager: MonoBehaviour {
        private GameSettings _gameSettings;
        private StageManager _stageManager;
        private CurrencyManager _cm;

        private UIRewardPanel _uiRewardPanel;
        private UICardPanel _uiCardPanel;
        private UICurrency _uiCurrency;
        private UIMainMenu _uiMainMenu;
        private UIStageBar _uiStageBar;
        private UIZoneInfoPanel _uiZoneInfoPanel;

        private int _reviveTime;
        private int _currentRevivePrice;
        #endregion
        #region INITIALIZATION

        [Inject]
        private void Constructor(UIRewardPanel uiRewardPanel, UICardPanel uiCardPanel, UICurrency uiCurrency, UIMainMenu uiMainMenu, UIStageBar uiStageBar, UIZoneInfoPanel uiZoneInfoPanel, GameSettings gameSettings, StageManager stageManager)
        {
            _uiRewardPanel = uiRewardPanel;
            _uiCardPanel = uiCardPanel;
            _uiCurrency = uiCurrency;
            _uiMainMenu = uiMainMenu;
            _uiStageBar = uiStageBar;
            _uiZoneInfoPanel = uiZoneInfoPanel;
            _gameSettings = gameSettings;
            _stageManager = stageManager;
        }
        private void Start()
        {
            _cm = CurrencyManager.Instance; 

        }
        #endregion
        #region BEHAVIOUR

        public void CheckTheRewardOfThisStage(RewardUnit rewardUnit)
        {
            bool isBomb = rewardUnit.RewardData.RewardType == RewardType.Bomb;
            if(isBomb)
            {
                _uiCardPanel.VisualizeBombPanel();
                _currentRevivePrice = _gameSettings.GetRevivePrice(_reviveTime);
                _uiCardPanel.InitializeReviveAmountText(_currentRevivePrice);
                ShowCurrency(true);
            }
            else
            {
                _uiCardPanel.VisualizeSafePanel(rewardUnit.RewardIcon, rewardUnit.AppliedRewardAmount);
                _uiRewardPanel.InitializeReward(rewardUnit);

            }
        }
        public void GiveUpRewards()
        {
            _uiCardPanel.ClosePanel();
            OpenMainMenu();
        }
        private void OpenMainMenu()
        {
            _stageManager.AutomaticStageSystem.gameObject.SetActive(false);
            _stageManager.ManualStageSystem.gameObject.SetActive(false);
            _uiMainMenu.gameObject.SetActive(true);
            _uiRewardPanel.ClearRewardTable();
            PickerWheel.Instance.ActivateSpin();
            ShowCurrency(true);

        }
        public bool RequestRevive()
        {
            CurrencyUnit currencyUnit = _gameSettings.ReviveCurrency;
            if(_cm.TrySpending(currencyUnit.CurrencyRewardData.RewardId, _currentRevivePrice))
            {
                _reviveTime++;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void SetNextStage()
        {
            ShowCurrency(false);


            // if all stages are done, collect and go to main menu
            if(_stageManager.InitializeNextStage()) // this method also returns if there is a next stage
            {
                _uiStageBar.SetNextStage();
            }
            else
            {
                CollectAndLeave();
            }


        }
        public void ShowCurrency(bool activation)
        {
            _uiCurrency.gameObject.SetActive(activation);

        }

        internal void CollectAndLeave()
        {
            CollectRewards(Consts.SAVE_INFO_NAME_NORMAL_REWARD, RewardType.Normal);
            CollectRewards(Consts.SAVE_INFO_NAME_CURRENCY, RewardType.Currency);
            CollectRewards(Consts.SAVE_INFO_NAME_SPECIAL, RewardType.Special);
            CurrencyManager.Instance.SynchronizeSavedCurrencyDatas();

            OpenMainMenu();

        }
        /// <summary>
        /// gets the datas and seperates and distributes to the spesified save named datas
        /// </summary>
        private void CollectRewards(string rewardKey, RewardType rewardType)
        {

            List<UIRewardContent> contents = _uiRewardPanel.RewardContents;
            List<DataSaveInfo> dataSaves = SaveSystem.LoadDatas(rewardKey, rewardType);

            for(int i = 0; i < contents.Count; i++)
            {



                if(contents[i].RewardType != rewardType)
                {
                    continue;
                }
                int amount = contents[i].TotalEarnedRewardAmount;
                for(int a = 0; a < dataSaves.Count; a++)
                {
                    if(contents[i].RewardType == dataSaves[a].RewardType && 
                        contents[i].RewardDataId == dataSaves[a].DataId)
                    {
                        amount += dataSaves[a].CurrentAmount;
                        break;
                    }
                }



                AbstractSaveData rewardSaveData = null;
                switch(rewardType)
                {
                    case RewardType.Normal:
                        rewardSaveData = new NormalRewardSaveData(contents[i].RewardDataId, amount);
                        break;
                    case RewardType.Currency:
                        CurrencyUnit currencyUnit = _gameSettings.GetCurrencyUnit(contents[i].RewardDataId);
                        rewardSaveData = new CurrencySaveData(contents[i].RewardDataId, amount, currencyUnit);
                        break;
                    case RewardType.Special:
                        rewardSaveData = new SpecialRewardSaveData(contents[i].RewardDataId, amount);
                        break;
                    case RewardType.Bomb:
                        break;
                    default:
                        break;
                }



                SaveSystem.UpdateData(rewardSaveData, rewardKey, rewardType);
            }
        }
        public void ResetReviveTime()
        {
            _reviveTime = 0;
        }

        public void ActivateSpecialRewardPopUp() => _uiZoneInfoPanel.ActivateSpecialRewardPopUp();
        public void InitializeZoneRewardIcon(StageZone stageZone, int zoneTargetStageNo, Sprite icon) => _uiZoneInfoPanel.InitializeZoneRewardIcon(stageZone, zoneTargetStageNo, icon);
        public void InitializeStageBar(int stageCount) => _uiStageBar.InitializeStageBarVisual(stageCount);
        public void InitializeCurrencyUI(List<CurrencySaveData> savedCurrencyDatas) => _uiCurrency.InitializeCurrencyUI(savedCurrencyDatas);
        public void UpdateCurrencyUI() => _uiCurrency.UpdateCurrencyUI();

        #endregion

    }
}
