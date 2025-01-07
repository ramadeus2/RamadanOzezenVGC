using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
