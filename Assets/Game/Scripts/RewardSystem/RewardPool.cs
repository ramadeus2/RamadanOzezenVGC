using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Reward {

    [CreateAssetMenu( menuName = "WheelOfFortune/Reward/RewardPool")]

    public class RewardPool: ScriptableObject {
       [SerializeField] private List<RewardData> _normalRewardDatas;
        public List<RewardData> NormalRewardDatas => _normalRewardDatas;

        [SerializeField] private List<RewardData> _specialRewardDatas;
        public List<RewardData> SpecialRewardDatas => _specialRewardDatas;


        [SerializeField] private  RewardData  _bombData;
        public  RewardData  BombData => _bombData;



        public void AddOrUpdateReward(RewardData rewardData)
        { 
            for(int i = 0; i < _normalRewardDatas.Count; i++)
            {
                if(_normalRewardDatas[i].RewardId == rewardData.RewardId)
                {
                    _normalRewardDatas[i] = rewardData;
                    return;
                }
            }
            _normalRewardDatas.Add(rewardData);
         
        } 
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
