using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem: MonoBehaviour {
        [SerializeField] protected int _stageCount;
        protected GameSettings _gameSettings;
        protected StageData _currentStage;
        protected int _currentStageNo = 0;

        protected virtual void OnEnable()
        {

            InitializeNextStage();
            UIManager.Instance.InitializeStageBar(_stageCount);

        }

        protected virtual void OnDisable()
        {
            _currentStage = null;
            _currentStageNo = 0;
        }




        public virtual void InitializeNextStage()
        {
            if(!_gameSettings)
            {
                _gameSettings = GameManager.Instance.GameSettings;
            }
            if(_currentStageNo >= _stageCount)
            {
                return;
            }
            _currentStageNo++;
            StageZone stageZone = Helpers.GetStageZone(_currentStageNo);
            SetCurrentStage();

            UpdateZoneRewardIcon(stageZone,StageZone.SuperZone);
            UpdateZoneRewardIcon(stageZone,StageZone.SafeZone);



            List<RewardData> rewardDatas = GetRewardList(stageZone);
            _currentStage.RunStage(rewardDatas, _currentStageNo, stageZone);


        }
        private void UpdateZoneRewardIcon(StageZone currentStageZone,StageZone targetZone)
        {
                Sprite zoneRewardIcon = null;
            if(_gameSettings.TryGetZoneRewardData(_currentStageNo, targetZone, out int zoneTargetStageNo, out RewardData specialReward))
            {
                    zoneRewardIcon = _gameSettings.GetRewardSprite(specialReward.SpriteName);
            }
            else
            {
                zoneRewardIcon = _gameSettings.SafeZoneRewardIcon;
            }
            UIManager.Instance.InitializeSpecialRewardIcon(targetZone, zoneTargetStageNo, zoneRewardIcon);
  

            if(currentStageZone == StageZone.SuperZone)
            {
                UIManager.Instance.ActivateSpecialRewardPopUp();
            }
        }
        protected abstract void SetCurrentStage();
        protected abstract List<RewardData> GetRewardList(StageZone stageZone);
    }

}
