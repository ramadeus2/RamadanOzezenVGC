using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
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

        public void RunStage()
        { 
            int rewardAmountMultiplier = 1;
            switch(_stageZone)
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
            PickerWheel.Instance.UpdatePieces(_rewardDatas,_stageZone, rewardAmountMultiplier);
        }
        public void RunStage(List<RewardData> rewardDatas, int stageNo, StageZone stageZone)
        {
            _rewardDatas = rewardDatas;
            _stageNo = stageNo;
            _stageZone = stageZone;
            RunStage();
        }
        public void InitializeStageData( int stageNo, StageZone stageZone,GameSettings gameSettings,RewardData bombData=null)
        { 
            _stageNo = stageNo;
            _stageZone = stageZone;
            RewardData[] rewardDataArray = new RewardData[gameSettings.StageRewardAmount];
            if(stageZone == StageZone.DangerZone)
            {
                rewardDataArray[0] =bombData; 
            }
            _rewardDatas = rewardDataArray.ToList();
            
        }

    }
}
