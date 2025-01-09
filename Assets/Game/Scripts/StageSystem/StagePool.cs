using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Stage { 
    [CreateAssetMenu(fileName = "StagePool", menuName = "WheelOfFortune/StageSystem/StagePool")]
public class StagePool : ScriptableObject {

    [SerializeField] private List<StageData> _stageDatas;
    public List<StageData> StageDatas => _stageDatas;
       
    }
}
