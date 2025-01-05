using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage { 
    [CreateAssetMenu(fileName = "StagePool", menuName = "WheelOfFortune/StageSystem/StagePool")]
public class StagePool : ScriptableObject {
    [SerializeField] private List<StageData> _stageDatas;
    public List<StageData> StageDatas => _stageDatas;
        public StageZone GetStageZone(int stageNo)
        {
            StageZone stageZone = StageZone.DangerZone;
            if(stageNo != 0 && stageNo % Consts.STAGE_SUPERZONE_MULTIPLIER == 0)
            {
                stageZone = StageZone.SuperZone;
            } else if(stageNo != 0 && stageNo % Consts.STAGE_SAFEZONE_MULTIPLIER == 0)
            {
                stageZone = StageZone.SafeZone;
            }
            return stageZone;
        }
    }
}
