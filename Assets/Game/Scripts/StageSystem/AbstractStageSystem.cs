using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WheelOfFortune.General;
using WheelOfFortune.Reward;
using WheelOfFortune.UserInterface;
using WheelOfFortune.Utilities;
using Zenject;

namespace WheelOfFortune.Stage {

    public abstract class AbstractStageSystem: MonoBehaviour {
        protected int _stageCount;
        protected GameSettings _gameSettings;
        protected StageData _currentStage;
        protected int _currentStageNo = 0;
        private UIRewardPanel _uiRewardPanel;
        protected UIManager _uiManager;
        [Inject]
        private void Constructor(UIRewardPanel uiRewardPanel, UIManager uIManager)
        {
            _uiRewardPanel = uiRewardPanel;
            _uiManager = uIManager;
        }
        protected virtual void OnEnable()
        {
            if(!_gameSettings)
            {
                _gameSettings = GameManager.Instance.GameSettings;
            } 
            _currentStageNo = 0;
            SetStageCount();
            InitializeNextStage();
            _uiManager.InitializeStageBar(_stageCount);
            _uiManager.ResetReviveTime();

        }
        public virtual bool InitializeNextStage()
        { 
            if(_currentStageNo >= _stageCount)
            {
                return false;
            }
            _currentStageNo++;
            StageZone stageZone = Helpers.GetStageZone(_currentStageNo);
            if(stageZone == StageZone.SuperZone)
            {
                CollectSpecialReward();
            }
            SetCurrentStage();

            UpdateZoneRewardIcon(StageZone.SuperZone);
            UpdateZoneRewardIcon(StageZone.SafeZone);



            List<RewardData> rewardDatas = GetRewardList(stageZone);
            RunStage(rewardDatas, _currentStageNo, stageZone);

            return true;
        }

        private void CollectSpecialReward()
        {
            _gameSettings.TryGetZoneRewardData(_currentStageNo, StageZone.SuperZone, out int zoneTargetStageNo, out RewardData specialReward);
            Sprite zoneRewardIcon = _gameSettings.GetRewardSprite(specialReward.SpriteName);
            RewardUnit rewardUnit = new RewardUnit(specialReward, zoneRewardIcon, specialReward.MinAmount);
            _uiRewardPanel.InitializeReward(rewardUnit);
           _uiManager.ActivateSpecialRewardPopUp();
        }
        protected abstract void RunStage(List<RewardData> rewardDatas, int stageNo, StageZone stageZone);
        private void UpdateZoneRewardIcon(StageZone targetZone)
        {
           
                Sprite zoneRewardIcon;
            if(_gameSettings.TryGetZoneRewardData(_currentStageNo, targetZone, out int zoneTargetStageNo, out RewardData specialReward))
            {
                    zoneRewardIcon = _gameSettings.GetRewardSprite(specialReward.SpriteName);
            }
            else
            {
                zoneRewardIcon = _gameSettings.SafeZoneRewardIcon;
            }
            _uiManager.InitializeZoneRewardIcon(targetZone, zoneTargetStageNo, zoneRewardIcon);
  

        }
        protected abstract void SetStageCount(); 

        protected abstract void SetCurrentStage();
        protected abstract List<RewardData> GetRewardList(StageZone stageZone);
    }

}
