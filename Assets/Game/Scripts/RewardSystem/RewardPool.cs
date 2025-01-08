 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Reward {

    [CreateAssetMenu(menuName = "WheelOfFortune/Reward/RewardPool")]

    public class RewardPool: ScriptableObject {
        [SerializeField] private List<RewardData> _normalRewardDatas;
        public List<RewardData> NormalRewardDatas => _normalRewardDatas;

        [SerializeField] private List<RewardData> _specialRewardDatas;
        public List<RewardData> SpecialRewardDatas => _specialRewardDatas;

        [SerializeField] private List<RewardData> _currencyDatas;
        public List<RewardData> CurrencyDatas => _currencyDatas;
        [SerializeField] private RewardData _bombData;
        public RewardData BombData => _bombData;


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
            Debug.Log("ff");
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

        //public void AddOrUpdateReward(RewardData rewardData)
        //{ 
        //    for(int i = 0; i < _normalRewardDatas.Count; i++)
        //    {
        //        if(_normalRewardDatas[i].RewardId == rewardData.RewardId)
        //        {
        //            _normalRewardDatas[i] = rewardData;
        //            return;
        //        }
        //    }
        //    _normalRewardDatas.Add(rewardData);

        //} 
        //public List<RewardData> GetRewardDatasByRank( )
        //{
        //    List<RewardData> rewardDatas = new List<RewardData>();

        //    for(int i = 0; i < _rewardDatas.Count; i++)
        //    {
        //        if(_rewardDatas[i].RewardRank == rewardRank)
        //        {
        //            rewardDatas.Add(_rewardDatas[i]);
        //        }
        //    }

        //    return rewardDatas;
        //}
    }
}
