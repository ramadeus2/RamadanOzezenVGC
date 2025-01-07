using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Reward;
using WheelOfFortune.Stage;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UserInterface {

    public class UIManager: MonoSingleton<UIManager> {
        [SerializeField] private UIRewardPanel _rewardPanel;
        [SerializeField] private UICardPanel _cardPanel;
        [SerializeField] private UICurrency _uiCurrency;
        [SerializeField] private UIMainMenu _uiMainMenu;

        private CurrencyManager _cm;
        private StageZone _lastStageZone;

        [SerializeField] private Sprite _bombIcon;
        public Sprite BombIcon => _bombIcon;
        private void Start()
        {
            _cm = CurrencyManager.Instance;

        }
        public void CheckTheStage(RewardUnit rewardUnit)
        {
            bool isBomb = rewardUnit.RewardData.RewardType == Utilities.RewardType.Bomb;
            //_cardPanel.InitializePanel(rewardUnit.RewardIcon, isBomb);
            if(isBomb)
            {
                _cardPanel.VisualizeBombPanel();
            } else
            {
                _cardPanel.VisualizeSafePanel(rewardUnit.RewardIcon);
                _rewardPanel.InitializeReward(rewardUnit);

            }
        }
        public void GiveUpRewards()
        {
                _cardPanel.ClosePanel();
            _uiMainMenu.gameObject.SetActive(true);
        }
        public void RequestRevive(/*int revivePrice*/)
        {
            if(_cm.TrySpending("Gold", 10))
            {
                _cardPanel.ClosePanel();
                SetNextStage();
            }
        }
        public void SetNextStage()
        {
            StageManager.Instance.InitializeNextStage();
        }
         
        public void InitializeCurrencyUI(List<CurrencyData> savedCurrencyDatas) => _uiCurrency.InitializeCurrencyUI(savedCurrencyDatas);
        public void UpdateCurrencyUI() => _uiCurrency.UpdateCurrencyUI();

    }
}
