using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.General;
using WheelOfFortune.SaveManagement;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.CurrencySystem {

    public class CurrencyManager: MonoSingleton<CurrencyManager> {

        private List<CurrencySaveData> _playerCurrencyDatas;
        private GameSettings _gameSettings;
        private UIManager _uiManager;
        private void Start()
        {
            _uiManager = UIManager.Instance;
            _gameSettings = GameManager.Instance.GameSettings;
            SynchronizeSavedCurrencyDatas();
        }

        public void SynchronizeSavedCurrencyDatas()
        {


            List<DataSaveInfo> savedCurrencyDataInfos = SaveSystem.LoadDatas (Consts.SAVE_INFO_NAME_CURRENCY,RewardType.Currency);
            List<CurrencySaveData> savedCurrencyDatas = new List<CurrencySaveData>();
            CurrencySettings availableSettings = _gameSettings.CurrencySettings;

            for(int a = 0; a < availableSettings.AvailableCurrencies.Count; a++)
            { 
                bool contains = false;
                CurrencySaveData newCurrentData = null;
                for(int b = 0; b < savedCurrencyDataInfos.Count; b++)
                {
                    if(availableSettings.AvailableCurrencies[a].CurrencyRewardData.RewardId == savedCurrencyDataInfos[b].DataId)
                    {
                        contains = true;
                        newCurrentData = new CurrencySaveData(savedCurrencyDataInfos[b].DataId,savedCurrencyDataInfos[b].CurrentAmount, availableSettings.AvailableCurrencies[a]);

                        break;
                    }
                }
                if(!contains)
                {
                    newCurrentData = new CurrencySaveData(availableSettings.AvailableCurrencies[a].CurrencyRewardData.RewardId,availableSettings.AvailableCurrencies[a]);
                    SaveSystem.UpdateData(newCurrentData, Consts.SAVE_INFO_NAME_CURRENCY,RewardType.Currency);
                } 
                savedCurrencyDatas.Add(newCurrentData);

            }
                _playerCurrencyDatas = savedCurrencyDatas;
                _uiManager.InitializeCurrencyUI(_playerCurrencyDatas);

        }


        public bool TrySpending(string currencyId, int amount)
        {
            CurrencySaveData currencyData = GetCurrencyData(currencyId);

            if(currencyData.CurrentAmount >= amount)
            {
                CurrencySpent(currencyData, amount);
                return true;
            }
            return false;
        }
        private void CurrencySpent(CurrencySaveData currencyData, int amount)
        {
            currencyData.ExtractAmount(amount);
            SaveSystem.UpdateData(currencyData, Consts.SAVE_INFO_NAME_CURRENCY,RewardType.Currency);
            _uiManager.UpdateCurrencyUI();
        }
        public void CurrencyEarned(string currencyId, int amount)
        {
            CurrencySaveData currencyData = GetCurrencyData(currencyId);
            currencyData.InsertAmount(amount);
            SaveSystem.UpdateData(currencyData, Consts.SAVE_INFO_NAME_CURRENCY, RewardType.Currency);
            _uiManager.UpdateCurrencyUI();
        }
        public CurrencySaveData GetCurrencyData(string currencyId)
        { 
            for(int i = 0; i < _playerCurrencyDatas.Count; i++)
            {  
                if(_playerCurrencyDatas[i].Currency.CurrencyRewardData.RewardId == currencyId)
                {
                    return _playerCurrencyDatas[i];
                }
            }
            return null;
        }
    }
}
