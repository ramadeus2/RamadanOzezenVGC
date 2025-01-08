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
       
    }
}
