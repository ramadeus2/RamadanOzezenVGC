using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;

namespace WheelOfFortune.SaveManagement {

    public class RewardSaveData: AbstractSaveData {
       
        public RewardSaveData(string rewardId,int amount) : base(rewardId,amount)
        {
        }


    }
}
