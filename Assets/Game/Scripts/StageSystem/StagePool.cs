using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage { 
    [CreateAssetMenu(fileName = "StagePool", menuName = "WheelOfFortune/StageSystem/StagePool")]
public class StagePool : ScriptableObject {
    [SerializeField] private List<StageData> _stageDatas;
    public List<StageData> StageDatas => _stageDatas;
        public StageZone GetStageZone(int stageNo,GameSettings gameSettings)
        {
            StageZone stageZone = StageZone.DangerZone;
            if(stageNo != 0 && stageNo % gameSettings.StageSuperZoneMultiplier== 0)
            {
                stageZone = StageZone.SuperZone;
            } else if(stageNo != 0 && stageNo % gameSettings.StageSafeZoneMultiplier == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
    }
}
