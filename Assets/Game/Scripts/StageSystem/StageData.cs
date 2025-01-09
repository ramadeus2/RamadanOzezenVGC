using System.Collections.Generic; 
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities; 

namespace WheelOfFortune.Stage {
    [CreateAssetMenu(fileName = "NewStage", menuName = "WheelOfFortune/StageSystem/Stage")]
    public class StageData: ScriptableObject {
       public List<RewardData> _rewardDatas;
        public List<RewardData> RewardDatas => _rewardDatas;
         
        [SerializeField] private StageZone _stageZone;
        public StageZone StageZone => _stageZone; 
         
        [SerializeField] private int _stageNo;
        public int StageNo => _stageNo;

        public void RunStage(GameSettings gameSettings)
        {
            
            int rewardAmountMultiplier = 1;
            switch(_stageZone)
            {
                case StageZone.SafeZone:
                    rewardAmountMultiplier = gameSettings.StageRewardMultiplierSafeZone;
                    break;
                case StageZone.SuperZone:
                    rewardAmountMultiplier = gameSettings.StageRewardMultiplierSuperZone;
                    break;
                default:
                    break;
            }
            PickerWheel.Instance.UpdatePieces(_rewardDatas,_stageZone, rewardAmountMultiplier);
        }
        public void RunStage(GameSettings gameSettings,List<RewardData> rewardDatas, int stageNo, StageZone stageZone)
        {
            _rewardDatas = rewardDatas;
            _stageNo = stageNo;
            _stageZone = stageZone;
            RunStage(gameSettings);
        }
        public void InitializeStageData( int stageNo, StageZone stageZone,List<RewardData> rewardDatas )
        { 
            _stageNo = stageNo;
            _stageZone = stageZone; 
            _rewardDatas = rewardDatas;

        }
        public void InitializeRewards( List<RewardData> rewardDatas)
        { 

            _rewardDatas = rewardDatas;

        }

    }
}
