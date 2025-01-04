using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem: MonoSingleton<AbstractStageSystem> {
        [SerializeField] protected RewardPool _rewardPool;
        [SerializeField] protected StageData[] _stageDatas;

    }
}
