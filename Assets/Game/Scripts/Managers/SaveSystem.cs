using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine; 
using WheelOfFortune.Utilities;

namespace WheelOfFortune.SaveManagement {

    public static class SaveSystem {
      /// <summary>
      /// each reward data is being storaged with reward type so we can seperate the reward types. data save name is also seperated. it makes easier to get spesificied datas from spesificied save names.
      /// </summary>
        public static List<DataSaveInfo> LoadDatas(string dataSaveName, RewardType rewardType)
        {
            List<DataSaveInfo> allDatas = new List<DataSaveInfo>();
            if(PlayerPrefs.HasKey(dataSaveName))

            {
                string serializedDatas = PlayerPrefs.GetString(dataSaveName); // first get all the datas

                //seperate the data to array so we can iterate
                string[] deserializedDatas = JsonConvert.DeserializeObject<string[]>(serializedDatas); 
                for(int i = 0; i < deserializedDatas.Length; i++)
                {
                    //cast it to global reward data type so we can see the properties 
                    DataSaveInfo data = JsonUtility.FromJson<DataSaveInfo>(deserializedDatas[i]);

                    // only get the requested save data. for example currency save datas like gold and cash datas
                    if(data.RewardType == rewardType)
                    {
                        allDatas.Add(data);
                    }
                }

            }
           
            return allDatas;

        }
        /// <summary>
        /// first it loads all the saved datas. checks if the new data is already exist. if it exists, it only adds the amount. if not, it adds to the datas and saves.
        /// </summary>
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

            // serialize back to json and save
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
    /// <summary>
    /// this class storages only needed datas. it makes easier to save and load so all reward datas are storaged with this way.
    /// </summary>
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
