using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.CurrencySystem {

    public class CurrencyManager: MonoSingleton<CurrencyManager> {

        private CurrencyData[] _playerCurrencyDatas;
        private void Start()
        {
            SynchronizeSavedCurrencyDatas();
        }

        private void SynchronizeSavedCurrencyDatas()
        {
            AddressablesManager.Instance.GetCurrencySettings((availableSettings) =>
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
            });
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
        }
        public void CurrencyEarned(string currenyName, int amount)
        {
            CurrencyData currencyData = GetCurrencyData(currenyName);
            currencyData.InsertAmount(amount);
            SaveSystem.UpdateCurrency(currencyData);
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
