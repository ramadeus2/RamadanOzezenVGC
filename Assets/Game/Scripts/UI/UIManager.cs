using System;
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

    public class UIManager: MonoSingleton<UIManager> {
        private UIRewardPanel _uiRewardPanel;
        private UICardPanel _uiCardPanel;
        private UICurrency _uiCurrency;
        private UIMainMenu _uiMainMenu;
        private UIStageBar _uiStageBar;
        private UIZoneInfoPanel _uiZoneInfoPanel;



        private CurrencyManager _cm;


        [Inject]
        private void Constructor(UIRewardPanel uiRewardPanel, UICardPanel uiCardPanel, UICurrency uiCurrency, UIMainMenu uiMainMenu, UIStageBar uiStageBar, UIZoneInfoPanel uiZoneInfoPanel)
        {
            _uiRewardPanel = uiRewardPanel;
            _uiCardPanel = uiCardPanel;
            _uiCurrency = uiCurrency;
            _uiMainMenu = uiMainMenu;
            _uiStageBar = uiStageBar;
            _uiZoneInfoPanel = uiZoneInfoPanel;
        }
        private void Start()
        {
            _cm = CurrencyManager.Instance;

        }
        public void CheckTheStage(RewardUnit rewardUnit, StageZone stageZone)
        {
            bool isBomb = rewardUnit.RewardData.RewardType == RewardType.Bomb;
            //_cardPanel.InitializePanel(rewardUnit.RewardIcon, isBomb);
            if(isBomb)
            {
                _uiCardPanel.VisualizeBombPanel();
                ShowCurrency(true);
            }
            else
            {
                _uiCardPanel.VisualizeSafePanel(rewardUnit.RewardIcon, stageZone);
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
            StageManager sm = StageManager.Instance;
            sm.AutomaticStageSystem.gameObject.SetActive(false);
            sm.ManualStageSystem.gameObject.SetActive(false);
            _uiMainMenu.gameObject.SetActive(true);
            _uiRewardPanel.ClearRewardTable();
            ShowCurrency(true);

        }
        public bool RequestRevive(/*int revivePrice*/)
        {

            if(_cm.TrySpending("7acca646-a0b8-4d40-b5d3-e2727ab11c7c", 10))
            {
                SetNextStage();
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
            if(StageManager.Instance.InitializeNextStage())
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
        public void InitializeCurrencyUI(List<CurrencySaveData> savedCurrencyDatas) => _uiCurrency.InitializeCurrencyUI(savedCurrencyDatas);
        public void UpdateCurrencyUI() => _uiCurrency.UpdateCurrencyUI();

        internal void CollectAndLeave()
        {
            CollectRewards(Consts.SAVE_INFO_NAME_REWARD, RewardType.Normal);
            CollectRewards(Consts.SAVE_INFO_NAME_CURRENCY, RewardType.Currency);
            CurrencyManager.Instance.SynchronizeSavedCurrencyDatas();

            OpenMainMenu();

        }

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
                    if(contents[i].RewardType == dataSaves[a].RewardType && contents[i].RewardDataId == dataSaves[a].DataId)
                    {
                        amount += dataSaves[a].CurrentAmount;
                        break;
                    }
                }
                AbstractSaveData rewardSaveData = null;
                switch(rewardType)
                {
                    case RewardType.Normal:
                        rewardSaveData = new RewardSaveData(contents[i].RewardDataId, amount);
                        break;
                    case RewardType.Currency:
                        CurrencyUnit currencyUnit = GameManager.Instance.GameSettings.GetCurrencyUnit(contents[i].RewardDataId);
                        rewardSaveData = new CurrencySaveData(contents[i].RewardDataId, amount, currencyUnit);
                        break;
                    case RewardType.Special:

                        break;
                    case RewardType.Bomb:
                        break;
                    default:
                        break;
                }

                SaveSystem.UpdateData(rewardSaveData, rewardKey, rewardType);
            }
        }

        public void ActivateSpecialRewardPopUp() => _uiZoneInfoPanel.ActivateSpecialRewardPopUp();
        public void InitializeSpecialRewardIcon(StageZone stageZone, int zoneTargetStageNo, Sprite icon) => _uiZoneInfoPanel.InitializeSpecialRewardIcon(stageZone, zoneTargetStageNo, icon);
        public void InitializeStageBar(int stageCount) => _uiStageBar.InitializeStageVisual(stageCount);

    }
}
