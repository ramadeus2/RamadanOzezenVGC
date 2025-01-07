using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.CurrencySystem {

    public class CurrencyManager: MonoSingleton<CurrencyManager> {

        private CurrencyData[] _playerCurrencyDatas;
        private AddressablesManager _addressablesManager;
        private UIManager _uiManager;
        private void Start()
        {
            _uiManager = UIManager.Instance;
            _addressablesManager = AddressablesManager.Instance;
            SynchronizeSavedCurrencyDatas();
        }

        private void SynchronizeSavedCurrencyDatas()
        {
            _addressablesManager.GetRewardAtlas((availableSettings) =>
            {
                _addressablesManager.GetCurrencySettings((availableSettings) =>
            {
                List<CurrencyDataSaveInfo> savedCurrencyDataInfos = SaveSystem.LoadCurrencyDatas();
                List<CurrencyData> savedCurrencyDatas = new List<CurrencyData>();


                for(int a = 0; a < availableSettings.AvailableCurrencies.Count; a++)
                {
                    bool contains = false;
                    CurrencyData newCurrentData = null;
                    for(int b = 0; b < savedCurrencyDataInfos.Count; b++)
                    {
                        if(availableSettings.AvailableCurrencies[a].CurrencyName == savedCurrencyDataInfos[b].CurrencyName)
                        {
                            contains = true;
                            newCurrentData = new CurrencyData(availableSettings.AvailableCurrencies[a], savedCurrencyDataInfos[b].CurrentAmount);

                            break;
                        }
                    }
                    if(!contains)
                    {
                        newCurrentData = new CurrencyData(availableSettings.AvailableCurrencies[a]);
                        SaveSystem.UpdateCurrency(newCurrentData);
                    }
                    savedCurrencyDatas.Add(newCurrentData);
                }
                _playerCurrencyDatas = savedCurrencyDatas.ToArray();
                AddressablesManager.Instance.ReleaseCurrencySettings();
                _uiManager.InitializeCurrencyUI(savedCurrencyDatas);
                });
            }, "CurrencySprites");
        }


        public bool TrySpending(string currencyName, int amount)
        {
            CurrencyData currencyData = GetCurrencyData(currencyName);

            if(currencyData.CurrentAmount >= amount)
            {
                CurrencySpent(currencyData, amount);
                return true;
            }
            return false;
        }
        private void CurrencySpent(CurrencyData currencyData, int amount)
        {
            currencyData.ExtractAmount(amount);
            SaveSystem.UpdateCurrency(currencyData);
            _uiManager.UpdateCurrencyUI();
        }
        public void CurrencyEarned(string currenyName, int amount)
        {
            CurrencyData currencyData = GetCurrencyData(currenyName);
            currencyData.InsertAmount(amount);
            SaveSystem.UpdateCurrency(currencyData);
            _uiManager.UpdateCurrencyUI();
        }
        public CurrencyData GetCurrencyData(string currencyName)
        {
            for(int i = 0; i < _playerCurrencyDatas.Length; i++)
            {
                if(_playerCurrencyDatas[i].Currency.CurrencyName == currencyName)
                {
                    return _playerCurrencyDatas[i];
                }
            }
            return null;
        }
    }
}
