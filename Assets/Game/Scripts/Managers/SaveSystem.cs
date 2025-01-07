 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Utilities;

namespace WheelOfFortune {

    public static class SaveSystem { 
        public static List<CurrencyDataSaveInfo> LoadCurrencyDatas()
        {

            List<CurrencyDataSaveInfo> allCurrencyDatas = new List<CurrencyDataSaveInfo>();


            if(PlayerPrefs.HasKey(Consts.SAVE_INFO_NAME_CURRENCY))

            {
                string serializedCurrencyDatas = PlayerPrefs.GetString(Consts.SAVE_INFO_NAME_CURRENCY); 

                string[] deserializedCurrencyDatas = JsonConvert.DeserializeObject<string[]>(serializedCurrencyDatas) ;
                for(int i = 0; i < deserializedCurrencyDatas.Length; i++)
                {
                    CurrencyDataSaveInfo currencyData = JsonUtility.FromJson<CurrencyDataSaveInfo>(deserializedCurrencyDatas[i]);
                    allCurrencyDatas.Add(currencyData);
                }

            } 
            return allCurrencyDatas;

        } 
        public static void UpdateCurrency(CurrencyData currencyData)
        {
            List<CurrencyDataSaveInfo> allCurrencyDatas = LoadCurrencyDatas();
            bool contains = false;
            for(int i = 0; i < allCurrencyDatas.Count; i++)
            {

                if(allCurrencyDatas[i].CurrencyName == currencyData.Currency.CurrencyName)
                {
                    allCurrencyDatas[i].UpdateAmount( currencyData.CurrentAmount) ;
                    contains = true;
                    break;
                }
            }
            if(!contains)
            { 
                CurrencyDataSaveInfo currencyDataSaveInfo = new CurrencyDataSaveInfo(currencyData.Currency.CurrencyName, currencyData.CurrentAmount);
                allCurrencyDatas.Add(currencyDataSaveInfo);
            }


            string[] allCurrencyDatasJson = new string[allCurrencyDatas.Count];
            for(int i = 0; i < allCurrencyDatasJson.Length; i++)
            {
                allCurrencyDatasJson[i] = JsonUtility.ToJson(allCurrencyDatas[i]); 

            }
            string serializedCurrencyDatas =  JsonConvert.SerializeObject(allCurrencyDatasJson) ; 

            PlayerPrefs.SetString(Consts.SAVE_INFO_NAME_CURRENCY, serializedCurrencyDatas);
            PlayerPrefs.Save();
        }
    }
    [Serializable]
    public class CurrencyDataSaveInfo
    {
        public CurrencyDataSaveInfo(string currencyName,int currentAmount)
        {
            CurrencyName = currencyName;
            CurrentAmount = currentAmount;
        }
        public void UpdateAmount(int amount)
        {
            CurrentAmount = amount;
        } 
        public string CurrencyName ; 
        public  int CurrentAmount ;

    }
}
