using System;
using System.Collections.Generic;
using UnityEngine; 
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Reward;
using WheelOfFortune.SaveManagement;
using WheelOfFortune.Stage;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UserInterface {

    public class UIManager: MonoSingleton<UIManager> {
        [SerializeField] private UIRewardPanel _rewardPanel;
        [SerializeField] private UICardPanel _cardPanel;
        [SerializeField] private UICurrency _uiCurrency;
        [SerializeField] private UIMainMenu _uiMainMenu;
        [SerializeField] private UIStageBar _uiStageBar;



        private CurrencyManager _cm; 

        private void Start()
        {
            _cm = CurrencyManager.Instance;

        }
        public void CheckTheStage(RewardUnit rewardUnit,StageZone stageZone)
        {
            bool isBomb = rewardUnit.RewardData.RewardType == RewardType.Bomb;
            //_cardPanel.InitializePanel(rewardUnit.RewardIcon, isBomb);
            if(isBomb)
            {
                _cardPanel.VisualizeBombPanel();
                ShowCurrency(true);
            } else
            {
                _cardPanel.VisualizeSafePanel(rewardUnit.RewardIcon, stageZone);
                _rewardPanel.InitializeReward(rewardUnit);

            }
        }
        public void GiveUpRewards()
        {
                _cardPanel.ClosePanel();
            OpenMainMenu();
        }
        private void OpenMainMenu()
        {
            StageManager sm = StageManager.Instance;
            sm.AutomaticStageSystem.gameObject.SetActive(false);
            sm.ManualStageSystem.gameObject.SetActive(false);
            _uiMainMenu.gameObject.SetActive(true);
            _rewardPanel.ClearRewardTable();
            ShowCurrency(true);

        }
        public void RequestRevive(/*int revivePrice*/)
        {

            if(_cm.TrySpending("99206c1c-e8ee-46a7-a462-10b478d0758b", 10))
            {
                _cardPanel.ClosePanel();
                SetNextStage();
            }
        }
        public void SetNextStage()
        {
            ShowCurrency(false);
            StageManager.Instance.InitializeNextStage();
          _uiStageBar.SetNextStage();
        }
        public void ShowCurrency(bool activation)
        {
            _uiCurrency.gameObject.SetActive(activation);

        }
        public void InitializeCurrencyUI(List<CurrencySaveData> savedCurrencyDatas) => _uiCurrency.InitializeCurrencyUI(savedCurrencyDatas);
        public void UpdateCurrencyUI() => _uiCurrency.UpdateCurrencyUI();

        internal void CollectAndLeave()
        {
            List<UIRewardContent> contents = _rewardPanel.RewardContents;
            List<DataSaveInfo> dataSaves = SaveSystem.LoadDatas(Consts.SAVE_INFO_NAME_REWARD) ;
            for(int i = 0; i < contents.Count; i++)
            {
                int amount = contents[i].TotalEarnedRewardAmount;
                for(int a = 0; a < dataSaves.Count; a++)
                {
                    if(contents[i].RewardDataId == dataSaves[a].DataId)
                    {
                        amount += dataSaves[a].CurrentAmount;
                        break;
                    }
                } 
                RewardSaveData rewardSaveData = new RewardSaveData(contents[i].RewardDataId, amount);
                SaveSystem.UpdateData(rewardSaveData, Consts.SAVE_INFO_NAME_REWARD);
            }
            OpenMainMenu();

        }

        public void InitializeStageBar(int stageCount)=> _uiStageBar.InitializeStageVisual(stageCount);

    }
}
