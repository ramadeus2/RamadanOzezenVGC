
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.CurrencySystem;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.SaveManagement {

    public static class SaveSystem {
        public static List<DataSaveInfo> LoadDatas(string dataSaveName, RewardType rewardType)
        {
            List<DataSaveInfo> allDatas = new List<DataSaveInfo>();


            if(PlayerPrefs.HasKey(dataSaveName))

            {
                string serializedDatas = PlayerPrefs.GetString(dataSaveName);

                string[] deserializedDatas = JsonConvert.DeserializeObject<string[]>(serializedDatas);
                for(int i = 0; i < deserializedDatas.Length; i++)
                {
                    DataSaveInfo data = JsonUtility.FromJson<DataSaveInfo>(deserializedDatas[i]);
                    if(data.RewardType == rewardType)
                    {
                        allDatas.Add(data);
                    }
                }

            }
           
            return allDatas;

        }

        public static void UpdateData(AbstractSaveData data, string dataSaveName,RewardType rewardType)
        {
            List<DataSaveInfo> allDatas = LoadDatas(dataSaveName, rewardType);
            bool contains = false;
            for(int i = 0; i < allDatas.Count; i++)
            {

                if(allDatas[i].DataId == data.DataId)
                {
                    allDatas[i].UpdateAmount(data.CurrentAmount);
                    contains = true;
                    break;
                }
            }
            if(!contains)
            {
                DataSaveInfo dataSaveInfo = new DataSaveInfo(data.DataId, data.CurrentAmount, rewardType);
                allDatas.Add(dataSaveInfo);
            }


            string[] allDatasJson = new string[allDatas.Count];
            for(int i = 0; i < allDatasJson.Length; i++)
            {
                allDatasJson[i] = JsonUtility.ToJson(allDatas[i]);

            }
            string serializedDatas = JsonConvert.SerializeObject(allDatasJson);

            PlayerPrefs.SetString(dataSaveName, serializedDatas);
            PlayerPrefs.Save();
        }
    }
    [Serializable]
    public class
        DataSaveInfo {
        public DataSaveInfo(string dataId, int currentAmount, RewardType rewardType)
        {
            DataId = dataId;
            CurrentAmount = currentAmount;
            RewardType = rewardType;
        }
        public void UpdateAmount(int amount )
        {
            CurrentAmount = amount; 
        }
        public string DataId;
        public int CurrentAmount;
        public RewardType RewardType;

    }
}
