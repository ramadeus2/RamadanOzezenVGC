using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Reward;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {
    [CreateAssetMenu(fileName = "NewStage", menuName = "WheelOfFortune/StageSystem/Stage")]
    public class StageData: ScriptableObject {
        [SerializeField] private List<RewardData> _rewardDatas;
        public List<RewardData> RewardDatas => _rewardDatas;

        [SerializeField] private int _stageNo;
        public int StageNo => _stageNo;

        [SerializeField] private StageZone _stageZone;
        public StageZone StageZone => _stageZone;

        public void InitializeStage(List<RewardData> rewardDatas, int stageNo, StageZone stageZone)
        {
            _rewardDatas = rewardDatas;
            _stageNo = stageNo;
            _stageZone = stageZone;
            int rewardAmountMultiplier = 1;
            switch(stageZone)
            {
                case StageZone.SafeZone:
                    rewardAmountMultiplier = 2;
                    break;
                case StageZone.SuperZone:
                    rewardAmountMultiplier = 4;
                    break;
                default:
                    break;
            } 
            PickerWheel.Instance.UpdatePieces(rewardDatas, rewardAmountMultiplier);
        }
    }
}
