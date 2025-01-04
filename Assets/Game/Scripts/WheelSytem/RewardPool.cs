using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune {

    [CreateAssetMenu( menuName = "WheelOfFortune/WheelSystem/RewardPool")]

    public class RewardPool: ScriptableObject {
       [SerializeField] private List<RewardData> _rewardDatas;
        public List<RewardData> RewardDatas { get { return _rewardDatas; }}

        public void AddOrUpdateReward(RewardData rewardData)
        { 
            for(int i = 0; i < _rewardDatas.Count; i++)
            {
                if(_rewardDatas[i].RewardId == rewardData.RewardId)
                {
                    _rewardDatas[i] = rewardData;
                    return;
                }
            }
            _rewardDatas.Add(rewardData);
         
        } 
    }
}
