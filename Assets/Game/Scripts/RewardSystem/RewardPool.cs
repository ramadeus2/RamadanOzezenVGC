using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Reward {

    [CreateAssetMenu(menuName = "WheelOfFortune/Reward/RewardPool")]

    public class RewardPool: ScriptableObject {

        #region FIELDS

        [SerializeField] private List<RewardData> _normalRewardDatas;
        public List<RewardData> NormalRewardDatas => _normalRewardDatas;

        [SerializeField] private List<RewardData> _specialRewardDatas;
        public List<RewardData> SpecialRewardDatas => _specialRewardDatas;

        [SerializeField] private List<RewardData> _currencyDatas;
        public List<RewardData> CurrencyDatas => _currencyDatas;

        [SerializeField] private RewardData _bombData; // bomb data is singular so no need to be in a list
        public RewardData BombData => _bombData;
        #endregion
        #region BEHAVIOURS

        public void InitializeBomb(RewardData bombData)
        {
            _bombData = bombData;
            bombData.InitializeRewardType(RewardType.Bomb);
        }

        public RewardData GetRewardData(string id)
        { 
            RewardData rewardData;

            if(GetRewardData(id,RewardType.Normal, out rewardData))
            {
                return rewardData;
            }else if(GetRewardData(id, RewardType.Special, out rewardData))
            {
                return rewardData;
            }else if(GetRewardData(id, RewardType.Currency, out rewardData))
            {
                return rewardData;
            }
            return null;
        }
        public bool   GetRewardData(string id,RewardType rewardType, out RewardData rewardData)
        {
            List<RewardData> rewardDatas = null;

            rewardData = null;
            switch(rewardType)
            {
                case RewardType.Normal:
                    rewardDatas = _normalRewardDatas;
                    break;
                case RewardType.Currency:
                    rewardDatas = _currencyDatas;
                    break;
                case RewardType.Special:
                    rewardDatas = _specialRewardDatas;
                    break;
                case RewardType.Bomb:
                    rewardData = BombData;
                    return true; ; 
                default:
                    break;
            }
            for(int i = 0; i < rewardDatas.Count; i++)
            {
                if(rewardDatas[i].RewardId == id)
                {
                    rewardData = rewardDatas[i];
                    return true;   
                }
            }

            return false;
        }
        public RewardData GetRewardDataByName(string name)
        {
            RewardData rewardData;

            if(GetRewardData(name, RewardType.Normal, out rewardData))
            {
                return rewardData;
            } else if(GetRewardData(name, RewardType.Special, out rewardData))
            {
                return rewardData;
            } else if(GetRewardData(name, RewardType.Currency, out rewardData))
            {
                return rewardData;
            }

            return null;
        }
        public bool GetRewardDataByName(string name, RewardType rewardType, out RewardData rewardData)
        {
            List<RewardData> rewardDatas = null;

            rewardData = null;
            switch(rewardType)
            {
                case RewardType.Normal:
                    rewardDatas = _normalRewardDatas;
                    break;
                case RewardType.Currency:
                    rewardDatas = _currencyDatas;
                    break;
                case RewardType.Special:
                    rewardDatas = _specialRewardDatas;
                    break;
                case RewardType.Bomb:
                    rewardData = BombData;
                    return true; ;
                default:
                    break;
            }
            for(int i = 0; i < rewardDatas.Count; i++)
            {
                if(rewardDatas[i].RewardName == name)
                {
                    rewardData = rewardDatas[i];
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// its for automatic stage system. it chooses random 8 rewards(it can be set from GameSettings scriptable obj)  regarding their possibilities
        /// and also includes the bomb if the stagezone is dangerzone
        /// </summary>
        public List<RewardData> GetRandomRewards(int stageCount,StageZone stageZone )
        {
            List<RewardData> rewardDatas = new List<RewardData>();
            for(int i = 0; i < NormalRewardDatas.Count; i++)
            {
                rewardDatas.Add(NormalRewardDatas[i]);
            }

            for(int i = 0; i < CurrencyDatas.Count; i++)
            {
                rewardDatas.Add(CurrencyDatas[i]);
            }

            int totalPossibility = 0;
            foreach(var reward in rewardDatas)
            {
                totalPossibility += reward.Probability;
            }

            List<RewardData> selectedRewards = new List<RewardData>();
            bool willThereBeBomb = stageZone == StageZone.DangerZone;
            int manuallyAddedItemCount = 0;
            if(willThereBeBomb)
            {
                selectedRewards.Add(BombData);
                manuallyAddedItemCount++;
            }


            List<RewardData> availableRewards = new List<RewardData>(rewardDatas);

            for(int i = 0; i < stageCount - manuallyAddedItemCount; i++) // if we added the bomb
            {
                if(availableRewards.Count == 0)
                    break;

                int randomValue = Random.Range(0, totalPossibility);
                int cumulativePossibility = 0;

                foreach(var reward in availableRewards)
                {
                    cumulativePossibility += reward.Probability;

                    if(randomValue < cumulativePossibility)
                    {
                        selectedRewards.Add(reward);

                        totalPossibility -= reward.Probability;
                        availableRewards.Remove(reward);

                        break;
                    }
                }
            }
            return selectedRewards;
        }
        #endregion

        
    }
}
