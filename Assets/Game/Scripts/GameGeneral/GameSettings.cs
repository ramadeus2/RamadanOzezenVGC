using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.General {

    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "WheelOfFortune/General/GameSettings")]
    public class GameSettings: ScriptableObject {
       [SerializeField] private   int _stageSuperZoneMultiplier = 30 ;
        public int StageSuperZoneMultiplier => _stageSuperZoneMultiplier;
        [SerializeField] private   int _stageSafeZoneMultiplier = 5;
        public int StageSafeZoneMultiplier => _stageSafeZoneMultiplier;
        [SerializeField] private   int _stageRewardAmount = 8;
        public int StageRewardAmount => _stageRewardAmount;
    }
}
